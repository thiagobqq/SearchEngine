namespace EtlService.Domain.Models;

public class SearchTerm
{
    public int Id { get; set; }
    public string Term { get; set; } = string.Empty;
    public int DocumentFrequency { get; set; }
}
