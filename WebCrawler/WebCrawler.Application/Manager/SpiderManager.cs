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
        private bool Paused = true;

        public SpiderManager()
        {
            _urlQueue = new Queue<string>();
        }

        public void EnqueueUrl(string url)
        {
            _urlQueue.Enqueue(url);            
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

        public void Pause() => Paused = true;
        public void Resume() => Paused = false;
        public bool IsPaused() => Paused;


        
    }
}