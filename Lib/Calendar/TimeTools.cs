namespace Calendar
{
    public class TimeTools
    {
        /// <summary>
        /// Constructs a DateTimeOffset with the given date and timezone.
        /// </summary>
        public static DateTimeOffset AsTimezone(DateTime source, string timezone)
        {
            var timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezone);
            return AsTimezone(source, timezoneInfo.BaseUtcOffset);
        }

        /// <summary>
        /// Constructs a DateTimeOffset with the given date and timezone offset.
        /// </summary>
        public static DateTimeOffset AsTimezone(DateTime source, TimeSpan offset)
        {
            var convertableSource = DateTime.SpecifyKind(source, DateTimeKind.Unspecified);
            return new DateTimeOffset(convertableSource, offset);
        }

        /// <summary>
        /// Returns the name for the day of the week.
        /// </summary>
        public static string NameOfDay(DateTimeOffset date)
        {
            return NameOfDay(date.DayOfWeek);
        }

        /// <summary>
        /// Returns the name for the day of the week.
        /// </summary>
        public static string NameOfDay(DayOfWeek dayOfWeek)
        {
            return dayOfWeek.ToString();
        }

        /// <summary>
        /// Returns an equal DateTimeOffset but adjusted for a different timezone.
        /// </summary>
        public static DateTimeOffset InTimezone(DateTimeOffset source, string timezone)
        {
            var timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezone);
            return InTimezone(source, timezoneInfo.BaseUtcOffset);
        }

        /// <summary>
        /// Returns an equal DateTimeOffset but adjusted for a different timezone offset.
        /// </summary>
        public static DateTimeOffset InTimezone(DateTimeOffset source, TimeSpan offset)
        {
            return source.ToOffset(offset);
        }

        /// <summary>
        /// Returns the last second of the containing month.
        /// </summary>
        public static DateTimeOffset MonthEnd(DateTimeOffset source)
        {
            return MonthStart(source).AddMonths(1).AddSeconds(-1);
        }

        /// <summary>
        /// Returns the first moment of the containing month.
        /// </summary>
        public static DateTimeOffset MonthStart(DateTimeOffset source)
        {
            return new DateTimeOffset(source.Year, source.Month, 1, 0, 0, 0, source.Offset);
        }

        /// <summary>
        /// Returns the last second of the containing week.
        /// </summary>
        public static DateTimeOffset WeekEnd(DateTimeOffset source, DayOfWeek firstDayOfWeek = DayOfWeek.Monday)
        {
            return WeekStart(source.AddDays(7), firstDayOfWeek).AddSeconds(-1);
        }

        /// <summary>
        /// Returns the first moment of the containing week.
        /// </summary>
        public static DateTimeOffset WeekStart(DateTimeOffset source, DayOfWeek firstDayOfWeek = DayOfWeek.Monday)
        {
            while (source.DayOfWeek != firstDayOfWeek)
            {
                source = source.AddDays(-1);
            }
            return new DateTimeOffset(source.Year, source.Month, source.Day, 0, 0, 0, source.Offset);
        }
    }
}