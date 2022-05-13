namespace Calendar.DTOs
{
    public struct CalendarInfo
    {
        public string GoogleId { get; set; }
        public string Color { get; set; }
        public string Name { get; set; }
        public TimeSpan TimezoneOffset { get; set; }
    }
}
