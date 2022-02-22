using AutoMapper;
using Business.DTOs;
using Business.Repositories.Interfaces;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ArticlesController : Controller
    {
        private readonly IArticleRepository _articleRepository;

        public ArticlesController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SearchResults<ArticleSummary>))]
        public IActionResult List([FromQuery] SearchParameters parameters)
        {
            var searchResults = _articleRepository.Search(parameters);
            return Json(searchResults);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleDetails))]
        public IActionResult Get(int id)
        {
            var article = _articleRepository.Fetch(id);
            if (article == null)
            {
                return NotFound();
            }
            return Json(article);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ArticleSummary))]
        public IActionResult Create([FromBody] ArticleSaveData articleSaveData)
        {
            var summary = _articleRepository.Create(articleSaveData);
            return new CreatedResult($"./{summary.Id}", summary);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Edit(int id, [FromBody] ArticleSaveData article)
        {
            return Ok();
        }

        [HttpPatch]
        [Consumes("application/json-patch+json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Edit(int id, [FromBody] JsonPatchDocument<ArticleSaveData> values)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            return Ok();
        }

    }
}
