using DataAccess.DTOs;
using DataAccess.Repositories;
using DataAccess.Utils;

namespace DataAccess.Services
{
    public class ArticlesService
    {
        private readonly IArticlesRepository _repository;

        internal ArticlesService(IArticlesRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResults<ArticleSummary>> ListAsync(
            string? search,
            string? cursor,
            int limit)
        {
            var start = CursorUtils.Decode(cursor);

            // requesting an additional item to check for more
            var results = await _repository.SearchAsync(search, start, limit + 1)
                .ConfigureAwait(false);

            string? nextCursor = null;
            if (results.Count > limit)
            {
                nextCursor = CursorUtils.Encode(results[limit].Title);
                results.RemoveAt(limit);
            }

            return new PagedResults<ArticleSummary>
            {
                Data = results,
                NextCursor = nextCursor,
            };
        }
    }
}
