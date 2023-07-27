﻿using Dapper;
using DataAccess.DTOs;
using DataAccess.Exceptions;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccess.Repositories.Dapper
{
    internal class TagsRepository : ITagsRepository
    {
        private readonly IDbConnection _connection;

        public TagsRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> CreateAsync(TagSaveData saveData)
        {
            var sql = "INSERT INTO Tags (Name) OUTPUT INSERTED.Id VALUES (@Name)";

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
            var sql = "DELETE FROM Tags WHERE Id = @id";
            var count = await _connection.ExecuteAsync(sql, new { id })
                .ConfigureAwait(false);
            if (count == 0)
            {
                throw new ItemNotFoundException();
            }
        }

        public async Task<TagDetails> GetAsync(int id)
        {
            var sql = "SELECT * FROM Tags WHERE Id = @id";
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
                  AND Name LIKE '%' + @search + '%'
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
            var sql = "UPDATE Tags SET Name = @Name WHERE Id = @id";

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
