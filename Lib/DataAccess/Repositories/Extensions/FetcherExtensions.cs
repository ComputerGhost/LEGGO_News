using Dapper;
using DataAccess.Exceptions;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories.Extensions
{
    public static class FetcherExtensions
    {
        public static async Task<T> FetchById<T>(
            this IFetcher<T> fetcher,
            int id)
        {
            var item = await fetcher.Connection.QuerySingleOrDefaultAsync<T?>(
                fetcher.FetchSql, new { id })
                .ConfigureAwait(false);
            return item ?? throw new ItemNotFoundException();
        }
    }
}
