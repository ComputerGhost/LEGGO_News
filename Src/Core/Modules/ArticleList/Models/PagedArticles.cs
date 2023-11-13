using Core.Common.Paging;

namespace Core.Modules.ArticleList.Models;

public struct PagedArticles
{
    public IEnumerable<ArticleSummary> Results { get; set; }

    public Cursor<int>? Next { get; set; }
}
