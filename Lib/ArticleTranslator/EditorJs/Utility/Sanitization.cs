using Vereyon.Web;

namespace ArticleTranslator.EditorJs.Utility
{
    internal class Sanitization
    {
        private static HtmlSanitizer _sanitizer;
        private static HtmlSanitizer Sanitizer
        {
            get
            {
                if (_sanitizer == null)
                {
                    _sanitizer = new HtmlSanitizer();
                    _sanitizer.Tag("a").CheckAttributeUrl("href").RemoveEmpty();
                    _sanitizer.Tag("b").RemoveEmpty();
                    _sanitizer.Tag("i").RemoveEmpty();
                }
                return _sanitizer;
            }
        }

        public static string KeepOnlySafeTags(string dirtyHtml)
        {
            return Sanitizer.Sanitize(dirtyHtml);
        }

    }
}
