using Calendar.Models;
using System.Diagnostics;

namespace Calendar.Matrix
{
    public class DayMatrix
    {
        public DateTimeOffset Date { get; }

        private List<EventInfo> _events { get; }
        public IReadOnlyList<EventInfo> Events
        {
            get { return _events; }
        }

        internal DayMatrix(DateTimeOffset date)
        {
            Date = date;
            _events = new List<EventInfo>();
        }

        public void AddEvent(EventInfo eventInfo)
        {
            AssertDateIsValid(eventInfo);
            _events.Add(eventInfo);
        }

        [Conditional("DEBUG")]
        private void AssertDateIsValid(EventInfo eventInfo)
        {
            var ourDate = TimeTools.InTimezone(Date, TimeSpan.Zero);
            var eventDate = TimeTools.InTimezone(eventInfo.Start, TimeSpan.Zero);
            Debug.Assert(ourDate.Date == eventDate.Date);
        }
    }
}