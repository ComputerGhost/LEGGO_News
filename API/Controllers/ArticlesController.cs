using DataAccess.Models;
using DataAccess.Repositories;
using DataAccess.Repositories.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController : Controller
    {
        private readonly ArticlesRepository _articlesRepository;

        public ArticlesController(ArticlesRepository articlesRepository)
        {
            _articlesRepository = articlesRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResults<ArticleSummary>))]
        public async Task<IActionResult> IndexAsync(
            [FromQuery] string? search,
            [FromQuery] string? cursor,
            int limit = 10)
        {
            var results = await _articlesRepository.SearchAsync(search, cursor, limit)
                .ConfigureAwait(false);
            return Ok(results);
        }
    }
}
