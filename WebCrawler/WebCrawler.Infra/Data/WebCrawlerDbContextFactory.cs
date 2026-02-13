using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WebCrawler.Infra.Data
{
    public class WebCrawlerDbContextFactory : IDesignTimeDbContextFactory<WebCrawlerDbContext>
    {
        public WebCrawlerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WebCrawlerDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=WebCrawlerDb;Trusted_Connection=True;TrustServerCertificate=True;");

            return new WebCrawlerDbContext(optionsBuilder.Options);
        }
    }
}
