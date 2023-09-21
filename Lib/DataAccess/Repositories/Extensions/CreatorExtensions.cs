using Dapper;
using DataAccess.Exceptions;
using DataAccess.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace DataAccess.Repositories.Extensions
{
    public static class CreatorExtensions
    {
        public static async Task<int> CreateAsync<T>(
            this ICreator<T> creator,
            T saveData)
        {
            try
            {
                return await creator.Connection.QuerySingleAsync<int>(
                    creator.InsertSql, saveData)
                    .ConfigureAwait(false);
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                throw new ItemAlreadyExistsException();
            }
        }
    }
}
