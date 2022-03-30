using System.Text;

namespace ArticleTranslator.EditorJs.Models
{
    abstract class Block
    {
        public abstract void Render(StringBuilder stringBuilder);
    }
}
