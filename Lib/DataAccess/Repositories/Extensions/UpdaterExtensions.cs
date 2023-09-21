using Dapper;
using DataAccess.Exceptions;
using DataAccess.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace DataAccess.Repositories.Extensions
{
    public static class UpdaterExtensions
    {
        public static async Task UpdateAsync<T>(
            this IUpdater<T> updater,
            T saveData)
        {
            try
            {
                var count = await updater.Connection.ExecuteAsync(
                    updater.UpdateSql, saveData)
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
