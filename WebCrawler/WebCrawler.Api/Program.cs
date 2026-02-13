using Microsoft.EntityFrameworkCore;
using WebCrawler.Api;
using WebCrawler.Domain.Interfaces.Repositories;
using WebCrawler.Domain.Worker;
using WebCrawler.Infra.Data;
using WebCrawler.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WebCrawlerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDependencies();
builder.Services.AddHostedService<QueueConsumer>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<WebCrawlerDbContext>();
    db.Database.Migrate();
}

app.Run();
