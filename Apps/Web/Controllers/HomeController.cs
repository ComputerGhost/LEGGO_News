using AutoMapper;
using Calendar;
using Calendar.Interfaces;
using Calendar.Matrix;
using Calendar.Models;
using Database.DTOs;
using Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleRepository _articlesRepository;
        private readonly ICalendarRepository _calendarRepository;
        private readonly IEventsService _eventsService;
        private readonly IMapper _mapper;

        public HomeController(
            IArticleRepository articlesRepository,
            ICalendarRepository calendarRepository,
            IEventsService eventsService,
            IMapper mapper)
        {
            _articlesRepository = articlesRepository;
            _calendarRepository = calendarRepository;
            _eventsService = eventsService;
            _mapper = mapper;
        }


        public IActionResult AboutExid()
        {
            return View();
        }

        public IActionResult Index()
        {
            var parameters = new SearchParameters();
            var articles = _articlesRepository.Search(parameters);
            return View(articles);
        }

        public async Task<IActionResult> Schedule(DateTime? month)
        {
            var when = month.HasValue
                ? TimeTools.AsTimezone(month.Value, "Korea Standard Time")
                : TimeTools.InTimezone(DateTimeOffset.UtcNow, "Korea Standard Time");

            var dbCalendars = _calendarRepository.Search(new SearchParameters());
            var libCalendars = dbCalendars.Data.Select(c => _mapper.Map<CalendarInfo>(c));

            var monthMatrix = new MonthMatrix(when, DayOfWeek.Monday);
            var events = await _eventsService.ListEventsAsync(libCalendars, monthMatrix.MonthStart, monthMatrix.MonthEnd);
            monthMatrix.AddEvents(events);

            return View(new ScheduleViewModel
            {
                Calendars = dbCalendars.Data,
                Matrix = monthMatrix,
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
