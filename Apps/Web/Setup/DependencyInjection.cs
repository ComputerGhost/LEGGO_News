using Microsoft.Extensions.DependencyInjection;
using Web.Services;
using Web.Services.Interfaces;

namespace Web.Setup
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
