using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebCrawler.Domain.Manager.Crawler
{
    public class DownloadManager
    {
        public DownloadManager()
        {
        }

        public async Task<HtmlDocument> DownloadPageAsync(string url)
        {
            var web = new HtmlWeb();
            var htmlDoc = await web.LoadFromWebAsync(url);
            var node = htmlDoc.DocumentNode.SelectSingleNode("//head/title");

            Console.WriteLine("Node Name: " + node.Name + "\n" + node.OuterHtml);
            return htmlDoc;
        }
    }
}