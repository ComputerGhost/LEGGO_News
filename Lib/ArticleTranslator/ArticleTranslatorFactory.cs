using Database.Constants;
using System;

namespace ArticleTranslator
{
    public class ArticleTranslatorFactory
    {
        public IArticleTranslator CreateTranslator(string format)
        {
            // We need to add WordPress and possibly more formats later.
            switch (format.ToLower()) {
                case ArticleFormat.EditorJs:
                    return new EditorJs.ArticleTranslator();
                default:
                    throw new ArgumentException("Format is not supported.", format);
            }
        }
    }
}
