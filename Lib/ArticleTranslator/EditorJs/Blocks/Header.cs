using ArticleTranslator.EditorJs.Utility;
using System.Text;

namespace ArticleTranslator.EditorJs.Blocks
{
    internal class Header : Block
    {
        public string Text { get; set; }
        public int Level { get; set; }

        public override void RenderTo(StringBuilder stringBuilder)
        {
            var safeText = Sanitization.KeepOnlyFormatTags(Text);
            stringBuilder.Append($"<h{Level}>{safeText}</h{Level}>");
        }
    }
}
