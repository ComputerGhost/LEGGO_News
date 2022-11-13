using APIClient.DTOs;
using APIServer.Attributes;
using APIServer.Constants;
using APIServer.Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController : Controller
    {
        private readonly IArticleRepository _articleRepository;

        public ArticlesController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        [HttpPost]
        [AuthorizeRoles(Roles.Journalist)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ArticleSummary))]
        public IActionResult Create([FromBody] ArticleSaveData articleSaveData)
        {
            var summary = _articleRepository.Create(articleSaveData);
            return CreatedAtAction(nameof(Fetch), new { id = summary.Id }, summary);
        }

        [HttpDelete("{id}")]
        [AuthorizeRoles(Roles.Editor)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(long id)
        {
            _articleRepository.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [AuthorizeRoles(Roles.Journalist, Roles.Editor)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Update(long id, [FromBody] ArticleSaveData articleSaveData)
        {
            _articleRepository.Update(id, articleSaveData);
            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleDetails))]
        public IActionResult Fetch(long id)
        {
            var article = _articleRepository.Fetch(id);
            if (article == null)
            {
                return NotFound();
            }
            return Json(article);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SearchResults<ArticleSummary>))]
        public IActionResult List([FromQuery] SearchParameters parameters)
        {
            var searchResults = _articleRepository.Search(parameters);
            return Json(searchResults);
        }

    }
}
