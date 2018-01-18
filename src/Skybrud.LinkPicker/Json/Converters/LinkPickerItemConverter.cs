using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Skybrud.LinkPicker.Json.Converters {
    
    /// <summary>
    /// JSON converter specifically for serializing and deserializing instances of <see cref="LinkPickerItem" />. This
    /// converter differs from <see cref="LinkPickerConverter" /> as this converter will serialize an invalid
    /// <see cref="LinkPickerItem" /> to <code>null</code>.
    /// </summary>
    public class LinkPickerItemConverter : JsonConverter {
        
        /// <summary>Writes the JSON representation of the object.</summary>
        /// <param name="writer">The <see cref="JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            if (!(value is LinkPickerItem)) return;
            LinkPickerItem linkPickerItem = (LinkPickerItem)value;
            if (linkPickerItem.IsValid) {
                serializer.Serialize(writer, linkPickerItem);
            } else {
                writer.WriteNull();
            }
        }

        /// <summary>Reads the JSON representation of the object.</summary>
        /// <param name="reader">The <see cref="JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            switch (reader.TokenType) {
                case JsonToken.StartObject:
                    return ReadJsonObject(reader, objectType);
                case JsonToken.Null:
                    return new LinkPickerItem(0, "", "/", "_self", LinkPickerMode.Content);
                default:
                    throw new JsonSerializationException("Unexpected token type: " + reader.TokenType);
            }
        }

        private object ReadJsonObject(JsonReader reader, Type objectType) {
            JObject obj = JObject.Load(reader);
            if (!(objectType == typeof(LinkPickerItem))) return null;
            if (obj != null) return LinkPickerItem.Parse(obj);
            return new LinkPickerItem();
        }

        /// <summary>
        /// Determines whether this instance can convert the specified <paramref name="objectType"/>.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns><code>true</code> if this instance can convert the specified object type; otherwise <code>false</code>.</returns>
        public override bool CanConvert(Type objectType){
            return objectType == typeof(LinkPickerItem);
        }

    }

}