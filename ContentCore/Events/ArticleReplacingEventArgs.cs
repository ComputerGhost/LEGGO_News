using ContentCore.Articles.Models;

namespace ContentCore.Events
{
    public class ArticleReplacingEventArgs : EventArgs
    {
        /// <summary>
        /// Unmodified incoming dto.
        /// </summary>
        public IArticleModel SaveData { get; set; } = null!;

        public ArticleReplacingEventArgs(IArticleModel saveData)
        {
            SaveData = saveData;
        }
    }
}
