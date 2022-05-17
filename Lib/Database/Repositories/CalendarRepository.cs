using AutoMapper;
using Database.DTOs;
using Database.Internal.Models;
using Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Database.Repositories
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;

        public CalendarRepository(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public CalendarSummary Create(CalendarSaveData saveData)
        {
            var newCalendar = _mapper.Map<Calendar>(saveData);
            _databaseContext.Calendars.Add(newCalendar);
            _databaseContext.SaveChanges();

            return _mapper.Map<CalendarSummary>(newCalendar);
        }

        public void Delete(long id)
        {
            var calendar = _databaseContext.Calendars.Find(id);
            _databaseContext.Calendars.Remove(calendar);
            _databaseContext.SaveChanges();
        }

        public CalendarDetails Fetch(long id)
        {
            var calendar = _databaseContext.Calendars.Find(id);
            if (calendar == null)
            {
                return null;
            }
            return _mapper.Map<CalendarDetails>(calendar);
        }

        public IEnumerable<CalendarSummary> List()
        {
            var calendars = _databaseContext.Calendars;
            return calendars.Select(calendar => _mapper.Map<CalendarSummary>(calendar));
        }

        public void Update(long id, CalendarSaveData saveData)
        {
            var calendar = _databaseContext.Calendars.Find(id);
            _mapper.Map(saveData, calendar);
            _databaseContext.SaveChanges();
        }
    }
}
