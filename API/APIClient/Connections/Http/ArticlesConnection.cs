using APIClient.DTOs;

namespace APIClient.Connections.Http
{
    public class ArticlesConnection : Connection<ArticleDetails, ArticleSaveData, ArticleSummary>, IArticlesConnection
    {
        public ArticlesConnection(HttpClient httpClient) :
            base(httpClient, new Uri("articles/", UriKind.Relative))
        {
        }
    }
}
