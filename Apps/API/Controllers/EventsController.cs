using AutoMapper;
using Calendar;
using Calendar.Interfaces;
using Calendar.Models;
using Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private readonly ICalendarRepository _calendarRepository;
        private readonly IEventsService _eventsService;
        private readonly IMapper _mapper;

        public EventsController(
            ICalendarRepository calendarRepository,
            IEventsService eventsService,
            IMapper mapper)
        {
            _calendarRepository = calendarRepository;
            _eventsService = eventsService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EventInfo>))]
        public async Task<IActionResult> List([FromQuery] DateTimeOffset start, [FromQuery] DateTimeOffset end)
        {
            var dbCalendars = _calendarRepository.List();
            var libCalendars = _mapper.Map<CalendarInfo>(dbCalendars);

            var results = await _eventsService.ListEventsAsync(libCalendars, start, end);

            return Json(results);
        }

    }
}
