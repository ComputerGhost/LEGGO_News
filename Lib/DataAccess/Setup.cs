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
            services.AddScoped<ArticlesService>(sp => new ArticlesService(sp.GetService<IArticlesRepository>()!));
            services.AddScoped<TagsService>(sp => new TagsService(sp.GetService<ITagsRepository>()!));

            services.AddScoped<IArticlesRepository, ArticlesRepository>();
            services.AddScoped<ITagsRepository, TagsRepository>();
        }
    }
}
