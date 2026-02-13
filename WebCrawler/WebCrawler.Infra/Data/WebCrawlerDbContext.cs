using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebCrawler.Domain.Models;

namespace WebCrawler.Infra.Data
{
    public class WebCrawlerDbContext : DbContext
    {
        public WebCrawlerDbContext(DbContextOptions<WebCrawlerDbContext> options) : base(options)
        {
        }

        public DbSet<Page> Pages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Page>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Url).IsRequired().HasMaxLength(2048);
                entity.Property(e => e.Title).HasMaxLength(500);
                entity.Property(e => e.Content).HasColumnType("nvarchar(max)");
            });
        }

        
    }
}