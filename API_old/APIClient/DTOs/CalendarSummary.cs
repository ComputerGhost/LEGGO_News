namespace APIClient.DTOs
{
    public class CalendarSummary
    {
        public long Id { get; set; }

        public string Color { get; set; } = string.Empty;

        public string GoogleId { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public TimeSpan TimezoneOffset { get; set; }
    }
}
