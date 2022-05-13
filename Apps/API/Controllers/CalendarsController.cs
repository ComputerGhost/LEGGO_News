using Calendar;
using Calendar.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("[controller]")]
    public class CalendarsController : Controller
    {
        private readonly ICalendarService _calendarService;

        public CalendarsController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CalendarInfo>))]
        public IActionResult List()
        {
            var results = _calendarService.ListCalendars();
            return Json(results);
        }
    }
}
