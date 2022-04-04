using ArticleTranslator.EditorJs.Utility;
using System.Text;

namespace ArticleTranslator.EditorJs.Blocks
{
    internal class Quote : Block
    {
        public string Text { get; set; }
        public string Caption { get; set; }

        public override void RenderTo(StringBuilder stringBuilder)
        {
            var safeText = Sanitization.KeepOnlyFormatTags(Text);
            var safeCaption = Sanitization.KeepOnlyFormatTags(Caption);
            stringBuilder.Append(
                "<figure>" +
                    $"<blockquote>{safeText}</blockquote>" +
                    $"<figcaption>{safeCaption}</figcaption>" +
                "</figure>");
        }
    }
}
