using APIClient.DTOs;

namespace APIClient.Connections.Http
{
    public class CalendarsConnection : Connection<CalendarDetails, CalendarSaveData, CalendarSummary>, ICalendarsConnection
    {
        public CalendarsConnection(HttpClient httpClient) :
            base(httpClient, new Uri("calendars/", UriKind.Relative))
        {
        }
    }
}
