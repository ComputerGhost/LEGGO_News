using System;

namespace Database.DTOs
{
    public class CalendarSummary
    {
        public long Id { get; set; }
        public string Color { get; set; }
        public string GoogleId { get; set; }
        public string Name { get; set; }
        public TimeSpan TimezoneOffset { get; set; }
    }
}
