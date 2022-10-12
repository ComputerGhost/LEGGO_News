using APIClient.DTOs;

namespace APIClient.Connections
{
    public interface ICalendarsConnection : IConnection<CalendarDetails, CalendarSaveData, CalendarSummary>
    {
    }
}
