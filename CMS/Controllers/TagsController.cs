using APIClient.Connections;
using APIClient.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class TagsController : Controller
    {
        private ITagsConnection _tagsConnection;

        public TagsController(ITagsConnection tagsConnection)
        {
            _tagsConnection = tagsConnection;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            var query = new SearchParameters();
            var results = await _tagsConnection.ListAsync(query);
            return View(results);
        }
    }
}
