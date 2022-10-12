using APIClient.DTOs;
using Public.ViewModels;
using System.Threading.Tasks;

namespace Public.Services.Interfaces
{
    public interface IArticleService
    {
        Task<ArticleIndexViewModel> SearchAsync(SearchParameters parameters);
        Task<ArticleViewModel> GetArticleAsync(long id);
        void TranslateToHtml(ArticleViewModel source);
    }
}
