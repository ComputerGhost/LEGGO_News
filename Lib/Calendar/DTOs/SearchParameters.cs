namespace Calendar.DTOs
{
    public class SearchParameters
    {
        public SearchParameters()
        {
            var todayUtc = DateTimeOffset.UtcNow;
            Start = new DateTime(todayUtc.Year, todayUtc.Month, 1);
            End = Start.AddMonths(1).AddSeconds(-1);
        }

        /// <summary>
        /// Start time in Calendar's default timezone
        /// </summary>
        public DateTimeOffset Start { get; set; }

        /// <summary>
        /// End time in Calendar's default timezone
        /// </summary>
        public DateTimeOffset End { get; set; }
    }
}
