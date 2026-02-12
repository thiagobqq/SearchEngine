using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCrawler.Domain.Manager.Crawler
{
    public class ScrapperManager
    {
        public DownloadManager DownloadManager { get; }    
        public SerializerManager SerializerManager { get; } 

        public ScrapperManager()
        {
            SerializerManager = new SerializerManager();
            DownloadManager = new DownloadManager();
        }

        public void AddUrlToScrape(string url)
        {
            WebCrawler.SPIDER_MANAGER.EnqueueUrl(url);
        }
        
    }
}