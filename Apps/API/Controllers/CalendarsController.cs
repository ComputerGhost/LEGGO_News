using Calendar;
using Calendar.Models;
using Database.DTOs;
using Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class CalendarsController : Controller
    {
        private readonly ICalendarRepository _calendarRepository;

        public CalendarsController(ICalendarRepository calendarRepository)
        {
            _calendarRepository = calendarRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CalendarSummary))]
        public IActionResult Create([FromBody] CalendarSaveData calendarSaveData)
        {
            var summary = _calendarRepository.Create(calendarSaveData);
            return CreatedAtAction(nameof(Get), new { id = summary.Id }, summary);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            _calendarRepository.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Edit(int id, [FromBody] CalendarSaveData calendarSaveData)
        {
            _calendarRepository.Update(id, calendarSaveData);
            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CalendarDetails))]
        public IActionResult Get(int id)
        {
            var calendar = _calendarRepository.Fetch(id);
            if (calendar == null)
            {
                return NotFound();
            }
            return Json(calendar);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CalendarInfo>))]
        public IActionResult List()
        {
            var results = _calendarRepository.List();
            return Json(results);
        }
    }
}
