using Microsoft.EntityFrameworkCore;
using SearchEgnine.Domain.Models;

namespace SearchEgnine.Infra.Data
{
    public class SearchDbContext : DbContext
    {
        public SearchDbContext(DbContextOptions<SearchDbContext> options) : base(options)
        {
        }

        public DbSet<Page> Pages { get; set; }
        
    }
}