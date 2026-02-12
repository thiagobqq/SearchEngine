using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebCrawler.Domain.Worker
{
    public class QueueConsumer : BackgroundService
    {
        private readonly ILogger<QueueConsumer> _logger;

        public QueueConsumer(ILogger<QueueConsumer> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("QueueConsumer iniciado");

            while (!stoppingToken.IsCancellationRequested)
            {
                if (WebCrawler.SPIDER_MANAGER.HasUrlsToProcess())
                {
                    var url = WebCrawler.SPIDER_MANAGER.DequeueUrl();
                    
                    if (url != null)
                    {
                        _logger.LogInformation("Processing URL: {Url}", url);
                        WebCrawler.SPIDER_MANAGER.MarkUrlAsVisited(url);
                        
                        WebCrawler.SCRAPPER_MANAGER.DownloadManager.DownloadPageAsync(url).Wait();
                    }
                }

                await Task.Delay(500, stoppingToken);
            }

            _logger.LogInformation("QueueConsumer parado");
        }
    }
}