using System.Text.RegularExpressions;
using EtlService.Application.Interfaces;

namespace EtlService.Application.Services;

public partial class TokenizerService : ITokenizerService
{
    public IReadOnlyList<string> Tokenize(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return Array.Empty<string>();
        }

        return WordRegex()
            .Matches(text.ToLowerInvariant())
            .Select(m => m.Value)
            .ToList();
    }

    [GeneratedRegex(@"[a-z0-9]+(?:'[a-z0-9]+)*", RegexOptions.CultureInvariant)]
    private static partial Regex WordRegex();
}
