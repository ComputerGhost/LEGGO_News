using Database.DTOs;
using System.Collections.Generic;

namespace Database.Repositories.Interfaces
{
    public interface ICalendarRepository
    {
        CalendarSummary Create(CalendarSaveData saveData);
        void Delete(long id);
        CalendarDetails Fetch(long id);
        IEnumerable<CalendarSummary> List();
        void Update(long id, CalendarSaveData saveData);
    }
}
