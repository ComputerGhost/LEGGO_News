namespace Calendar.DTOs
{
    public struct CalendarConfig
    {
        public string GoogleApiKey { get; set; }
        public IEnumerable<CalendarInfo> Calendars { get; set; }
    }
}
