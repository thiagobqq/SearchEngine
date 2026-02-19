using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.Domain.Interfaces.Repositories;

namespace WebCrawler.Application.Manager
{
    public class SpiderManager
    {
        private readonly Queue<string> _urlQueue;

        public SpiderManager()
        {
            _urlQueue = new Queue<string>();
        }

        public void EnqueueUrl(string url, bool isVisited)
        {
            if (isVisited)
            {   
                Console.WriteLine($"Pagina já visitada: {url}");    
                return;           
            }
            _urlQueue.Enqueue(url);            
        }

        public void EnqueueUrl(string url)
        {
           this.EnqueueUrl(url, false);
        }


        public string? DequeueUrl()
        {
            return _urlQueue.TryDequeue(out var url) ? url : null;
        }

        public string? PeekUrl()
        {
            return _urlQueue.TryPeek(out var url) ? url : null;
        }

        public bool HasUrlsToProcess()
        {
            return _urlQueue.Count > 0;
        }

        public int GetQueueCount()
        {
            return _urlQueue.Count;
        }

        public string ListUrls()
        {
            return string.Join(Environment.NewLine, _urlQueue);
        }


        
    }
}