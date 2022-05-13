using Database.DTOs;

namespace Database.Repositories.Interfaces
{
    public interface IArticleRepository
    {
        ArticleSummary Create(ArticleSaveData saveData);
        void Delete(long id);
        ArticleDetails Fetch(long id);
        SearchResults<ArticleSummary> Search(SearchParameters parameters);
        void Update(long id, ArticleSaveData saveData);
    }
}
