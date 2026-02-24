using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCrawler.Domain.DTO;
using WebCrawler.Domain.Interfaces.Repositories;
using WebCrawler.Domain.Interfaces.Services;

namespace WebCrawler.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagesController : ControllerBase
    {
        private readonly IPageService _pageService;

        public PagesController(IPageService pageService) => _pageService = pageService;      

        [HttpGet("pages")]
        public async Task<ActionResult<List<PageListDTO>>> GetPages()
        {
            var pages = await _pageService.GetAllPages();
            return Ok(pages);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<PageDTO>> GetPage(long id)
        {
            var page = await _pageService.GetPageById((int)id);
            if (page == null)
                return NotFound();
            return Ok(page);
        }
    }
}
