using Database.DTOs;
using Public.ViewModels;

namespace Public.Services.Interfaces
{
    public interface IArticleService
    {
        ArticleIndexViewModel Search(SearchParameters parameters);
        ArticleViewModel GetArticle(long id);
        void TranslateToHtml(ArticleViewModel source);
    }
}
