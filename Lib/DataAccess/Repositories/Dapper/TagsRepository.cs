using Dapper;
using DataAccess.DTOs;
using DataAccess.Exceptions;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccess.Repositories.Dapper
{
    public class TagsRepository : ITagsRepository
    {
        private readonly IDbConnection _connection;

        public TagsRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> CreateAsync(TagSaveData saveData)
        {
            var sql = "insert into Tags (Name) output inserted.Id values (@Name)";

            try
            {
                return await _connection.QuerySingleAsync<int>(sql, saveData)
                    .ConfigureAwait(false);
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                throw new ItemAlreadyExistsException();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "delete from Tags where Id = @id";
            var count = await _connection.ExecuteAsync(sql, new { id })
                .ConfigureAwait(false);
            if (count == 0)
            {
                throw new ItemNotFoundException();
            }
        }

        public async Task<TagDetails> GetAsync(int id)
        {
            var sql = "select * from Tags where Id = @id";
            var item = await _connection.QuerySingleOrDefaultAsync<TagDetails?>(sql, id)
                .ConfigureAwait(false);
            return item ?? throw new ItemNotFoundException();
        }

        public async Task<IList<TagSummary>> SearchAsync(
            string? search,
            string? start, 
            int limit)
        {
            var sql = @"
                SELECT TOP (@limit) Id, Name FROM Tags 
                WHERE Name >= @start 
                  AND Name like '%' + @search + '%'
                ORDER BY Name";

            var parameters = new
            {
                search = search ?? "",
                start = start ?? "",
                limit,
            };

            var results = await _connection.QueryAsync<TagSummary>(sql, parameters)
                .ConfigureAwait(false);
            return results.AsList();
        }

        public async Task UpdateAsync(int id, TagSaveData saveData)
        {
            var sql = "update Tags set Name = @Name where Id = @id";

            try
            {
                var count = await _connection.ExecuteAsync(sql, saveData)
                    .ConfigureAwait(false);
                if (count == 0)
                {
                    throw new ItemNotFoundException();
                }
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                throw new ItemAlreadyExistsException();
            }
        }
    }
}
