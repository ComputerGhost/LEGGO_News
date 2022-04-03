using System.Text;

namespace ArticleTranslator.EditorJs.Blocks
{
    abstract class Block
    {
        public abstract void RenderTo(StringBuilder stringBuilder);
    }
}
