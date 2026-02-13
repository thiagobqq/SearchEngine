using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebCrawler.Domain.DTO;
using WebCrawler.Domain.Interfaces.Repositories;
using WebCrawler.Domain.Models;
using WebCrawler.Infra.Data;

namespace WebCrawler.Infra.Repositories
{
    public class PageRepository : IPageRepository
    {
        private readonly WebCrawlerDbContext _context;
        public PageRepository(WebCrawlerDbContext context) => _context = context;
        
        public Task SavePageAsync(PageDTO page)
        {
            var pageEntity = new Page
            {
                Url = page.Url,
                Title = page.Title,
                Content = page.Content
            };

            _context.Pages.Add(pageEntity);
            return _context.SaveChangesAsync();
        }

        public async Task<bool> IsPageAlreadyVisited(string url)
        {
            return await _context.Pages.AnyAsync(p => p.Url == url);
        }
        
    }
}