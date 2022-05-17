using Calendar.Models;

namespace Calendar.Matrix
{
    public class MonthMatrix
    {
        public DateTimeOffset MonthStart { get; set; }
        public DateTimeOffset MonthEnd { get; set; }
        public IReadOnlyList<WeekMatrix> Weeks { get; set; }

        public MonthMatrix(DateTimeOffset dateInMonth, DayOfWeek firstDayOfWeek)
        {
            MonthStart = TimeTools.MonthStart(dateInMonth);
            MonthEnd = TimeTools.MonthEnd(dateInMonth);

            var weekCount = (MonthEnd - MonthStart).Days / 7 + 1;
            Weeks = Enumerable.Range(0, weekCount)
                .Select(i => new WeekMatrix(MonthStart.AddDays(i * 7), firstDayOfWeek))
                .ToList();
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
            if (eventInfo.Start < MonthStart || eventInfo.Start > MonthEnd)
            {
                throw new ArgumentOutOfRangeException(nameof(eventInfo), "Start date is outside of the month.");
            }

            var index = (eventInfo.Start - MonthStart).Days / 7;
            Weeks[index].AddEventTrustingly(eventInfo);
        }
    }
}