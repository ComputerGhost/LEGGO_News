using DataAccess.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Authorize]
    public class TagsController : Controller
    {
        private TagsService _tagsService;

        public TagsController(TagsService tagsService)
        {
            _tagsService = tagsService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> IndexDataAsync([FromQuery] string search)
        {
            var results = await _tagsService.ListAsync(search, null, 10)
                .ConfigureAwait(false);

            return Ok(results);
        }
    }
}
