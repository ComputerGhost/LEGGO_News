using API.Modules.Articles.DTOs;
using System.Data;
using Dapper;

namespace API.Modules.Articles.Services
{
    public class CreateArticleService
    {
        private readonly IDbConnection _connection;

        public CreateArticleService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> CreateArticleAsync(
            CreateArticleDto creationDto)
        {
            using var transaction = _connection.BeginTransaction();

            var articleId = await InsertArticleAsync(creationDto);

            await InsertNewTagsAsync(creationDto.Tags);
            await AttachTagsAsync(articleId, creationDto.Tags);

            transaction.Commit();
            return articleId;
        }

        private async Task<int> InsertArticleAsync(
            CreateArticleDto creationDto)
        {
            var sql = @"
                INSERT INTO Articles (Title, ContentJson)
                OUTPUT INSERTED.Id
                VALUES (@Title, @ContentJson)";
            return await _connection.QuerySingleAsync<int>(sql, new
            {
                Title = creationDto.Title,
                ContentJson = creationDto.Content,
            }).ConfigureAwait(false);
        }

        private async Task InsertNewTagsAsync(
            IEnumerable<string>? tags)
        {
            if (tags == null)
            {
                return;
            }

            var sql = @"
                IF NOT EXISTS (SELECT 1 FROM Tags WHERE Name = @Name)
                INSERT INTO Tags (Name) VALUES (@Name)";

            foreach (var tag in tags)
            {
                await _connection.ExecuteAsync(sql, new {
                    Name = tag
                }).ConfigureAwait(false);
            }
        }

        private async Task AttachTagsAsync(
            int articleId,
            IEnumerable<string>? tags)
        {
            if (tags == null)
            {
                return;
            }

            var sql = @"
                INSERT INTO ArticleTags (ArticleId, TagId)
                SELECT @ArticleId, Id
                FROM Tags WHERE Name = @Name";

            foreach (var tag in tags)
            {
                await _connection.ExecuteAsync(sql, new
                {
                    ArticleId = articleId,
                    Name = tag,
                }).ConfigureAwait(false);
            }
        }

    }
}
