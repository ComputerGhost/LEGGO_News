using APIClient.DTOs;
using APIServer.Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Users.Attributes;
using Users.Constants;

namespace APIServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarsController : Controller
    {
        private readonly ICalendarRepository _calendarRepository;

        public CalendarsController(ICalendarRepository calendarRepository)
        {
            _calendarRepository = calendarRepository;
        }

        [HttpPost]
        [AuthorizeRoles()]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CalendarSummary))]
        public IActionResult Create([FromBody] CalendarSaveData calendarSaveData)
        {
            var summary = _calendarRepository.Create(calendarSaveData);
            return CreatedAtAction(nameof(Fetch), new { id = summary.Id }, summary);
        }

        [HttpDelete("{id}")]
        [AuthorizeRoles()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            _calendarRepository.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [AuthorizeRoles(Roles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Update(int id, [FromBody] CalendarSaveData calendarSaveData)
        {
            _calendarRepository.Update(id, calendarSaveData);
            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CalendarDetails))]
        public IActionResult Fetch(int id)
        {
            var calendar = _calendarRepository.Fetch(id);
            if (calendar == null)
            {
                return NotFound();
            }
            return Json(calendar);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SearchResults<CalendarSummary>))]
        public IActionResult List([FromQuery] SearchParameters parameters)
        {
            var results = _calendarRepository.Search(parameters);
            return Json(results);
        }
    }
}
