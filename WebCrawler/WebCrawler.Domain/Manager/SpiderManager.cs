using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCrawler.Domain.Manager
{
    public class SpiderManager
    {
        private Queue<string> _urlQueue;
        private HashSet<string> _visitedUrls;

        public SpiderManager()
        {
            _urlQueue = new Queue<string>();
            _visitedUrls = new HashSet<string>();
        }

        public void EnqueueUrl(string url)
        {
            if (!_visitedUrls.Contains(url))
            {
                _urlQueue.Enqueue(url);
            }
        }

        public void MarkUrlAsVisited(string url)
        {
            _visitedUrls.Add(url);
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


        
    }
}