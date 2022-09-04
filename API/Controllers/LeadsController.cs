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
    public class LeadsController : Controller
    {
        private readonly ILeadRepository _leadRepository;

        public LeadsController(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }

        [HttpPost]
        [AuthorizeRoles(Roles.Informant)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Create([FromBody] LeadSaveData leadSaveData)
        {
            var summary = _leadRepository.Create(leadSaveData);
            return CreatedAtAction(nameof(Get), new { id = summary.Id }, summary);
        }

        [HttpDelete("{id}")]
        [AuthorizeRoles()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            _leadRepository.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [AuthorizeRoles(Roles.Journalist)]
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
