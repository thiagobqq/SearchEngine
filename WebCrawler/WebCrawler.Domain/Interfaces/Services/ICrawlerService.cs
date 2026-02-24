using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCrawler.Domain.Interfaces.Services
{
    public interface ICrawlerService
    {
        void EnqueueUrl(string url);
        void Pause(bool pause);
        Task<List<string>> GetQueue();
        
    }
}