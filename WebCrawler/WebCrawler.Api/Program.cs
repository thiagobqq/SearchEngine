using WebCrawler.Domain.Worker;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<QueueConsumer>();

var app = builder.Build();

app.Run();
