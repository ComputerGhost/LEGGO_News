using Calendar;
using Calendar.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("[controller]")]
    public class EventsController : Controller
    {
        private readonly ICalendarService _calendarService;

        public EventsController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EventInfo>))]
        public async Task<IActionResult> List([FromQuery] IEnumerable<string> calendarIds, [FromQuery] SearchParameters parameters)
        {
            var selectedCalendars = calendarIds.Any()
                ? _calendarService.GetCalendarsFromIds(calendarIds)
                : _calendarService.ListCalendars();
            var results = await _calendarService.ListEventsAsync(selectedCalendars, parameters);
            return Json(results);
        }

    }
}
