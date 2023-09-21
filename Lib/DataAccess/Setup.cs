using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class Setup
    {
        public static void AddDataAccess(this IServiceCollection services)
        {
            services.AddScoped<ArticlesRepository>();
            services.AddScoped<TagsRepository>();
        }
    }
}
