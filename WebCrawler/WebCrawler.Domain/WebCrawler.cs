using WebCrawler.Domain.Manager;
using WebCrawler.Domain.Manager.Crawler;

namespace WebCrawler.Domain
{
    public static class WebCrawler
    {
        public static SpiderManager SPIDER_MANAGER { get; } = new SpiderManager();
        public static ScrapperManager SCRAPPER_MANAGER { get; } = new ScrapperManager();

        static WebCrawler()
        {
            SPIDER_MANAGER.EnqueueUrl("https://html-agility-pack.net/");
        }
    }
}