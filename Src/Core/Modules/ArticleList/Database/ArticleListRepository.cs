using Core.Common.DependencyInjection;
using Core.Modules.ArticleList.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Modules.ArticleList.Database;

[ServiceImplementation(Lifetime = ServiceLifetime.Scoped)]
internal class ArticleListRepository : IArticleListRepository
{
    public Task<IList<ArticleSummary>> ListArticlesByMostRecentAsync(int? firstId, int count)
    {
        // Mock data
        var resultsList = GenerateMockData(firstId ?? 1, count).ToList();
        return Task.FromResult<IList<ArticleSummary>>(resultsList);
    }

    private IEnumerable<ArticleSummary> GenerateMockData(int firstId, int count)
    {
        int lastId = firstId + count;
        for (var i = firstId; i != lastId; i++)
        {
            yield return new ArticleSummary
            {
                Id = i,
                Title = $"Title Number <i class='test-htmlescape'>{i}</i>",
                Abstract = "This is a short description of the article.  <i class='test-htmlescape'>This is test data.</i>  When the website goes live, this paragraph will be the first paragraph of the article or a custom abstract.  The way I write, the first paragraph would be a good default abstract.",
            };
        }
    }
}
