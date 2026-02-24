using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.Domain.DTO;
using WebCrawler.Domain.Interfaces.Repositories;
using WebCrawler.Domain.Interfaces.Services;

namespace WebCrawler.Application.Services
{
    public class PageService : IPageService
    {

        private readonly IPageRepository _pageRepository;
        public PageService(IPageRepository pageRepository) => _pageRepository = pageRepository;
        
        
        public Task<List<PageListDTO>> GetAllPages()
        {
            var pages = _pageRepository.GetPagesListAsync();
            return pages;
        }

        public Task<PageDTO> GetPageById(int id)
        {
            var page = _pageRepository.GetPageByIdAsync(id);
            return page!;
        
        }
    }
}