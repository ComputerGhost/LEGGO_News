using DataAccess.DTOs;

namespace DataAccess.Repositories
{
    internal interface IArticlesRepository
    {
        Task<int> CreateAsync(ArticleSaveData saveData);

        Task DeleteAsync(int id);

        Task<ArticleDetails> GetAsync(int id);

        Task<IList<ArticleSummary>> SearchAsync(string? search, string? cursor, int limit);

        Task UpdateAsync(int id, ArticleSaveData saveData);
    }
}
