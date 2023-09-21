using Dapper;
using DataAccess.Exceptions;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories.Extensions
{
    public static class DeleterExtensions
    {
        public static async Task DeleteAsync(
            this IDeleter deleter,
            int id)
        {
            var count = await deleter.Connection.ExecuteAsync(
                deleter.DeleteSql, new { id })
                .ConfigureAwait(false);
            if (count == 0)
            {
                throw new ItemNotFoundException();
            }
        }
    }
}
