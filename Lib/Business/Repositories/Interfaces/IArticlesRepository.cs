using Business.DTOs;

namespace Business.Repositories.Interfaces
{
    public interface IArticlesRepository
    {
        ArticleSummary Create(ArticleSaveData saveData);
        ArticleDetails Fetch(long id);
        SearchResults<ArticleSummary> Search(SearchParameters parameters);
    }
}
