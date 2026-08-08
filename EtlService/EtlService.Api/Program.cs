using DotNetEnv;
using EasyNetQ;
using EtlService.Application.Interfaces;
using EtlService.Application.Services;
using EtlService.Infra;
using EtlService.Infra.Data;
using EtlService.Infra.Services;
using EtlService.Worker;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

Env.TraversePath().Load();

var builder = Host.CreateApplicationBuilder(args);

var rabbitMqUrl = Environment.GetEnvironmentVariable("RABBITMQ_URL")
    ?? builder.Configuration["RabbitMQ:Url"]
    ?? "amqp://guest:guest@localhost:5672";

builder.Services.AddEasyNetQ(rabbitMqUrl)
    .UseSystemTextJson();

var targetConnectionString = Environment.GetEnvironmentVariable("ETL_TARGET_CONNECTION_STRING")
    ?? builder.Configuration.GetConnectionString("TargetConnection")
    ?? throw new InvalidOperationException("ETL_TARGET_CONNECTION_STRING não foi configurada.");

builder.Services.AddDbContext<TargetDbContext>(options => options.UseSqlServer(targetConnectionString));

var sourceConnectionString = Environment.GetEnvironmentVariable("ETL_SOURCE_CONNECTION_STRING")
    ?? builder.Configuration.GetConnectionString("SourceConnection");

if (!string.IsNullOrWhiteSpace(sourceConnectionString))
{
    builder.Services.AddDbContext<SourceDbContext>(options =>
    {
        options.UseSqlServer(sourceConnectionString).EnableSensitiveDataLogging();
    });
}

builder.Services.AddSingleton<ITokenizerService, TokenizerService>();
builder.Services.AddSingleton<IStemmerService, StemmerService>();
builder.Services.AddScoped<IIndexingService, IndexingService>();

builder.Services.AddHostedService<Worker>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TargetDbContext>();
    EnsureDatabaseExists(targetConnectionString, scope.ServiceProvider.GetRequiredService<ILoggerFactory>());
    db.Database.Migrate();
}

app.Run();

static void EnsureDatabaseExists(string connectionString, ILoggerFactory loggerFactory)
{
    var logger = loggerFactory.CreateLogger("EtlService.Api");
    var csb = new SqlConnectionStringBuilder(connectionString);
    var dbName = csb.InitialCatalog;

    if (string.IsNullOrWhiteSpace(dbName))
    {
        return;
    }

    var masterCsb = new SqlConnectionStringBuilder(connectionString)
    {
        InitialCatalog = "master"
    };

    for (var attempt = 1; attempt <= 30; attempt++)
    {
        try
        {
            using var conn = new SqlConnection(masterCsb.ConnectionString);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = $"IF DB_ID(N'{dbName.Replace("'", "''")}') IS NULL CREATE DATABASE [{dbName}]";
            cmd.ExecuteNonQuery();

            logger.LogInformation("Banco {DbName} disponivel.", dbName);
            return;
        }
        catch (Exception ex)
        {
            logger.LogInformation("Tentativa {Attempt}/30 ao SQL (master) falhou: {Msg}", attempt, ex.Message);
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }
    }

    throw new Exception("Nao foi possivel conectar ao SQL Server para criar o DB apos 30 tentativas.");
}
