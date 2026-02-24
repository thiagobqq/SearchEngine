using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.Domain.DTO;

namespace WebCrawler.Domain.Interfaces.Services
{
    public interface IPageService
    {
        Task<List<PageListDTO>> GetAllPages();
        Task<PageDTO> GetPageById(int id);
    }
}