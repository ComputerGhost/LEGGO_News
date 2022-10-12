using APIClient.Connections;
using APIClient.Connections.Http;
using Microsoft.Extensions.DependencyInjection;
using Public.Services;
using Public.Services.Interfaces;

namespace Public.Setup
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection services, Config config)
        {
            services.AddHttpClient<IArticlesConnection, ArticlesConnection>(client =>
                client.BaseAddress = config.ApiBaseUri
            );
            services.AddHttpClient<ICalendarsConnection, CalendarsConnection>(client =>
                client.BaseAddress = config.ApiBaseUri
            );
            services.AddHttpClient<ICharactersConnection, CharactersConnection>(client =>
                client.BaseAddress = config.ApiBaseUri
            );
            services.AddHttpClient<ITagsConnection, TagsConnection>(client =>
                client.BaseAddress = config.ApiBaseUri
            );

            services
                .AddTransient<IArticleService, ArticleService>()
                .AddTransient<IScheduleService, ScheduleService>()
            ;
        }
    }
}
