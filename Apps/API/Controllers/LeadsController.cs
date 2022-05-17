using Database.DTOs;
using Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class LeadsController : Controller
    {
        private readonly ILeadRepository _leadRepository;

        public LeadsController(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Create([FromBody] LeadSaveData leadSaveData)
        {
            var summary = _leadRepository.Create(leadSaveData);
            return CreatedAtAction(nameof(Get), new { id = summary.Id }, summary);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            _leadRepository.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Edit(int id, [FromBody] LeadSaveData leadSaveData)
        {
            _leadRepository.Update(id, leadSaveData);
            return Ok();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LeadDetails))]
        public IActionResult Get(int id)
        {
            var lead = _leadRepository.Fetch(id);
            if (lead == null)
            {
                return NotFound();
            }
            return Json(lead);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SearchResults<LeadSummary>))]
        public IActionResult List([FromQuery] SearchParameters parameters)
        {
            var searchResults = _leadRepository.Search(parameters);
            return Json(searchResults);
        }

    }
}
