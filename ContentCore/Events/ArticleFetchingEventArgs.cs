using ContentCore.Articles.Models;
using static Dapper.SqlMapper;

namespace ContentCore.Events
{
    public class ArticleFetchingEventArgs : EventArgs
    {
        public struct FetchQuery
        {
            public string Query { get; set; }
            public Func<GridReader, IArticleModel> Parser { get; set; }
        };

        /// <summary>
        /// Article model being built to return.
        /// </summary>
        public IArticleModel Article { get; set; } = null!;

        public ArticleFetchingEventArgs(IArticleModel article)
        {
            Article = article;
        }
    }
}
