using ContentCore.Articles;
using ContentCore.Articles.Models;
using ContentCore.Exceptions;

namespace ContentCore.ArticlesCore.tbd
{
    public class ArticleService
    {
        private readonly ArticleRepository _articleRepository;

        public readonly ArticleEvents Events = new();

        internal ArticleService(ArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<int> CreateArticleAsync(IArticleModel saveData)
        {
            var articleId = await _articleRepository.InsertArticleAsync().ConfigureAwait(false);
            await Events.RaiseArticleCreatingAsync(this, articleId, saveData).ConfigureAwait(false);
            return articleId;
        }

        public async Task<IArticleModel> FetchArticleAsync<ModelType>(
            int articleId)
            where ModelType : IArticleModel, new()
        {
            if (!await _articleRepository.DoesArticleExistAsync(articleId).ConfigureAwait(false))
            {
                throw new ItemNotFoundException();
            }

            var fetchers = new List<FetchQuery>();
            Events.RaiseBuildingFetch(this, fetchers);

            return await _articleRepository.FetchArticleAsync<ModelType>(articleId, fetchers).ConfigureAwait(false);
        }

        public async Task ReplaceArticleAsync(IArticleModel saveData)
        {
            if (saveData.ArticleId == null)
            {
                throw new ArgumentNullException(nameof(saveData.ArticleId));
            }

            if (!await _articleRepository.DoesArticleExistAsync(saveData.ArticleId.Value).ConfigureAwait(false))
            {
                throw new ItemNotFoundException();
            }

            await Events.RaiseArticleReplacingAsync(this, saveData).ConfigureAwait(false);
        }
    }
}
