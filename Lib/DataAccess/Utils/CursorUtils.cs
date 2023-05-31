using DataAccess.Exceptions;
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
    }
}
