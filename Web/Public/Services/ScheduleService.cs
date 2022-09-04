using AutoMapper;
using Calendar;
using Calendar.Interfaces;
using Calendar.Matrix;
using Calendar.Models;
using Database.DTOs;
using Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Public.Services.Interfaces;

namespace Public.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly IEventsService _eventsService;
        private readonly IMapper _mapper;

        public ScheduleService(
            ICalendarRepository calendarRepository,
            IEventsService eventsService,
            IMapper mapper)
        {
            _calendarRepository = calendarRepository;
            _eventsService = eventsService;
            _mapper = mapper;
        }

        public IEnumerable<CalendarSummary> GetCalendars()
        {
            var results = _calendarRepository.Search(new SearchParameters());
            return results.Data;
        }

        public async Task<MonthMatrix> GetMonthMatrix(IEnumerable<CalendarSummary> dbCalendars, DateTime? monthKst)
        {
            var libCalendars = dbCalendars.Select(c => _mapper.Map<CalendarInfo>(c));

            var when = monthKst.HasValue
                ? TimeTools.AsTimezone(monthKst.Value, "Korea Standard Time")
                : TimeTools.InTimezone(DateTimeOffset.UtcNow, "Korea Standard Time");

            var monthMatrix = new MonthMatrix(when, DayOfWeek.Monday);
            var events = await _eventsService.ListEventsAsync(libCalendars, monthMatrix.MonthStart, monthMatrix.MonthEnd);
            monthMatrix.AddEvents(events);

            return monthMatrix;
        }
    }
}
