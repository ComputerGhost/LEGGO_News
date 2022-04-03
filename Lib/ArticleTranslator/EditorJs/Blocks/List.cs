using ArticleTranslator.EditorJs.Utility;
using System;
using System.Text;

namespace ArticleTranslator.EditorJs.Blocks
{
    internal class List : Block
    {
        public string Style { get; set; }
        public string[] Items { get; set; }

        public override void RenderTo(StringBuilder stringBuilder)
        {
            var listTag = Style switch
            {
                "ordered" => "ol",
                "unordered" => "ul",
                _ => throw new ArgumentException("Unsupported list type.")
            };

            stringBuilder.Append($"<{listTag}>");
            foreach (var item in Items)
            {
                var safeText = Sanitization.KeepOnlyFormatTags(item);
                stringBuilder.Append($"<li>{safeText}</li>");
            }
            stringBuilder.Append($"</{listTag}>");
        }
    }
}
