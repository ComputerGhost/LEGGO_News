using APIClient.Connections;
using APIClient.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class ArticlesController : Controller
    {
        private IArticlesConnection _articlesConnection;

        public ArticlesController(IArticlesConnection articlesConnection)
        {
            _articlesConnection = articlesConnection;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            var query = new SearchParameters();
            var results = await _articlesConnection.ListAsync(query);
            return View(results);
        }
    }
}
