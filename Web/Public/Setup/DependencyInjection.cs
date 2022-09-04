using Microsoft.Extensions.DependencyInjection;
using Public.Services;
using Public.Services.Interfaces;

namespace Public.Setup
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services
                .AddTransient<IArticleService, ArticleService>()
                .AddTransient<IScheduleService, ScheduleService>()
            ;
        }
    }
}
