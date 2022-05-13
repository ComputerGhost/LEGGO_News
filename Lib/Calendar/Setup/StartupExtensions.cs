using Calendar;
using Calendar.DTOs;
using Microsoft.Extensions.DependencyInjection;

namespace Database.Setup
{
    public static class StartupExtensions
    {
        public static void AddCalendar(this IServiceCollection services, CalendarConfig config)
        {
            services.AddDependencyInjection(config);
        }


        // Too simple to move to a separate file.
        private static void AddDependencyInjection(this IServiceCollection services, CalendarConfig config)
        {
            services.AddTransient<ICalendarService>(p => new CalendarService(config));
        }
    }
}
