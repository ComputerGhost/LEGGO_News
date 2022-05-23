using Database.DTOs;
using Web.ViewModels;

namespace Web.Services.Interfaces
{
    public interface IArticleService
    {
        ArticleIndexViewModel Search(SearchParameters parameters);
        ArticleViewModel GetArticle(long id);
        void TranslateToHtml(ArticleViewModel source);
    }
}
