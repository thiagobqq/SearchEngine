namespace EtlService.Domain.Models;

public class IndexedPage
{
    public long Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string ContentHash { get; set; } = string.Empty;
    public DateTime IndexedAt { get; set; }
}
