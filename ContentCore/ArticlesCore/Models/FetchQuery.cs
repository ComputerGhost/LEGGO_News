using static Dapper.SqlMapper;

namespace ContentCore.Articles.Models
{
    public struct FetchQuery
    {
        /// <summary>
        /// Query using @ArticleId for the article Id parameter.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Parser for the result of executing `Query`.
        /// </summary>
        public Func<IArticleModel, GridReader, Task> Parser { get; set; }
    }
}
