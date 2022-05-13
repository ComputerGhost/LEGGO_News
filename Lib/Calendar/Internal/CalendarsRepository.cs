using Calendar.DTOs;

namespace Calendar.Internal
{
    public class CalendarsRepository
    {
        private readonly IEnumerable<CalendarInfo> _calendars;

        public CalendarsRepository(IEnumerable<CalendarInfo> calendars)
        {
            _calendars = calendars;
        }


        public IEnumerable<CalendarInfo> GetCalendarsFromIds(IEnumerable<string> calendarIds)
        {
            return _calendars
                .Where(cal => calendarIds.Any(id => cal.GoogleId == id))
                .DistinctBy(cal => cal.GoogleId);
        }

        public IEnumerable<CalendarInfo> ListCalendars()
        {
            return _calendars;
        }

    }
}