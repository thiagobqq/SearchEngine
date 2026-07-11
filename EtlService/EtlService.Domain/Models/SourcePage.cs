namespace EtlService.Domain.Models;

public class SourcePage
{
    public long Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}
