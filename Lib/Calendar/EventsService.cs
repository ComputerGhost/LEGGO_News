using Calendar.Models;
using Calendar.Interfaces;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Calendar.v3;

namespace Calendar
{
    public class EventsService : IEventsService
    {
        private readonly CalendarService _googleCalendarService;

        public EventsService(CalendarService googleCalendarService)
        {
            _googleCalendarService = googleCalendarService;
        }


        public async Task<IEnumerable<EventInfo>> ListEventsAsync(
            IEnumerable<CalendarInfo> calendarInfos,
            DateTimeOffset start,
            DateTimeOffset end)
        {
            var all = new List<EventInfo>();
            foreach (var calendarInfo in calendarInfos)
            {
                all.AddRange(await ListEventsAsync(calendarInfo, start, end));
            }
            return all;
        }

        public async Task<IEnumerable<EventInfo>> ListEventsAsync(
            CalendarInfo calendarInfo,
            DateTimeOffset start,
            DateTimeOffset end)
        {
            var googleEvents = await GetEventsFromGoogleAsync(calendarInfo, start, end);
            var ourEvents = googleEvents.Select(e => ConvertToOurEvent(calendarInfo, e));
            return ourEvents;
        }


        private async Task<IList<Event>> GetEventsFromGoogleAsync(
            CalendarInfo calendarInfo,
            DateTimeOffset start,
            DateTimeOffset end)
        {
            var request = _googleCalendarService.Events.List(calendarInfo.GoogleId);
            request.SingleEvents = true;
            request.TimeMin = TimeTools.InTimezone(start, calendarInfo.TimezoneOffset).DateTime;
            request.TimeMax = TimeTools.InTimezone(end, calendarInfo.TimezoneOffset).DateTime;

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
                End = TimeTools.AsTimezone(endDate, calendarInfo.TimezoneOffset),
                EventUri = googleEvent.HtmlLink,
                GoogleCalendarId = calendarInfo.GoogleId,
                IsAllDay = isAllDay,
                Start = TimeTools.AsTimezone(startDate, calendarInfo.TimezoneOffset),
                Title = googleEvent.Summary,
            };
        }

    }
}