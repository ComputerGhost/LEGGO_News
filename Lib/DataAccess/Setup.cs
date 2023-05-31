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
            services.AddScoped<TagsService>(sp => new TagsService(sp.GetService<ITagsRepository>()!));

            services.AddScoped<ITagsRepository, TagsRepository>();
        }
    }
}
