using ContentCore.Articles.Models;
using ContentCore.ArticlesCore.Events;
using ContentCore.Events;
using ContentCore.Models;

namespace ContentCore.ArticlesCore.tbd
{
    public class ArticleEvents
    {
        public event AsyncEventHandler<ArticleCreatingEventArgs>? OnArticleCreating;
        public event AsyncEventHandler<ArticleFetchingEventArgs>? OnArticleFetching;
        public event AsyncEventHandler<ArticleListingEventArgs>? OnArticleSummariesFetching;
        public event AsyncEventHandler<ArticleReplacingEventArgs>? OnArticleReplacing;


        public event EventHandler<BuildingFetchEventArgs>? OnBuildingFetch;

        internal void RaiseBuildingFetch(object sender, IEnumerable<FetchQuery> fetchers)
        {
            var eventArgs = new BuildingFetchEventArgs(fetchers);
            OnBuildingFetch?.Invoke(sender, eventArgs);
        }


        internal async Task RaiseArticleCreatingAsync(object sender, int articleId, IArticleModel saveData)
        {
            var eventArgs = new ArticleCreatingEventArgs(articleId, saveData);
            await OnArticleCreating.InvokeAsync(sender, eventArgs);
        }

        internal async Task RaiseArticleFetchingAsync(object sender, IArticleModel articleModel)
        {
            var eventArgs = new ArticleFetchingEventArgs(articleModel);
            await OnArticleFetching.InvokeAsync(sender, eventArgs);
        }

        internal async Task RaiseArticleListingAsync(object sender, IEnumerable<IArticleSummaryModel> summaryModels)
        {
            var eventArgs = new ArticleListingEventArgs(summaryModels)
        }

        internal async Task RaiseArticleReplacingAsync(object sender, IArticleModel saveData)
        {
            var eventArgs = new ArticleReplacingEventArgs(saveData);
            await OnArticleReplacing.InvokeAsync(sender, eventArgs);
        }
    }
}
