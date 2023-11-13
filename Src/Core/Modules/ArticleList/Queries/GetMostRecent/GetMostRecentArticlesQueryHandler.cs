using Core.Common.Paging;
using Core.Modules.ArticleList.Database;
using Core.Modules.ArticleList.Models;
using MediatR;

namespace Core.Modules.ArticleList.Queries.GetMostRecent
{
    internal class GetMostRecentArticlesQueryHandler : IRequestHandler<GetMostRecentArticlesQuery, PagedArticles>
    {
        IArticleListRepository _repository { get; }

        public GetMostRecentArticlesQueryHandler(IArticleListRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedArticles> Handle(GetMostRecentArticlesQuery request, CancellationToken cancellationToken)
        {
            var firstId = request.Next?.Value;

            var results = await _repository.ListArticlesByMostRecentAsync(
                firstId, request.Count + 1)
                .ConfigureAwait(false);

            Cursor<int>? next = null;
            if (results.Count == request.Count + 1)
            {
                var lastId = results.Last().Id;
                next = Cursor<int>.FromUnencodedValue(lastId);
                results.RemoveAt(request.Count);
            }

            return new PagedArticles
            {
                Next = next,
                Results = results,
            };
        }
    }
}
