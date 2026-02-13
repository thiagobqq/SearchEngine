using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebCrawler.Domain.Interfaces.Repositories;

namespace WebCrawler.Domain.Worker
{
    public class QueueConsumer : BackgroundService
    {
        private readonly ILogger<QueueConsumer> _logger;
        private readonly IPageRepository   _pageRepository;

        public QueueConsumer(ILogger<QueueConsumer> logger, IPageRepository pageRepository)
        {
            _logger = logger;
            _pageRepository = pageRepository;
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
                        var page = await WebCrawler.CRAWLER_MANAGER.ProcessPage(url);
                        WebCrawler.SPIDER_MANAGER.MarkUrlAsVisited(page.Url);
                        await _pageRepository.SavePageAsync(page);                       
                    }
                }

                await Task.Delay(500, stoppingToken);
            }

            _logger.LogInformation("QueueConsumer parado");
        }
    }
}