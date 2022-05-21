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

            var firstWeekStart = TimeTools.WeekStart(MonthStart);
            var lastWeekEnd = TimeTools.WeekEnd(MonthEnd);
            var weekCount = (lastWeekEnd - firstWeekStart).Days / 7 + 1;

            var weeks = new List<WeekMatrix>();
            for (var i = 0; i < weekCount; i++)
            {
                var dayInWeek = MonthStart.AddDays(i * 7);
                weeks.Add(new WeekMatrix(dayInWeek, firstDayOfWeek));
            }
            Weeks = weeks;
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