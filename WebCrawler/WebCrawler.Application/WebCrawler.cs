using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.Application.Manager;

namespace WebCrawler.Application
{
    public static class WebCrawler
    {
        public static SpiderManager SPIDER_MANAGER { get; } = new SpiderManager();
        public static CrawlerManager CRAWLER_MANAGER { get; } = new CrawlerManager();

        static WebCrawler()
        {
        }
    }
}