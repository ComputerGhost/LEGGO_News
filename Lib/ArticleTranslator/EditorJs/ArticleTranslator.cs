using ArticleTranslator.EditorJs.Blocks;
using ArticleTranslator.EditorJs.Utility;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace ArticleTranslator.EditorJs
{
    internal class ArticleTranslator : IArticleTranslator
    {
        public string GetTranslatorType()
        {
            return "EditorJs";
        }

        public string TranslateToHtml(string savedData)
        {
            var savedBlocks = JsonConvert.DeserializeObject<IEnumerable<Block>>(
                savedData, 
                new BlockJsonConverter()
            );

            var stringBuilder = new StringBuilder();
            foreach (var block in savedBlocks)
            {
                block.RenderTo(stringBuilder);
            }

            return stringBuilder.ToString();
        }
    }
}
