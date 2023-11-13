using Dapper;
using System.Data;

namespace ContentCore.Modules.Tags.Repositories
{
    internal class TagRepository
    {
        private readonly IDbConnection _connection;

        public TagRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task InsertTagIfNotExistsAsync(string name)
        {
            var sql = @"
                IF NOT EXISTS (SELECT 1 FROM Tags WHERE Name = @name)
                INSERT INTO Tags (Name) VALUES (@name)";
            var parameters = new { name };
            await _connection.ExecuteAsync(sql, parameters).ConfigureAwait(false);
        }
    }
}
