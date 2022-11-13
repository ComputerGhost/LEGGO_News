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
    public class TagsController : Controller
    {
        private readonly ITagRepository _tagsRepository;

        public TagsController(ITagRepository tagRepository)
        {
            _tagsRepository = tagRepository;
        }

        [HttpPost]
        [AuthorizeRoles(Roles.Journalist)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TagSummary))]
        public IActionResult Create([FromBody] TagSaveData tagSaveData)
        {
            var summary = _tagsRepository.Create(tagSaveData);
            return CreatedAtAction(nameof(Fetch), new { id = summary.Id }, summary);
        }

        [HttpDelete]
        [AuthorizeRoles()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            _tagsRepository.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [AuthorizeRoles(Roles.Editor)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Update(int id, [FromBody] TagSaveData tagSaveData)
        {
            _tagsRepository.Update(id, tagSaveData);
            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TagDetails))]
        public IActionResult Fetch(int id)
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
