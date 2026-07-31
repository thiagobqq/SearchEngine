using EtlService.Application.Interfaces;
using Porter2Stemmer;

namespace EtlService.Infra.Services;

public class StemmerService : IStemmerService
{
    private readonly EnglishPorter2Stemmer _stemmer = new();

    public string Stem(string word) => _stemmer.Stem(word).Value;
}
