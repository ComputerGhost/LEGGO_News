using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleTranslator
{
    public interface IArticleTranslator
    {
        string TranslateToHtml(string savedData);
    }
}
