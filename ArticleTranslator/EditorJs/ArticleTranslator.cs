using ArticleTranslator.EditorJs.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleTranslator.EditorJs
{
    class ArticleTranslator : IArticleTranslator
    {
        public string TranslateToHtml(string savedData)
        {
            var blocks = JsonConvert.DeserializeObject<IEnumerable<Block>>(
                savedData, 
                new BlockJsonConverter()
            );

            var stringBuilder = new StringBuilder();
            foreach (var block in blocks) {
                block.Render(stringBuilder);
            }

            return stringBuilder.ToString();
        }
    }
}
