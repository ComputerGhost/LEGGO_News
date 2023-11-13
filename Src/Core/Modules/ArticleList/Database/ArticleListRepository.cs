using Core.Common.DependencyInjection;
using Core.Modules.ArticleList.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Modules.ArticleList.Database;

[ServiceImplementation(Lifetime = ServiceLifetime.Scoped)]
internal class ArticleListRepository : IArticleListRepository
{
    public Task<IList<ArticleSummary>> ListArticlesByMostRecentAsync(int? firstId, int count)
    {
        throw new NotImplementedException();
    }
}
