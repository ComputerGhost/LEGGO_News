using DataAccess.Exceptions;
using DataAccess.Models;
using System.Text;

namespace DataAccess.Utils
{
    internal static class CursorUtils
    {
        public static string? Decode(string? cursor)
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

        public static string? Encode(string? pointer)
        {
            if (pointer == null)
            {
                return null;
            }

            var bytes = Encoding.UTF8.GetBytes(pointer);
            return Convert.ToBase64String(bytes);
        }

        public static PagedResults<T> CreatePagedResults<T>(
            IList<T> results_maybePlusOne,
            int limit,
            Func<T, string> pointerFunc)
        {
            var pagedResults = new PagedResults<T>
            {
                Data = results_maybePlusOne,
                NextCursor = null,
            };

            if (results_maybePlusOne.Count > limit)
            {
                var pointer = pointerFunc(results_maybePlusOne[limit]);
                pagedResults.NextCursor = Encode(pointer);
                results_maybePlusOne.RemoveAt(limit);
            }

            return pagedResults;
        }
    }
}
