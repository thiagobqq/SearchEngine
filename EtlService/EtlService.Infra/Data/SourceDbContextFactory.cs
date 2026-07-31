using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EtlService.Infra.Data;

public class SourceDbContextFactory : IDesignTimeDbContextFactory<SourceDbContext>
{
    public SourceDbContext CreateDbContext(string[] args)
    {
        Env.TraversePath().Load();

        var connectionString = Environment.GetEnvironmentVariable("ETL_SOURCE_CONNECTION_STRING");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("ETL_SOURCE_CONNECTION_STRING não foi configurada nas variáveis de ambiente.");
        }

        var optionsBuilder = new DbContextOptionsBuilder<SourceDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new SourceDbContext(optionsBuilder.Options);
    }
}
