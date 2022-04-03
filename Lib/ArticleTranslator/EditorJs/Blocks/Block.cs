using System.Text;

namespace ArticleTranslator.EditorJs.Blocks
{
    internal abstract class Block
    {
        public abstract void RenderTo(StringBuilder stringBuilder);
    }
}
