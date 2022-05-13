using Calendar.DTOs;
using Google.Apis.Calendar.v3.Data;
using GoogleCalendarService = Google.Apis.Calendar.v3.CalendarService;

namespace Calendar.Internal
{
    public class EventsRepository
    {
        private readonly GoogleCalendarService _googleCalendarService;

        public EventsRepository(GoogleCalendarService googleCalendarService)
        {
            _googleCalendarService = googleCalendarService;
        }


        public async Task<IEnumerable<EventInfo>> ListEventsAsync(
            IEnumerable<CalendarInfo> calendars, 
            SearchParameters parameters)
        {
            var all = new List<EventInfo>();
            foreach (var calendar in calendars)
            {
                all.AddRange(await ListEventsAsync(calendar, parameters));
            }
            return all;
        }

        public async Task<IEnumerable<EventInfo>> ListEventsAsync(
            CalendarInfo calendarInfo,
            SearchParameters parameters)
        {
            var googleEvents = await GetEventsFromGoogleAsync(calendarInfo, parameters);
            var ourEvents = googleEvents.Select(e => ConvertToOurEvent(calendarInfo, e));
            return ourEvents;
        }


        private async Task<IList<Event>> GetEventsFromGoogleAsync(
            CalendarInfo calendarInfo,
            SearchParameters parameters)
        {
            var request = _googleCalendarService.Events.List(calendarInfo.GoogleId);
            request.SingleEvents = true;
            request.TimeMin = ToCalendarTime(calendarInfo, parameters.Start);
            request.TimeMax = ToCalendarTime(calendarInfo, parameters.End);

            var response = await request.ExecuteAsync();

            return response.Items;
        }

        private EventInfo ConvertToOurEvent(CalendarInfo calendarInfo, Event googleEvent)
        {
            var startDate = googleEvent.Start.DateTime ?? DateTime.Parse(googleEvent.Start.Date);
            var endDate = googleEvent.End.DateTime ?? DateTime.Parse(googleEvent.End.Date);
            var isAllDay = googleEvent.Start.DateTime == null;

            return new EventInfo
            {
                Color = calendarInfo.Color,
                End = FromCalendarTime(calendarInfo, endDate),
                EventUri = googleEvent.HtmlLink,
                GoogleCalendarId = calendarInfo.GoogleId,
                IsAllDay = isAllDay,
                Start = FromCalendarTime(calendarInfo, startDate),
                Title = googleEvent.Summary,
            };
        }

        private DateTime ToCalendarTime(CalendarInfo calendarInfo, DateTimeOffset source)
        {
            return source.ToOffset(calendarInfo.TimezoneOffset).DateTime;
        }

        private DateTimeOffset FromCalendarTime(CalendarInfo calendarInfo, DateTime source)
        {
            var convertableSource = DateTime.SpecifyKind(source, DateTimeKind.Unspecified);
            return new DateTimeOffset(convertableSource, calendarInfo.TimezoneOffset);
        }

    }
}