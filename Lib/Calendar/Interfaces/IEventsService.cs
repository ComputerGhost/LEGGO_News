using Calendar.Models;

namespace Calendar.Interfaces
{
    public interface IEventsService
    {
        Task<IEnumerable<EventInfo>> ListEventsAsync(IEnumerable<CalendarInfo> calendars, DateTimeOffset start, DateTimeOffset end);
        Task<IEnumerable<EventInfo>> ListEventsAsync(CalendarInfo calendarInfo, DateTimeOffset start, DateTimeOffset end);
    }
}