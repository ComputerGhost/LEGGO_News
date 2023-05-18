using APIClient.Connections;
using APIClient.Connections.Http;

namespace CMS.Setup
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
        }
    }
}
