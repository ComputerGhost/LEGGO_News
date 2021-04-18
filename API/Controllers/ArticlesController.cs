using API.DTOs;
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

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(SearchResults<ArticleSummary>))]
        public IActionResult List([FromQuery] SearchParameters parameters)
        {
            return Json(new SearchResults<ArticleSummary>());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ArticleDetails))]
        public IActionResult Get(int id)
        {
            return Json(new ArticleDetails());
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public IActionResult Post([FromBody] ArticleSaveData character)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public IActionResult Put(int id, [FromBody] ArticleSaveData character)
        {
            return Ok();
        }

        [HttpPatch]
        [Consumes("application/json-patch+json")]
        [ProducesResponseType(204)]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<ArticleSaveData> values)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public IActionResult Delete(int id)
        {
            return Ok();
        }

    }
}
