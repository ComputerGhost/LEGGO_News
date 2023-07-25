using DataAccess.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Authorize]
    public class TagsApiController : Controller
    {
        private TagsService _tagsService;

        public TagsApiController(TagsService tagsService)
        {
            _tagsService = tagsService;
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteAsync([FromQuery] int id)
        //{
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditAsync([FromQuery] int id)
        //{
        //}

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
