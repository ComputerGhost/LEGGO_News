using APIClient.Connections;
using APIClient.DTOs;
using AutoMapper;
using Calendar;
using Calendar.Interfaces;
using Calendar.Matrix;
using Calendar.Models;
using Public.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Public.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly ICalendarsConnection _calendarsConnection;
        private readonly IEventsService _eventsService;
        private readonly IMapper _mapper;

        public ScheduleService(
            ICalendarsConnection calendarsConnection,
            IEventsService eventsService,
            IMapper mapper)
        {
            _calendarsConnection = calendarsConnection;
            _eventsService = eventsService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CalendarSummary>> GetCalendarsAsync()
        {
            var results = await _calendarsConnection.ListAsync(new SearchParameters());
            return results.Data;
        }

        public async Task<MonthMatrix> GetMonthMatrixAsync(IEnumerable<CalendarSummary> dbCalendars, DateTime? monthKst)
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
