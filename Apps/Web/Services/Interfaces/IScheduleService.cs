using Calendar.Matrix;
using Database.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Services.Interfaces
{
    public interface IScheduleService
    {
        IEnumerable<CalendarSummary> GetCalendars();

        Task<MonthMatrix> GetMonthMatrix(IEnumerable<CalendarSummary> dbCalendars, DateTime? monthKst);
    }
}
