using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.Domain.Interfaces.Repositories;
using WebCrawler.Domain.Interfaces.Services;

namespace WebCrawler.Application.Services
{
    public class CrawlerService : ICrawlerService
    {
        private readonly IPageRepository _pageRepository;

        public CrawlerService(IPageRepository pageRepository) => _pageRepository = pageRepository;
        public void EnqueueUrl(string url)
        {
            if(string.IsNullOrEmpty(url))
                throw new ArgumentException("url is required");
            if(_pageRepository.IsPageAlreadyVisited(url).Result)
                return;
            
            WebCrawler.SPIDER_MANAGER.EnqueueUrl(url);
        }

        public void Pause(bool pause)
        {
            if (pause)
                WebCrawler.SPIDER_MANAGER.Pause();
            else
                WebCrawler.SPIDER_MANAGER.Resume();
        }

        public async Task<List<string>> GetQueue()
        {
            var list = WebCrawler.SPIDER_MANAGER.ListUrls();
            return string.IsNullOrEmpty(list) ? new List<string>() : new List<string>(list.Split(Environment.NewLine));
        }
        
    }
}