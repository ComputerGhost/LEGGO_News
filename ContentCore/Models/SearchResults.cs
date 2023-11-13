using System.Text;

namespace ContentCore.Models
{
    public class SearchResults<SummaryType>
    {
        /// <summary>
        /// Cursor to the item after the results; null if no more items.
        /// </summary>
        public string? NextCursor { get; set; }

        /// <summary>
        /// Cursor to the item before the results; null if no previous items.
        /// </summary>
        public string? PreviousCursor { get; set; }

        public IEnumerable<SummaryType> Results { get; set; } = null!;

        // These can be used internally and by modules for easier cursor work:

        /// <summary>
        /// Helper for internals and modules to encode `NextCursor`.
        /// </summary>
        public string? DecodedNextCursor
        {
            set => NextCursor = EncodeCursor(value);
        }

        /// <summary>
        /// Helper for internals and modules to encode `PreviousCursor`.
        /// </summary>
        public string? DecodedPreviousCursor
        {
            set => PreviousCursor = EncodeCursor(value);
        }

        private string? EncodeCursor(string? unencoded)
        {
            if (unencoded == null)
            {
                return null;
            }

            var bytes = Encoding.UTF8.GetBytes(unencoded);
            return Convert.ToBase64String(bytes);
        }
    }
}
