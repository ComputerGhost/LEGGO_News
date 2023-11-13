using Dapper;
using System.Data;

namespace API.ArticlesCore
{
    public class ArticleRepository
    {
        private readonly IDbConnection _connection;

        public ArticleRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> InsertArticleAsync()
        {
            var sql = "INSERT INTO Articles OUTPUT INSERTED.Id";
            return await _connection.QuerySingleAsync<int>(sql).ConfigureAwait(false);
        }
    }
}
