using ArticleTranslator.EditorJs.Utility;
using System.Text;

namespace ArticleTranslator.EditorJs.Blocks
{
    class Paragraph : Block
    {
        public string Text { get; set; }

        public override void RenderTo(StringBuilder stringBuilder)
        {
            var safeText = Sanitization.KeepOnlySafeTags(Text);
            stringBuilder.Append($"<p>{safeText}</p>");
        }
    }
}
