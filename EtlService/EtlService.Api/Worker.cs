using EasyNetQ;
using EtlService.Application.Interfaces;
using EtlService.Domain.ValueObjects;

namespace EtlService.Worker;

public class Worker : BackgroundService
{
    private readonly IBus _bus;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<Worker> _logger;

    public Worker(IBus bus, IServiceScopeFactory scopeFactory, ILogger<Worker> logger)
    {
        _bus = bus;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("ETL Worker iniciado. Aguardando mensagens...");

        await _bus.PubSub.SubscribeAsync<PageMessage>("etl", async (message, _) =>
        {
            using var scope = _scopeFactory.CreateScope();
            var indexingService = scope.ServiceProvider.GetRequiredService<IIndexingService>();

            try
            {
                var indexed = await indexingService.IndexPageAsync(message, stoppingToken);

                if (indexed)
                {
                    _logger.LogInformation("Página {PageId} indexada: {Url}", message.PageId, message.Url);
                }
                else
                {
                    _logger.LogInformation("Página {PageId} ignorada (já indexada/sem conteúdo): {Url}", message.PageId, message.Url);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao indexar página {PageId}: {Url}", message.PageId, message.Url);
            }
        }, configure: x => x.WithQueueName("etl_pages"));

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}