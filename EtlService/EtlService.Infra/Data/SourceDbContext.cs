using EtlService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EtlService.Infra.Data;

public class SourceDbContext : DbContext
{
    public SourceDbContext(DbContextOptions<SourceDbContext> options) : base(options)
    {
    }

    public DbSet<SourcePage> Pages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SourcePage>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Url).IsUnique();
            entity.Property(e => e.Title).HasMaxLength(500);
            entity.Property(e => e.Content).HasColumnType("nvarchar(max)");
        });
    }
}
