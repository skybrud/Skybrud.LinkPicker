using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Skybrud.LinkPicker {

    /// <summary>
    /// Class representing the model for the LinkPicker editor.
    /// </summary>
    public class LinkPickerList {

        #region Properties

        /// <summary>
        /// Gets a reference to the <see cref="JObject"/> the link picker list was parsed from (if parsed from a JSON object).
        /// </summary>
        [JsonIgnore]
        public JObject JObject { get; private set; }

        /// <summary>
        /// Gets an array of all link items.
        /// </summary>
        public LinkPickerItem[] Items { get; internal set; }

        /// <summary>
        /// Gets the total amount of link items.
        /// </summary>
        public int Count {
            get { return Items.Length; }
        }

        #endregion

        #region Constructors

        internal LinkPickerList() { }

        #endregion

        #region Static methods

        /// <summary>
        /// Deseralizes the specified JSON string into an instance of <code>LinkPickerList</code>.
        /// </summary>
        /// <param name="json">The raw JSON to be parsed.</param>
        public static LinkPickerList Deserialize(string json) {

            if (json != null && json.StartsWith("[") && json.EndsWith("]")) {
                JArray array = JsonConvert.DeserializeObject<JArray>(json);
                return new LinkPickerList {
                    Items = (
                        from obj in array
                        let link = LinkPickerItem.Parse(obj as JObject)
                        where link != null
                        select link
                    ).ToArray()
                };
            }

            return new LinkPickerList {
                Items = new LinkPickerItem[0]
            };

        }

        #endregion

    }

}