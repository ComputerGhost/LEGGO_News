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
        private readonly IArticlesRepository _articlesRepository;

        public ArticlesController(IArticlesRepository articlesRepository)
        {
            _articlesRepository = articlesRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SearchResults<ArticleSummary>))]
        public IActionResult List([FromQuery] SearchParameters parameters)
        {
            var searchResults = _articlesRepository.Search(parameters);
            return Json(searchResults);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleDetails))]
        public IActionResult Get(int id)
        {
            var article = _articlesRepository.Fetch(id);
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
            var summary = _articlesRepository.Create(articleSaveData);
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
