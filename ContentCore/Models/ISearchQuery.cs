using ContentCore.Exceptions;
using System.Text;
using System.Text.Json.Serialization;

namespace ContentCore.Models
{
    public class ISearchQuery
    {
        /// <summary>
        /// Cursor to the first item of the desired results.
        /// </summary>
        /// <remarks>
        /// The behavior is not defined if `EndCursor` is also set.
        /// </remarks>
        public string? StartCursor { get; set; }

        /// <summary>
        /// Cursor to the last item of the desired results.
        /// </summary>
        /// <remarks>
        /// The behavior is not defined if `StartCursor` is also set.
        /// </remarks>
        public string? EndCursor { get; set; }

        /// <summary>
        /// Maximum number of results to return.
        /// </summary>
        public int Count { get; set; }

        // These can be used internally and by modules for easier cursor work:

        /// <summary>
        /// Helper for internals and modules to decode `StartCursor`.
        /// </summary>
        /// <remarks>
        /// This property is not serialized.
        /// </remarks>
        [JsonIgnore]
        public string? DecodedStartCursor => DecodeCursor(StartCursor);

        /// <summary>
        /// Helper for internals and modules to decode `EndCursor`.
        /// </summary>
        /// <remarks>
        /// This property is not serialized.
        /// </remarks>
        [JsonIgnore]
        public string? DecodedEndCursor => DecodeCursor(EndCursor);

        private string? DecodeCursor(string? cursor)
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
    }
}
