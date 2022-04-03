using ArticleTranslator.EditorJs.Blocks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace ArticleTranslator.EditorJs.Utility
{
    internal class BlockJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Block);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            var type = (string)jObject["type"];
            var data = jObject["data"];

            switch (type)
            {
                case "header":
                    return data.ToObject<Header>(serializer);
                case "list":
                    return data.ToObject<List>(serializer);
                case "paragraph":
                    return data.ToObject<Paragraph>(serializer);
                default:
                    throw new NotImplementedException(type);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
