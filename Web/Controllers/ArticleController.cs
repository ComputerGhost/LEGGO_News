using ArticleTranslator;
using Business.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("[controller]")]
    public class ArticleController : Controller
    {
        private readonly IArticlesRepository _articlesRepository;

        public ArticleController(IArticlesRepository articlesRepository)
        {
            _articlesRepository = articlesRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Index(long id)
        {
            var article = _articlesRepository.Fetch(id);

            var translator = ArticleTranslatorFactory.CreateTranslator(article.Format);
            article.Content = translator.TranslateToHtml(article.Content);

            return View(article);
        }
    }
}
