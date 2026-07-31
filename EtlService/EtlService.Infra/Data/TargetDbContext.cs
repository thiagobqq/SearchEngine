using EtlService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EtlService.Infra.Data;

public class TargetDbContext : DbContext
{
    public TargetDbContext(DbContextOptions<TargetDbContext> options) : base(options)
    {
    }

    public DbSet<SearchTerm> SearchTerms { get; set; }
    public DbSet<SearchPosting> SearchPostings { get; set; }
    public DbSet<IndexedPage> IndexedPages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SearchTerm>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Term).IsUnique();
            entity.Property(e => e.Term).HasMaxLength(200);
        });

        modelBuilder.Entity<SearchPosting>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.TermId);
            entity.HasIndex(e => e.PageId);
            entity.HasIndex(e => new { e.TermId, e.PageId });
        });

        modelBuilder.Entity<IndexedPage>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Url);
        });
    }
}
