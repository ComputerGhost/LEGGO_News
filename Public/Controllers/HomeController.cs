using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Web;
using Public.Services.Interfaces;
using Public.ViewModels;

namespace Public.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;

        public HomeController(
            IArticleService articleService)
        {
            _articleService = articleService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var parameters = new SearchParameters();
            var articles = await _articleService.SearchAsync(parameters);
            return View(articles);
        }

        [HttpGet("{id}/{friendly?}")]
        public async Task<IActionResult> ArticleAsync(long id, string friendly)
        {
            var article = await _articleService.GetArticleAsync(id);

            if (friendly != article.FriendlyUrlSegment)
            {
                var encodedFriendly = HttpUtility.UrlEncode(article.FriendlyUrlSegment);
                Response.Redirect(Url.Action(nameof(ArticleAsync), new
                {
                    id,
                    friendly = article.FriendlyUrlSegment,
                }));
            }

            _articleService.TranslateToHtml(article);

            return View(article);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
