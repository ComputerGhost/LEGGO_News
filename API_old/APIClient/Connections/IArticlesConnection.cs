using APIClient.DTOs;

namespace APIClient.Connections
{
    public interface IArticlesConnection : IConnection<ArticleDetails, ArticleSaveData, ArticleSummary>
    {
    }
}
