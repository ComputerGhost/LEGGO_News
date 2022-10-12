using APIClient.DTOs;

namespace APIServer.Database.Repositories.Interfaces
{
    public interface ICalendarRepository
    {
        CalendarSummary Create(CalendarSaveData saveData);
        void Delete(long id);
        CalendarDetails Fetch(long id);
        SearchResults<CalendarSummary> Search(SearchParameters parameters);
        void Update(long id, CalendarSaveData saveData);
    }
}
