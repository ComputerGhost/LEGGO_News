using Dapper;
using DataAccess.Exceptions;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using System.Text;

namespace DataAccess.Repositories.Extensions
{
    public static class SearchableExtensions
    {
        public static async Task<PagedResults<T>> SearchAsync<T>(
            this ISearchable<T> searchable,
            string? search,
            string? cursor,
            int limit)
        {
            var parameters = new
            {
                search = search ?? "",
                start = DecodeCursor(cursor) ?? "",
                limit = limit + 1, // +1 so we can check for more
            };

            var results = await searchable.Connection.QueryAsync<T>(
                searchable.SearchSql, parameters)
                .ConfigureAwait(false);
            var resultsList = results.AsList();

            // If there are more, set the cursor and remove the extra.
            string? nextCursor = null;
            if (resultsList.Count > limit)
            {
                var pointer = searchable.PointerFunc(resultsList[limit]);
                resultsList.RemoveAt(limit);
                nextCursor = EncodeCursor(pointer);
            }

            return new PagedResults<T>
            {
                Data = resultsList,
                NextCursor = nextCursor,
            };
        }

        private static string? DecodeCursor(string? cursor)
        {
            if (cursor == null)
            {
                return null;
            }

            try
            {
                var bytes = Convert.FromBase64String(cursor);
                return Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                throw new InvalidCursorException();
            }
        }

        private static string? EncodeCursor(string? pointer)
        {
            if (pointer == null)
            {
                return null;
            }

            var bytes = Encoding.UTF8.GetBytes(pointer);
            return Convert.ToBase64String(bytes);
        }
    }
}
