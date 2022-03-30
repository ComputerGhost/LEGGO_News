using ArticleTranslator.EditorJs.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace ArticleTranslator.EditorJs
{
    class ArticleTranslator : IArticleTranslator
    {
        public string GetTranslatorType()
        {
            return "EditorJs";
        }

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
