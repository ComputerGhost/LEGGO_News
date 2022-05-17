using System;

namespace Database.DTOs
{
    public class CalendarSaveData
    {
        public string GoogleId { get; set; }
        public string Color { get; set; }
        public string Name { get; set; }
        public TimeSpan TimezoneOffset { get; set; }
    }
}
