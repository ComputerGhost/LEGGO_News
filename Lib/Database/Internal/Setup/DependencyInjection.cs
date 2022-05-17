using Database.Repositories;
using Database.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Database.Internal.Setup
{
    internal static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services
                .AddTransient<IArticleRepository, ArticleRepository>()
                .AddTransient<ICalendarRepository, CalendarRepository>()
                .AddTransient<ICharacterRepository, CharacterRepository>()
                .AddTransient<ILeadRepository, LeadRepository>()
                .AddTransient<IMediaRepository, MediaRepository>()
                .AddTransient<ITagRepository, TagRepository>()
            ;
        }
    }
}
