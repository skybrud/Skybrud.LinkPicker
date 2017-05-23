using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Skybrud.LinkPicker.Json.Converters {

    /// <summary>
    /// JSON coverter for serializing instances of either <see cref="LinkPickerList"/> or <see cref="LinkPickerItem"/>.
    /// </summary>
    public class LinkPickerConverter : JsonConverter {

        public override bool CanWrite {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {

            LinkPickerItem item = value as LinkPickerItem;

            if (item != null) {

                serializer.Serialize(writer, item.JObject);

            }

        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            
            // Skip if the reader is not at the start of an object
            if (reader.TokenType != JsonToken.StartObject) return null;
            
            // Load JObject from stream
            JObject obj = JObject.Load(reader);

            switch (objectType.FullName) {

                case "Skybrud.LinkPicker.LinkPickerList":
                    return LinkPickerList.Parse(obj);

                case "Skybrud.LinkPicker.LinkPickerItem":
                    return LinkPickerItem.Parse(obj);

                default:
                    return null;

            }
        
        }

        public override bool CanConvert(Type objectType) {
            return objectType == typeof(LinkPickerItem);
        }

    }

}