using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleTranslator.EditorJs.Models
{
    abstract class Block
    {
        public abstract void Render(StringBuilder stringBuilder);
    }
}
