using DataAccess.DTOs;
using DataAccess.Repositories;
using DataAccess.Utils;
using System.Text;

namespace DataAccess.Services
{
    public class TagsService
    {
        private readonly ITagsRepository _repository;

        internal TagsService(ITagsRepository repository)
        {
            _repository = repository;
        }

        public Task<int> CreateAsync(TagSaveData saveData)
        {
            return _repository.CreateAsync(saveData);
        }

        public async Task<PagedResults<TagSummary>> ListAsync(
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
                nextCursor = CursorUtils.Encode(results[limit].Name);
                results.RemoveAt(limit);
            }

            return new PagedResults<TagSummary>
            {
                Data = results,
                NextCursor = nextCursor,
            };
        }
    }
}
