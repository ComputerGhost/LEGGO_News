using Calendar;
using Database.DTOs;
using Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleRepository _articlesRepository;
        private readonly ICalendarService _calendarService;

        public HomeController(
            IArticleRepository articlesRepository,
            ICalendarService calendarService)
        {
            _articlesRepository = articlesRepository;
            _calendarService = calendarService;
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

        public async Task<IActionResult> Schedule()
        {
            var calendars = _calendarService.ListCalendars();

            var search = new Calendar.DTOs.SearchParameters();
            var events = await _calendarService.ListEventsAsync(search);

            return View(new ScheduleViewModel
            {
                Calendars = calendars,
                Events = events,
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
