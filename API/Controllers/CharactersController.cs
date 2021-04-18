using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharactersController : Controller
    {

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(SearchResults<CharacterSummary>))]
        public IActionResult List([FromQuery] SearchParameters parameters)
        {
            return Json(new SearchResults<CharacterSummary>());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(CharacterDetails))]
        public IActionResult Get(int id)
        {
            return Json(new CharacterDetails());
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public IActionResult Create([FromBody] CharacterSaveData character)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public IActionResult Edit(int id, [FromBody] CharacterSaveData character)
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
