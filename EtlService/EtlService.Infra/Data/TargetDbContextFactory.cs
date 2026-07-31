using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EtlService.Infra.Data;

public class TargetDbContextFactory : IDesignTimeDbContextFactory<TargetDbContext>
{
    public TargetDbContext CreateDbContext(string[] args)
    {
        Env.TraversePath().Load();

        var connectionString = Environment.GetEnvironmentVariable("ETL_TARGET_CONNECTION_STRING");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("ETL_TARGET_CONNECTION_STRING não foi configurada nas variáveis de ambiente.");
        }

        var optionsBuilder = new DbContextOptionsBuilder<TargetDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new TargetDbContext(optionsBuilder.Options);
    }
}
