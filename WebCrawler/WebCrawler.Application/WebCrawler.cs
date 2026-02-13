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
            SPIDER_MANAGER.EnqueueUrl("https://html-agility-pack.net/");
            SPIDER_MANAGER.EnqueueUrl("https://github.com/thiagobqq/chat-irl");
            SPIDER_MANAGER.EnqueueUrl("https://www.nuget.org/packages/HtmlAgilityPack/");
            SPIDER_MANAGER.EnqueueUrl("https://www.google.com/");
            SPIDER_MANAGER.EnqueueUrl("https://pt.wikipedia.org/wiki/L%C3%A1zaro_Barbosa_de_Sousa");
        }
    }
}