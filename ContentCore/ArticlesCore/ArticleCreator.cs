using ContentCore.ArticlesCore.Models;
using ContentCore.ArticlesCore.Events;
using ContentCore.Events;
using Dapper;

namespace ContentCore.ArticlesCore
{
    public class ArticleCreator
    {
        public event AsyncEventHandler<ArticleCreatingEventArgs>? OnArticleCreating;

        public void AttachModule()
        {
        }

        public async Task<int> CreateArticleAsync(IArticleModel saveData)
        {
            var articleId = await InsertArticleAsync().ConfigureAwait(false);
            await RaiseArticleCreatingAsync(this, articleId, saveData).ConfigureAwait(false);
            return articleId;
        }

        private async Task RaiseArticleCreatingAsync(object sender, int articleId, IArticleModel saveData)
        {
            var eventArgs = new ArticleCreatingEventArgs(articleId, saveData);
            await OnArticleCreating.InvokeAsync(sender, eventArgs);
        }

        private async Task<int> InsertArticleAsync()
        {
            var sql = "INSERT INTO Articles OUTPUT INSERTED.Id";
            return await _connection.QuerySingleAsync<int>(sql).ConfigureAwait(false);
        }
    }
}
