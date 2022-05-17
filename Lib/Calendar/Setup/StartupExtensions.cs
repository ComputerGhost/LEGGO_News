using Calendar.Interfaces;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Calendar.Setup
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
            services.AddTransient<IEventsService>(p =>
            {
                var initializer = new BaseClientService.Initializer
                {
                    ApiKey = config.GoogleApiKey
                };
                var calendarService = new CalendarService(initializer);
                return new EventsService(calendarService);
            });
        }
    }
}
