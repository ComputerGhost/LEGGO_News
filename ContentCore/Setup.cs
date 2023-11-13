using ContentCore.Articles;
using ContentCore.ArticlesCore.tbd;
using ContentCore.Modules.Tags;
using ContentCore.Modules.Tags.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ContentCore
{
    public static class Setup
    {
        public static void AddContentCore(this IServiceCollection services)
        {
            // Article / Core
            services.AddScoped<ArticleRepository>();
            services.AddScoped<ArticleService>();

            // Tags
            services.AddScoped<ArticleTagRepository>();
            services.AddScoped<TagEventHandlers>();
            services.AddScoped<TagRepository>();
        }
    }
}
