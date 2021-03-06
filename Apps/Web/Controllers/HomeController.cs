using Database.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Diagnostics;
using System.Web;
using Web.Services.Interfaces;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;

        public HomeController(
            IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IActionResult Index()
        {
            var parameters = new SearchParameters();
            var articles = _articleService.Search(parameters);
            return View(articles);
        }

        [HttpGet("{id}/{friendly?}")]
        public IActionResult Article(long id, string friendly)
        {
            var article = _articleService.GetArticle(id);

            if (friendly != article.FriendlyUrlSegment)
            {
                var encodedFriendly = HttpUtility.UrlEncode(article.FriendlyUrlSegment);
                Response.Redirect(Url.Action(nameof(Article), new
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
