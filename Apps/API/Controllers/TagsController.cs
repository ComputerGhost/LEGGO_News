using Business.DTOs;
using Business.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class TagsController : Controller
    {
        private readonly ITagRepository _tagsRepository;

        public TagsController(ITagRepository tagRepository)
        {
            _tagsRepository = tagRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TagSummary))]
        public IActionResult Create([FromBody] TagSaveData tagSaveData)
        {
            var summary = _tagsRepository.Create(tagSaveData);
            return CreatedAtAction(nameof(Get), new { id = summary.Id }, summary);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            _tagsRepository.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Edit(int id, [FromBody] TagSaveData tagSaveData)
        {
            _tagsRepository.Update(id, tagSaveData);
            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TagDetails))]
        public IActionResult Get(int id)
        {
            var tag = _tagsRepository.Fetch(id);
            if (tag == null)
            {
                return NotFound();
            }
            return Json(tag);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SearchResults<TagSummary>))]
        public IActionResult List([FromQuery] SearchParameters parameters)
        {
            var searchResults = _tagsRepository.Search(parameters);
            return Json(searchResults);
        }

    }
}
