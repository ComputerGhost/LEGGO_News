using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ArticleTranslator.EditorJs.Models
{
    class Paragraph : Block
    {
        public string Text { get; set; }

        public override void Render(StringBuilder stringBuilder)
        {
            string safeText = HttpUtility.HtmlEncode(Text);
            stringBuilder.Append($"<p>{safeText}</p>");
        }
    }
}
