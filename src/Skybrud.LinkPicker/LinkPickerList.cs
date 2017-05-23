using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.LinkPicker.Json.Converters;

namespace Skybrud.LinkPicker {

    /// <summary>
    /// Class representing the model for the LinkPicker editor.
    /// </summary>
    [JsonConverter(typeof(LinkPickerConverter))]
    public class LinkPickerList {

        #region Properties

        /// <summary>
        /// Gets a reference to the <see cref="JObject"/> the link picker list was parsed from (if parsed from a JSON object).
        /// </summary>
        [JsonIgnore]
        public JObject JObject { get; private set; }

        /// <summary>
        /// Gets the title of the control.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; private set; }

        /// <summary>
        /// Gets whether the control has a title.
        /// </summary>
        [JsonIgnore]
        public bool HasTitle {
            get { return !String.IsNullOrWhiteSpace(Title); }
        }

        /// <summary>
        /// Gets an array of all link items.
        /// </summary>
        [JsonProperty("items")]
        public LinkPickerItem[] Items { get; internal set; }

        /// <summary>
        /// Gets whether the link picker list has any items.
        /// </summary>
        [JsonIgnore]
        public bool HasItems {
            get { return Items != null && Items.Length > 0; }
        }

        /// <summary>
        /// Gets the total amount of link items.
        /// </summary>
        [JsonProperty("count")]
        public int Count {
            get { return Items.Length; }
        }

        /// <summary>
        /// Gets whether the link picker list is valid (alias of <see cref="HasItems"/>).
        /// </summary>
        [JsonIgnore]
        public virtual bool IsValid {
            get { return HasItems; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance with an empty model.
        /// </summary>
        internal LinkPickerList() {
            Items = new LinkPickerItem[0];
        }

        /// <summary>
        /// Initializes a new instance based on the specified <see cref="JObject"/>.
        /// </summary>
        /// <param name="obj">An instance of <see cref="JObject"/> representing the link picker list.</param>
        protected LinkPickerList(JObject obj) {
            JObject = obj;
            Title = obj.GetString("title");
            Items = obj.GetArrayItems("items", LinkPickerItem.Parse);
        }

        /// <summary>
        /// Initializes a new instance based on the specified <see cref="JArray"/>.
        /// </summary>
        /// <param name="array">An instance of <see cref="JArray"/> representing the link picker list.</param>
        protected LinkPickerList(JArray array) {
            Items = (
                from obj in array
                let link = LinkPickerItem.Parse(obj as JObject)
                where link != null
                select link
            ).ToArray();
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <see cref="JObject"/> into an instance of <see cref="LinkPickerList"/>.
        /// </summary>
        /// <param name="obj">An instance of <see cref="JObject"/> representing the link picker list.</param>
        /// <returns>Returns an instacne of <see cref="LinkPickerList"/>, or <code>null</code> if <code>obj</code> is <code>null</code>.</returns>
        public static LinkPickerList Parse(JObject obj) {
            return obj == null ? null : new LinkPickerList(obj);
        }

        /// <summary>
        /// Parses the specified <see cref="JArray"/> into an instance of <see cref="LinkPickerList"/>.
        /// </summary>
        /// <param name="array">An instance of <see cref="JArray"/> representing the link picker list.</param>
        /// <returns>Returns an instacne of <see cref="LinkPickerList"/>, or <code>null</code> if <code>array</code> is <code>null</code>.</returns>
        public static LinkPickerList Parse(JArray array) {
            return array == null ? null : new LinkPickerList(array);
        }

        /// <summary>
        /// Deseralizes the specified JSON string into an instance of <see cref="LinkPickerList"/>.
        /// </summary>
        /// <param name="json">The raw JSON to be parsed.</param>
        public static LinkPickerList Deserialize(string json) {
            if (json == null) return new LinkPickerList();
            if (json.StartsWith("{") && json.EndsWith("}")) return Parse(JsonConvert.DeserializeObject<JObject>(json));
            if (json.StartsWith("[") && json.EndsWith("]")) return Parse(JsonConvert.DeserializeObject<JArray>(json));
            return new LinkPickerList();
        }

        #endregion

    }

}