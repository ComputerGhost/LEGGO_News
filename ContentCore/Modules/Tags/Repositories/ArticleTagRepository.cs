using Dapper;
using System.Data;

namespace ContentCore.Modules.Tags.Repositories
{
    internal class ArticleTagRepository
    {
        private readonly IDbConnection _connection;

        public ArticleTagRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task AttachArticleTagAsync(int articleId, string name)
        {
            var sql = @"
                INSERT INTO ArticleTags (ArticleId, TagId)
                SELECT @articleId, Id
                FROM Tags WHERE Name = @name";
            var parameters = new { articleId, name };
            await _connection.ExecuteAsync(sql, parameters).ConfigureAwait(false);
        }

        public async Task DetachArticleTagsAsync(int articleId)
        {
            var sql = "DELETE FROM ArticleTags WHERE ArticleId = @articleId";
            var parameters = new { articleId };
            await _connection.ExecuteAsync(sql, parameters).ConfigureAwait(false);
        }

        public async Task<IEnumerable<string>> FetchArticleTagsAsync(int articleId)
        {
            var sql = @"
                SELECT Tags.Name
                FROM ArticleTags
                JOIN Tags ON Tags.Id = ArticleTags.TagId
                WHERE ArticleTags.ArticleId = @articleId";
            var parameters = new { articleId };
            return await _connection.QueryAsync<string>(sql, parameters).ConfigureAwait(false);
        }
    }
}
