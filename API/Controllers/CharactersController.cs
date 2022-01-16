using API.DTOs;
using AutoMapper;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class CharactersController : Controller
    {
        public readonly DatabaseContext _context;
        public readonly IMapper _mapper;

        public CharactersController(DatabaseContext databaseContext, IMapper mapper)
        {
            _context = databaseContext;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SearchResults<CharacterSummary>))]
        public IActionResult List([FromQuery] SearchParameters parameters)
        {
            var foundCharacters = _context.Character;

            var charactersPage = foundCharacters
                .Skip(parameters.Offset)
                .Take(parameters.Count);

            return Json(new SearchResults<CharacterSummary> {
                Key = parameters.Key,
                Offset = parameters.Offset,
                Count = charactersPage.Count(),
                TotalCount = foundCharacters.Count(),
                Data = charactersPage.Select(character => _mapper.Map<CharacterSummary>(character)),
            });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CharacterDetails))]
        public IActionResult Get(int id)
        {
            var character = _context.Character.Find(id);
            var characterDetails = _mapper.Map<CharacterDetails>(character);
            return Ok(characterDetails);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CharacterDetails))]
        public IActionResult Create([FromBody] CharacterSaveData characterSaveData)
        {
            var character = _mapper.Map<Character>(characterSaveData);
            _context.Character.Add(character);
            _context.SaveChanges();

            var characterDetails = _mapper.Map<CharacterDetails>(character);
            return CreatedAtAction(nameof(Get), new { id = 0 }, characterDetails);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Edit(int id, [FromBody] CharacterSaveData characterSaveData)
        {
            var character = _context.Character.Find(id);
            _mapper.Map(characterSaveData, character);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            var character = _context.Character.Find(id);
            _context.Character.Remove(character);
            return NoContent();
        }

    }
}
