using DataAccess.DTOs;
using DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController : Controller
    {
        private readonly ArticlesService _articlesService;

        public ArticlesController(ArticlesService articlesService)
        {
            _articlesService = articlesService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResults<ArticleSummary>))]
        public async Task<IActionResult> IndexAsync(
            [FromQuery] string? search,
            [FromQuery] string? cursor,
            int limit = 10)
        {
            var results = await _articlesService.ListAsync(search, cursor, limit)
                .ConfigureAwait(false);
            return Ok(results);
        }
    }
}
