using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using WebCrawler.Domain.DTO;

namespace WebCrawler.Application.Manager
{
    public class CrawlerManager
    {

        public void AddUrlToScrape(string url, bool isVisited)
        {
            WebCrawler.SPIDER_MANAGER.EnqueueUrl(url, isVisited);
        }

        public async Task<PageDTO> ProcessPage(string url)
        {
            var web = new HtmlWeb();
            var htmlDoc = await web.LoadFromWebAsync(url);
            var node = htmlDoc.DocumentNode.SelectSingleNode("//head/title");

            var content = Regex.Replace(htmlDoc.DocumentNode.InnerText.Trim(), @"\s+", " ");
            
            return new PageDTO
            {
                Url = url,
                Title = node.InnerText,
                Content = content
            };
        }
        
    }
}