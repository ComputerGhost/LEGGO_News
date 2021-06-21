using API.DTOs;
using AutoMapper;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController : Controller
    {
        public readonly DatabaseContext _context;
        public readonly IMapper _mapper;

        public ArticlesController(DatabaseContext databaseContext, IMapper mapper)
        {
            _context = databaseContext;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SearchResults<ArticleSummary>))]
        public IActionResult List([FromQuery] SearchParameters parameters)
        {
            var foundArticles = _context.Article;

            var articlesPage = foundArticles
                .Skip(parameters.Offset)
                .Take(parameters.Count);

            return Json(new SearchResults<ArticleSummary> {
                Key = parameters.Key,
                Offset = parameters.Offset,
                Count = articlesPage.Count(),
                TotalCount = foundArticles.Count(),
                Data = articlesPage.Select(article => _mapper.Map<ArticleSummary>(article))
            });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleDetails))]
        public IActionResult Get(int id)
        {
            return Json(new ArticleDetails());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Create([FromBody] ArticleSaveData article)
        {
            return Ok();
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
