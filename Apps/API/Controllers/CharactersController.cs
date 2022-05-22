using Database.DTOs;
using Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Users.Attributes;
using Users.Constants;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharactersController : Controller
    {
        private readonly ICharacterRepository _characterRepository;

        public CharactersController(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        [HttpPost]
        [AuthorizeRoles()]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CharacterDetails))]
        public IActionResult Create([FromBody] CharacterSaveData characterSaveData)
        {
            var summary = _characterRepository.Create(characterSaveData);
            return CreatedAtAction(nameof(Get), new { id = summary.Id }, summary);
        }

        [HttpDelete("{id}")]
        [AuthorizeRoles()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            _characterRepository.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [AuthorizeRoles()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Edit(int id, [FromBody] CharacterSaveData characterSaveData)
        {
            _characterRepository.Update(id, characterSaveData);
            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CharacterDetails))]
        public IActionResult Get(int id)
        {
            var character = _characterRepository.Fetch(id);
            if (character == null)
            {
                return NotFound();
            }
            return Json(character);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SearchResults<CharacterSummary>))]
        public IActionResult List([FromQuery] SearchParameters parameters)
        {
            var searchResults = _characterRepository.Search(parameters);
            return Json(searchResults);
        }

    }
}
