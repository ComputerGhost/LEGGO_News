using ArticleTranslator.EditorJs.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace ArticleTranslator.EditorJs
{
    class BlockJsonConverter : JsonConverter
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

            switch (type) {
                case "paragraph":
                    return data.ToObject<Paragraph>(serializer);
                case "header":
                    return data.ToObject<Header>(serializer);
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
