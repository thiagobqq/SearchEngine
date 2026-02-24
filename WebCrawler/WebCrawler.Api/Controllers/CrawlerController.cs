using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCrawler.Domain.DTO;
using WebCrawler.Domain.Interfaces.Repositories;
using WebCrawler.Application;
using WebCrawler.Domain.Interfaces.Services;

namespace WebCrawler.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CrawlerController : ControllerBase
    {
        private readonly ICrawlerService _crawlerService;

        public CrawlerController(ICrawlerService crawlerService) => _crawlerService = crawlerService;

        [HttpPost("enqueue")]
        public async Task<IActionResult> Enqueue([FromBody] string url)
        {
            _crawlerService.EnqueueUrl(url);
            return Accepted();
        }

        [HttpPost("pause")]
        public IActionResult Pause()
        {
            _crawlerService.Pause(true);
            return Ok();
        }

        [HttpPost("resume")]
        public IActionResult Resume()
        {
            _crawlerService.Pause(false);
            return Ok();
        }

        [HttpGet("queue")]
        public async Task<IActionResult> GetQueue()
        {
            var queue = await _crawlerService.GetQueue();
            return Ok(new { count = queue.Count, urls = queue });
        }
    }
}
