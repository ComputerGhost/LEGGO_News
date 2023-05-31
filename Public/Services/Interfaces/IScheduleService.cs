using APIClient.DTOs;
using Calendar.Matrix;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Public.Services.Interfaces
{
    public interface IScheduleService
    {
        Task<IEnumerable<CalendarSummary>> GetCalendarsAsync();

        Task<MonthMatrix> GetMonthMatrixAsync(IEnumerable<CalendarSummary> dbCalendars, DateTime? monthKst);
    }
}
