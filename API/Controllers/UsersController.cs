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
    public class UsersController : Controller
    {

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(SearchResults<UserSummary>))]
        public IActionResult List([FromQuery] SearchParameters parameters)
        {
            return Json(new SearchResults<UserSummary>());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UserDetails))]
        public IActionResult Get(int id)
        {
            return Json(new CharacterDetails());
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public IActionResult Create([FromBody] UserSaveData character)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public IActionResult Edit(int id, [FromBody] UserSaveData character)
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
