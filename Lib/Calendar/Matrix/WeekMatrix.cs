using Calendar.Models;

namespace Calendar.Matrix
{
    public class WeekMatrix
    {
        public DateTimeOffset WeekStart { get; }
        public DateTimeOffset WeekEnd { get; set; }
        public IReadOnlyList<DayMatrix> Days { get; }

        public WeekMatrix(DateTimeOffset weekStart, DayOfWeek firstDayOfWeek)
        {
            WeekStart = TimeTools.WeekStart(weekStart, firstDayOfWeek);
            WeekEnd = TimeTools.WeekEnd(weekStart, firstDayOfWeek);
            Days = Enumerable.Range(0, 7)
                .Select(i => new DayMatrix(weekStart.AddDays(i)))
                .ToList();
        }

        public void AddEvent(EventInfo eventInfo)
        {
            if (eventInfo.Start < WeekStart || eventInfo.Start > WeekEnd)
            {
                throw new ArgumentOutOfRangeException(nameof(eventInfo), "Start date is outside of the week.");
            }
            AddEventTrustingly(eventInfo);
        }

        internal void AddEventTrustingly(EventInfo eventInfo)
        {
            var index = (eventInfo.Start - WeekStart).Days;
            Days[index].AddEvent(eventInfo);
        }
    }
}