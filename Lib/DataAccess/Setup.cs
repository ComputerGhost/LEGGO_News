using DataAccess.Repositories;
using DataAccess.Repositories.Dapper;
using DataAccess.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class Setup
    {
        public static void AddDataAccess(this IServiceCollection services)
        {
            services.AddScoped(sp => new ArticlesService(sp.GetService<Repositories.ArticlesRepository>()!));
            services.AddScoped(sp => new TagsService(sp.GetService<Repositories.TagsRepository>()!));

            services.AddScoped<ArticlesRepository, ArticlesRepository>();
            services.AddScoped<TagsRepository, TagsRepository>();
        }
    }
}
