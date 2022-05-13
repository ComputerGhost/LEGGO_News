using Calendar.DTOs;

namespace Calendar
{
    public interface ICalendarService
    {
        /// <summary>
        /// Gets the valid CalendarInfos associated with calendarIds.
        /// </summary>
        /// <remarks>
        /// If some ids are invalid, fewer results will be returned than how many were requested.
        /// </remarks>
        IEnumerable<CalendarInfo> GetCalendarsFromIds(IEnumerable<string> calendarIds);

        IEnumerable<CalendarInfo> ListCalendars();

        /// <summary>
        /// Gets events from all registered calendars.
        /// </summary>
        Task<IEnumerable<EventInfo>> ListEventsAsync(SearchParameters parameters);

        /// <summary>
        /// Gets events from specific calendars.
        /// </summary>
        /// <remarks>
        /// This does not verify that the calendars are valid.
        /// </remarks>
        Task<IEnumerable<EventInfo>>  ListEventsAsync(
            IEnumerable<CalendarInfo> calendars, 
            SearchParameters parameters);

        /// <summary>
        /// Gets events from specific calendar.
        /// </summary>
        /// <remarks>
        /// This does not verify that the calendar is valid.
        /// </remarks>
        Task<IEnumerable<EventInfo>> ListEventsAsync(
            CalendarInfo calendarInfo,
            SearchParameters parameters);
    }
}