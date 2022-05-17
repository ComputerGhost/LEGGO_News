using Calendar.Models;

namespace Calendar.Interfaces
{
    public interface ICalendarMatrix
    {
        DateTimeOffset StartDate { get; }
        DateTimeOffset EndDate { get; }
        IReadOnlyList<IList<EventInfo>> Events { get; }

        void AddEvents(IEnumerable<EventInfo> eventInfos);
        void AddEvent(EventInfo eventInfo);
    }
}
