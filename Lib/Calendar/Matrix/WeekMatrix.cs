using Calendar.Models;

namespace Calendar.Matrix
{
    public class WeekMatrix
    {
        public DateTimeOffset WeekStart { get; }
        public DateTimeOffset WeekEnd { get; set; }
        public IReadOnlyList<DayMatrix> Days { get; }

        public WeekMatrix(DateTimeOffset dayInWeek, DayOfWeek firstDayOfWeek)
        {
            WeekStart = TimeTools.WeekStart(dayInWeek, firstDayOfWeek);
            WeekEnd = TimeTools.WeekEnd(dayInWeek, firstDayOfWeek);
            Days = Enumerable.Range(0, 7)
                .Select(i => new DayMatrix(WeekStart.AddDays(i)))
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