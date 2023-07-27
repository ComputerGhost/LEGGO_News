using Dapper;
using DataAccess.DTOs;
using DataAccess.Exceptions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Dapper
{
    internal class ArticlesRepository : IArticlesRepository
    {
        private readonly IDbConnection _connection;

        public ArticlesRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> CreateAsync(ArticleSaveData saveData)
        {
            var sql = "INSERT INTO Articles (Title) OUTPUT INSERTED.Id VALUES (@Title)";

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
            var sql = "DELETE FROM Articles WHERE Id = @id";
            var count = await _connection.ExecuteAsync(sql, new { id })
                .ConfigureAwait(false);
            if (count == 0)
            {
                throw new ItemNotFoundException();
            }
        }

        public async Task<ArticleDetails> GetAsync(int id)
        {
            var sql = "SELECT * FROM Articles WHERE Id = @id";
            var item = await _connection.QuerySingleOrDefaultAsync<ArticleDetails?>(sql, id)
                .ConfigureAwait(false);
            return item ?? throw new ItemNotFoundException();
        }

        public async Task<IList<ArticleSummary>> SearchAsync(
            string? search, 
            string? start, 
            int limit)
        {
            var sql = @"
                SELECT TOP (@limit) Id, Title FROM Articles 
                WHERE Title >= @start 
                  AND Title LIKE '%' + @search + '%'
                ORDER BY Title";

            var parameters = new
            {
                search = search ?? "",
                start = start ?? "",
                limit,
            };

            var results = await _connection.QueryAsync<ArticleSummary>(sql, parameters)
                .ConfigureAwait(false);
            return results.AsList();
        }

        public async Task UpdateAsync(int id, ArticleSaveData saveData)
        {
            var sql = "UPDATE Articles SET Title = @Title WHERE Id = @id";

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
