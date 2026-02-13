using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.Domain.DTO;

namespace WebCrawler.Domain.Interfaces.Repositories
{
    public interface IPageRepository
    {
        Task SavePageAsync(PageDTO page);
    }
}