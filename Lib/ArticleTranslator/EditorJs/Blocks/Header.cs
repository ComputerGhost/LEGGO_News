using System.Text;
using System.Web;

namespace ArticleTranslator.EditorJs.Models
{
    class Header : Block
    {
        public string Text { get; set; }
        public int Level { get; set; }

        public override void Render(StringBuilder stringBuilder)
        {
            string safeText = HttpUtility.HtmlEncode(Text);
            stringBuilder.Append($"<h{Level}>{safeText}</h{Level}>");
        }
    }
}
