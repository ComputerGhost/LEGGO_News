using APIServer.Database.Repositories;
using APIServer.Database.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace APIServer.Setup
{
    internal static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services
                .AddTransient<IArticleRepository, ArticleRepository>()
                .AddTransient<ICalendarRepository, CalendarRepository>()
                .AddTransient<ICharacterRepository, CharacterRepository>()
                .AddTransient<IMediaRepository, MediaRepository>()
                .AddTransient<ITagRepository, TagRepository>()
            ;
        }
    }
}
