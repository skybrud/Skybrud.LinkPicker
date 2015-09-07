using System;
using Newtonsoft.Json.Linq;
using Skybrud.LinkPicker.Extensions.Json;

namespace Skybrud.LinkPicker {

    /// <summary>
    /// Class representing a single link item.
    /// </summary>
    public class LinkPickerItem {

        #region Properties

        /// <summary>
        /// Gets the ID of the selected content or media. If an URL has been selected, this will return <code>0</code>.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the name of the link.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the URL of the link.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Gets the link target.
        /// </summary>
        public string Target { get; private set; }

        /// <summary>
        /// Gets the mode (or type) of the link.
        /// </summary>
        public LinkPickerMode Mode { get; private set; }

        /// <summary>
        /// Gets whether the link is valid.
        /// </summary>
        public bool IsValid {
            get { return !String.IsNullOrWhiteSpace(Url); }
        }

        #endregion

        #region Constructors

        internal LinkPickerItem() { }

        #endregion

        #region Static methods
        
        /// <summary>
        /// Parses the specified <code>obj</code> into an instance of <code>LinkPickerItem</code>.
        /// </summary>
        /// <param name="obj">The instance of <code>JObject</code> to be parsed.</param>
        public static LinkPickerItem Parse(JObject obj) {

            if (obj == null) return null;

            // Get the ID
            int id = obj.GetInt32("id");

            // Parse the link mode
            LinkPickerMode mode;
            if (obj.GetValue("mode") == null) {
                if (obj.GetBoolean("isMedia")) {
                    mode = LinkPickerMode.Media;
                } else if (id > 0) {
                    mode = LinkPickerMode.Content;
                } else {
                    mode = LinkPickerMode.Url;
                }
            } else {
                mode = (LinkPickerMode) Enum.Parse(typeof(LinkPickerMode), obj.GetString("mode"), true);
            }
            
            // Parse remaining properties
            return new LinkPickerItem {
                Id = id,
                Name = obj.GetString("name"),
                Url = obj.GetString("url"),
                Target = obj.GetString("target"),
                Mode = mode
            };

        }

        #endregion

    }

}