using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Public.Services.Interfaces;
using Public.ViewModels;

namespace Public.Controllers
{
    [Route("[Controller]")]
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(
            IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet("{month?}")]
        public async Task<IActionResult> Index(DateTime? month)
        {
            var calendars = await _scheduleService.GetCalendarsAsync();

            var monthMatrix = await _scheduleService.GetMonthMatrixAsync(calendars, month);

            return View(new ScheduleViewModel
            {
                Calendars = calendars,
                Matrix = monthMatrix,
            });
        }
    }
}
