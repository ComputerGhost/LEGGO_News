using Calendar.DTOs;
using Calendar.Internal;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using GoogleCalendarService = Google.Apis.Calendar.v3.CalendarService;

namespace Calendar
{
    public class CalendarService : ICalendarService
    {
        private readonly CalendarsRepository _calendarsRepository;
        private readonly EventsRepository _eventsRepository;

        public CalendarService(CalendarConfig config)
        {
            // TODO: Can this be moved to DI, then unit test this service?
            var googleCalendarService = new GoogleCalendarService(new BaseClientService.Initializer
            {
                ApiKey = config.GoogleApiKey,
            });
            _calendarsRepository = new CalendarsRepository(config.Calendars);
            _eventsRepository = new EventsRepository(googleCalendarService);
        }


        public IEnumerable<CalendarInfo> GetCalendarsFromIds(IEnumerable<string> calendarIds)
        {
            return _calendarsRepository.GetCalendarsFromIds(calendarIds);
        }

        public IEnumerable<CalendarInfo> ListCalendars()
        {
            return _calendarsRepository.ListCalendars();
        }

        public Task<IEnumerable<EventInfo>> ListEventsAsync(SearchParameters parameters)
        {
            var calendars = ListCalendars();
            return _eventsRepository.ListEventsAsync(calendars, parameters);
        }

        public Task<IEnumerable<EventInfo>> ListEventsAsync(
            IEnumerable<CalendarInfo> calendars, 
            SearchParameters parameters)
        {
            return _eventsRepository.ListEventsAsync(calendars, parameters);
        }

        public Task<IEnumerable<EventInfo>> ListEventsAsync(
            CalendarInfo calendarInfo,
            SearchParameters parameters)
        {
            return _eventsRepository.ListEventsAsync(calendarInfo, parameters);
        }

        public async Task<CalendarMatrix> BuildCalendarMatrix(DateTimeOffset containedDate)
        {
            var matrix = new CalendarMatrix(containedDate);

            var calendars = _calendarsRepository.ListCalendars();

            var search = new SearchParameters
            {
                Start = matrix.StartDate,
                End = matrix.EndDate,
            };
            var events = await _eventsRepository.ListEventsAsync(calendars, search);

            matrix.AddEvents(events);

            return matrix;
        }

    }
}