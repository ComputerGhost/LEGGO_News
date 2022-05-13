using Calendar.DTOs;
using Calendar.Internal;

namespace Calendar
{
    public class CalendarMatrix
    {
        public DateTimeOffset StartDate { get; }
        public DateTimeOffset EndDate { get; }
        public IReadOnlyList<IList<EventInfo>> Events { get; }

        public CalendarMatrix(DateTimeOffset containedDate) :
            this(containedDate.Year, containedDate.Month, containedDate.Offset)
        {
        }

        public CalendarMatrix(int year, int month, TimeSpan timezoneOffset)
        {
            var monthStart = new DateTimeOffset(year, month, 0, 0, 0, 0, timezoneOffset);
            StartDate = TimeTools.WeekStart(monthStart);

            var monthEnd = TimeTools.MonthEnd(monthStart);
            EndDate = TimeTools.WeekEnd(monthEnd);

            var dayCount = (EndDate - StartDate).Days;
            Events = Enumerable.Range(0, dayCount).Select(x => new List<EventInfo>()).ToList();
        }

        public void AddEvents(IEnumerable<EventInfo> eventInfos)
        {
            foreach (var eventInfo in eventInfos)
            {
                AddEvent(eventInfo);
            }
        }

        public void AddEvent(EventInfo eventInfo)
        {
            if (eventInfo.Start < StartDate || eventInfo.Start > EndDate)
            {
                throw new ArgumentOutOfRangeException(nameof(eventInfo), "Start date is outside of the calendar date range.");
            }

            var index = (eventInfo.Start - StartDate).Days;
            Events[index].Add(eventInfo);
        }

    }
}