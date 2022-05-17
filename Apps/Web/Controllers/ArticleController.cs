using ArticleTranslator;
using Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("[controller]")]
    public class ArticleController : Controller
    {
        private readonly IArticleRepository _articlesRepository;

        public ArticleController(IArticleRepository articlesRepository)
        {
            _articlesRepository = articlesRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Index(long id)
        {
            var article = _articlesRepository.Fetch(id);

            var factory = new ArticleTranslatorFactory();
            var translator = factory.CreateTranslator(article.Format);
            article.Content = translator.TranslateToHtml(article.Content);

            return View(article);
        }
    }
}
