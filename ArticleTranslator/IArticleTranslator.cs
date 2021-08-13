using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleTranslator
{
    public interface IArticleTranslator
    {
        string GetTranslatorType();

        string TranslateToHtml(string savedData);
    }
}
