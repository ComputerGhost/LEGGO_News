using Calendar.Matrix;

namespace Public.ViewModels
{
    public struct ScheduleViewModel
    {
        public IEnumerable<CalendarSummary> Calendars { get; set; }
        public MonthMatrix Matrix {get;set;}
    }
}
