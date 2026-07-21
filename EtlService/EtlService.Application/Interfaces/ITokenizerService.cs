namespace EtlService.Application.Interfaces;

public interface ITokenizerService
{
    IReadOnlyList<string> Tokenize(string text);
}
