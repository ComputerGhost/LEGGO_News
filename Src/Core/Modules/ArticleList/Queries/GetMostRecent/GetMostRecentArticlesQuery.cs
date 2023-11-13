using Core.Common.Paging;
using Core.Modules.ArticleList.Models;
using MediatR;

namespace Core.Modules.ArticleList.Queries.GetMostRecent
{
    public class GetMostRecentArticlesQuery : IRequest<PagedArticles>
    {
        public int Count { get; set; } = 10;

        public Cursor<int>? Next { get; set; } = null;
    }
}
