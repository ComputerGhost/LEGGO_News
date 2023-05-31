using DataAccess.DTOs;

namespace DataAccess.Repositories
{
    internal interface ITagsRepository
    {
        Task<int> CreateAsync(TagSaveData tag);

        Task DeleteAsync(int id);

        Task<TagDetails> GetAsync(int id);

        Task<IList<TagSummary>> SearchAsync(string? search, string? cursor, int limit);

        Task UpdateAsync(int id, TagSaveData data);
    }
}
