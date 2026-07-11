namespace EtlService.Domain.Models;

public class SearchPosting
{
    public long Id { get; set; }
    public int TermId { get; set; }
    public long PageId { get; set; }
    public int TermFrequency { get; set; }
    public bool InTitle { get; set; }
    public string Positions { get; set; } = "[]";
}
