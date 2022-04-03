using ArticleTranslator.EditorJs.Utility;
using System.Text;

namespace ArticleTranslator.EditorJs.Blocks
{
    internal class Paragraph : Block
    {
        public string Text { get; set; }

        public override void RenderTo(StringBuilder stringBuilder)
        {
            var safeText = Sanitization.KeepOnlyFormatTags(Text);
            stringBuilder.Append($"<p>{safeText}</p>");
        }
    }
}
