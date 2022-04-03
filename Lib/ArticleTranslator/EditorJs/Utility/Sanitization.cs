using Vereyon.Web;

namespace ArticleTranslator.EditorJs.Utility
{
    internal class Sanitization
    {
        private static HtmlSanitizer _formatSanitizer;
        private static HtmlSanitizer FormatSanitizer
        {
            get
            {
                if (_formatSanitizer == null)
                {
                    _formatSanitizer = new HtmlSanitizer();
                    _formatSanitizer.Tag("a").CheckAttributeUrl("href").RemoveEmpty();
                    _formatSanitizer.Tag("em").RemoveEmpty();
                    _formatSanitizer.Tag("b").RemoveEmpty();
                    _formatSanitizer.Tag("i").RemoveEmpty();
                    _formatSanitizer.Tag("strong").RemoveEmpty();
                }
                return _formatSanitizer;
            }
        }

        public static string KeepOnlyFormatTags(string dirtyHtml)
        {
            return FormatSanitizer.Sanitize(dirtyHtml);
        }

    }
}
