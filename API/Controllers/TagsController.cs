using DataAccess.DTOs;
using DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagsController : Controller
    {
        private readonly TagsService _tagsService;

        public TagsController(TagsService tagsService)
        {
            _tagsService = tagsService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResults<TagSummary>))]
        public async Task<IActionResult> IndexAsync(
            [FromQuery] string? search,
            [FromQuery] string? cursor,
            int limit = 10)
        {
            var results = await _tagsService.ListAsync(search, cursor, limit)
                .ConfigureAwait(false);
            return Ok(results);
        }
    }
}
