using Core.Modules.ArticleList.Models;

namespace Core.Modules.ArticleList.Database;

internal interface IArticleListRepository
{
    public Task<IList<ArticleSummary>> ListArticlesByMostRecentAsync(
        int? firstId, int count);
}
