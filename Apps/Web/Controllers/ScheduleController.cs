using AutoMapper;
using Calendar;
using Calendar.Interfaces;
using Calendar.Matrix;
using Calendar.Models;
using Database.DTOs;
using Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Services;
using Web.Services.Interfaces;
using Web.ViewModels;

namespace Web.Controllers
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
            var calendars = _scheduleService.GetCalendars();

            var monthMatrix = await _scheduleService.GetMonthMatrix(calendars, month);

            return View(new ScheduleViewModel
            {
                Calendars = calendars,
                Matrix = monthMatrix,
            });
        }
    }
}
