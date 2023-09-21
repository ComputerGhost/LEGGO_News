using DataAccess.Models;
using DataAccess.Repositories;
using DataAccess.Repositories.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagsController : Controller
    {
        private readonly TagsRepository _tagsRepository;

        public TagsController(TagsRepository tagsRepository)
        {
            _tagsRepository = tagsRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResults<TagSummary>))]
        public async Task<IActionResult> IndexAsync(
            [FromQuery] string? search,
            [FromQuery] string? cursor,
            int limit = 10)
        {
            var results = await _tagsRepository.SearchAsync(search, cursor, limit)
                .ConfigureAwait(false);
            return Ok(results);
        }
    }
}
