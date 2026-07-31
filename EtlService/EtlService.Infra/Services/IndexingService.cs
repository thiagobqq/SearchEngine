using System.Text.Json;
using EtlService.Application.Interfaces;
using EtlService.Domain.Models;
using EtlService.Domain.Utils;
using EtlService.Domain.ValueObjects;
using EtlService.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace EtlService.Infra.Services;

public class IndexingService : IIndexingService
{
    private readonly TargetDbContext _db;
    private readonly ITokenizerService _tokenizer;
    private readonly IStemmerService _stemmer;

    public IndexingService(TargetDbContext db, ITokenizerService tokenizer, IStemmerService stemmer)
    {
        _db = db;
        _tokenizer = tokenizer;
        _stemmer = stemmer;
    }

    public async Task<bool> IndexPageAsync(PageMessage message, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(message.Content) && string.IsNullOrWhiteSpace(message.Title))
        {
            return false;
        }

        var indexed = await _db.IndexedPages
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == message.PageId, cancellationToken);

        if (indexed != null && indexed.ContentHash == message.ContentHash)
        {
            return false;
        }

        var oldPostings = await Queryable
            .Where<SearchPosting>(_db.SearchPostings, p => p.PageId == message.PageId)
            .ToListAsync(cancellationToken);

        _db.SearchPostings.RemoveRange(oldPostings);
        var removedTermIds = Enumerable.Select<SearchPosting, int>(oldPostings, p => p.TermId).Distinct().ToList();

        var titleTokens = Prepare(message.Title);
        var contentTokens = Prepare(message.Content);

        var stats = BuildTermStats(titleTokens, contentTokens);

        if (stats.Count > 0)
        {
            await IndexTermsAsync(stats, message.PageId, removedTermIds, cancellationToken);
        }

        await UpsertIndexedPageAsync(message, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }

    private async Task IndexTermsAsync(
        Dictionary<string, TermStats> stats,
        long pageId,
        List<int> removedTermIds,
        CancellationToken cancellationToken)
    {
        var termTexts = stats.Keys.ToList();

        var existingTerms = await Queryable
            .Where<SearchTerm>(_db.SearchTerms, t => termTexts.Contains(t.Term))
            .ToDictionaryAsync(t => t.Term, cancellationToken);

        foreach (var term in termTexts)
        {
            if (!existingTerms.ContainsKey(term))
            {
                var entity = new SearchTerm { Term = term, DocumentFrequency = 0 };
                _db.SearchTerms.Add(entity);
                existingTerms[term] = entity;
            }
        }

        await _db.SaveChangesAsync(cancellationToken);

        var affectedTerms = new List<SearchTerm>();
        foreach (var term in termTexts)
        {
            var termEntity = existingTerms[term];
            var termStats = stats[term];

            var posting = await _db.SearchPostings
                .FirstOrDefaultAsync(p => p.TermId == termEntity.Id && p.PageId == pageId, cancellationToken);

            if (posting == null)
            {
                _db.SearchPostings.Add(new SearchPosting
                {
                    TermId = termEntity.Id,
                    PageId = pageId,
                    TermFrequency = termStats.Count,
                    InTitle = termStats.InTitle,
                    Positions = JsonSerializer.Serialize(termStats.Positions)
                });
            }
            else
            {
                posting.TermFrequency = termStats.Count;
                posting.InTitle = termStats.InTitle;
                posting.Positions = JsonSerializer.Serialize(termStats.Positions);
            }

            affectedTerms.Add(termEntity);
        }

        foreach (var termId in removedTermIds)
        {
            var term = await _db.SearchTerms.FindAsync(new object[] { termId }, cancellationToken);
            if (term != null)
            {
                affectedTerms.Add(term);
            }
        }

        await _db.SaveChangesAsync(cancellationToken);

        foreach (var term in affectedTerms.Distinct())
        {
            term.DocumentFrequency = await _db.SearchPostings
                .CountAsync(p => p.TermId == term.Id, cancellationToken);
        }
    }

    private async Task UpsertIndexedPageAsync(PageMessage message, CancellationToken cancellationToken)
    {
        var indexed = await _db.IndexedPages
            .FirstOrDefaultAsync(p => p.Id == message.PageId, cancellationToken);

        if (indexed == null)
        {
            _db.IndexedPages.Add(new IndexedPage
            {
                Id = message.PageId,
                Url = message.Url,
                ContentHash = message.ContentHash,
                IndexedAt = DateTime.UtcNow
            });
        }
        else
        {
            indexed.Url = message.Url;
            indexed.ContentHash = message.ContentHash;
            indexed.IndexedAt = DateTime.UtcNow;
        }
    }

    private List<string> Prepare(string text)
    {
        return _tokenizer.Tokenize(text)
            .Where(t => !StopWords.Contains(t))
            .Select(_stemmer.Stem)
            .ToList();
    }

    private static Dictionary<string, TermStats> BuildTermStats(List<string> titleTokens, List<string> contentTokens)
    {
        var stats = new Dictionary<string, TermStats>();

        for (var i = 0; i < titleTokens.Count; i++)
        {
            AddToken(stats, titleTokens[i], i, inTitle: true);
        }

        for (var i = 0; i < contentTokens.Count; i++)
        {
            AddToken(stats, contentTokens[i], titleTokens.Count + i, inTitle: false);
        }

        return stats;
    }

    private static void AddToken(Dictionary<string, TermStats> stats, string token, int position, bool inTitle)
    {
        if (!stats.TryGetValue(token, out var termStats))
        {
            termStats = new TermStats();
            stats[token] = termStats;
        }

        termStats.Count++;
        termStats.Positions.Add(position);

        if (inTitle)
        {
            termStats.InTitle = true;
        }
    }

    private sealed class TermStats
    {
        public int Count { get; set; }
        public bool InTitle { get; set; }
        public List<int> Positions { get; } = new();
    }
}
