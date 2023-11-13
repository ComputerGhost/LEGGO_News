namespace API.ArticlesCore
{
    public class ArticleEvents
    {
        public event AsyncEventHandler<ArticleCreatingEventArgs>? OnArticleCreating;

        public async Task RaiseArticleCreatingAsync(object sender, int articleId, IArticleModel saveData)
        {
            var eventArgs = new ArticleCreatingEventArgs(articleId, saveData);
            await OnArticleCreating.InvokeAsync(sender, eventArgs);
        }
    }
}
