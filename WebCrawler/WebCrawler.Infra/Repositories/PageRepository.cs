using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
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
        
        public async Task SavePageAsync(PageDTO page)
        {
            if (await _context.Pages.AnyAsync(p => p.Url == page.Url))
                return;

            var pageEntity = new Page
            {
                Url = page.Url,
                Title = page.Title,
                Content = page.Content
            };

            _context.Pages.Add(pageEntity);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))                
                    return;               
                throw;
            }
        }

        public async Task<bool> IsPageAlreadyVisited(string url)
        {
            return await _context.Pages.AnyAsync(p => p.Url == url);
        }
        
    }
}