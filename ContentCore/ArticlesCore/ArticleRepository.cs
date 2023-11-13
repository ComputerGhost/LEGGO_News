using ContentCore.Articles.Models;
using Dapper;
using System.Data;
using static Dapper.SqlMapper;

namespace ContentCore.Articles
{
    internal class ArticleRepository
    {
        private readonly IDbConnection _connection;

        public ArticleRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> DoesArticleExistAsync(int articleId)
        {
            var sql = "SELECT 1 FROM Articles WHERE Id = @articleId";
            var parameters = new { articleId };
            return await _connection.QueryFirstOrDefaultAsync<bool>(sql, parameters).ConfigureAwait(false);
        }

        public async Task<IArticleModel> FetchArticleAsync<ModelType>(int articleId)
            where ModelType : IArticleModel
        {
            var sql = "SELECT Id ArticleId FROM Articles WHERE Id = @articleId";
            var parameters = new { articleId };
            return await _connection.QuerySingleOrDefaultAsync<ModelType>(sql, parameters).ConfigureAwait(false);
        }

        public async Task<IArticleModel> FetchArticleAsync<ModelType>(
            int articleId,
            IEnumerable<FetchQuery> fetchers)
            where ModelType : IArticleModel, new()
        {
            var sql = string.Join(";", fetchers.Select(f => f.Query));
            var parameters = new { ArticleId = articleId };
            var results = await _connection.QueryMultipleAsync(sql, parameters).ConfigureAwait(false);

            var article = new ModelType { ArticleId = articleId };
            foreach (var fetcher in fetchers)
            {
                await fetcher.Parser.Invoke(article, results).ConfigureAwait(false);
            }
            return article;
        }

        public async Task<int> InsertArticleAsync()
        {
            var sql = "INSERT INTO Articles OUTPUT INSERTED.Id";
            return await _connection.QuerySingleAsync<int>(sql).ConfigureAwait(false);
        }
    }
}
