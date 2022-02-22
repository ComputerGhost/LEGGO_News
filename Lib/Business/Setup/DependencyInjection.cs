using Business.Repositories;
using Business.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Setup
{
    public class DependencyInjection
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<IArticleRepository, ArticleRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
        }
    }
}
