using System;
using Newtonsoft.Json.Linq;

namespace Skybrud.LinkPicker.Extensions.Json {

    [Obsolete("Use extension methods from the Skybrud.Essemtials package instead.")]
    public static class JArrayExtensionMethods {

        [Obsolete("Use extension methods from the Skybrud.Essemtials package instead.")]
        public static JObject GetObject(this JArray array, int index) {
            if (array == null) return null;
            return array[index] as JObject;
        }

        [Obsolete("Use extension methods from the Skybrud.Essemtials package instead.")]
        public static T GetObject<T>(this JArray array, int index) {
            if (array == null) return default(T);
            JObject child = array[0] as JObject;
            return child == null ? default(T) : child.ToObject<T>();
        }

        [Obsolete("Use extension methods from the Skybrud.Essemtials package instead.")]
        public static T GetObject<T>(this JArray array, int index, Func<JObject, T> func) {
            return array == null ? default(T) : func(array[index] as JObject);
        }

        [Obsolete("Use extension methods from the Skybrud.Essemtials package instead.")]
        public static string GetString(this JArray array, int index) {
            if (array == null) return null;
            JToken property = array[index];
            return property == null ? null : property.Value<string>();
        }

        [Obsolete("Use extension methods from the Skybrud.Essemtials package instead.")]
        public static int GetInt32(this JArray array, int index) {
            if (array == null) return 0;
            JToken property = array[index];
            return property == null ? 0 : property.Value<int>();
        }

        [Obsolete("Use extension methods from the Skybrud.Essemtials package instead.")]
        public static double GetDouble(this JArray array, int index) {
            if (array == null) return 0;
            JToken property = array[index];
            return property == null ? 0 : property.Value<double>();
        }

        [Obsolete("Use extension methods from the Skybrud.Essemtials package instead.")]
        public static JArray GetArray<T>(this JArray array, int index) {
            return array == null ? null : array[index] as JArray;
        }

    }

}