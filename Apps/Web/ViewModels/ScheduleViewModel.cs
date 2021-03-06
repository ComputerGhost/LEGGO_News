using Calendar.Matrix;
using Database.DTOs;
using System.Collections.Generic;

namespace Web.ViewModels
{
    public struct ScheduleViewModel
    {
        public IEnumerable<CalendarSummary> Calendars { get; set; }
        public MonthMatrix Matrix {get;set;}
    }
}
