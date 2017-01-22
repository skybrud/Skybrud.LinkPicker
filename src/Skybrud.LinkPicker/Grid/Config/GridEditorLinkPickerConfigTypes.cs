using System;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Json;

namespace Skybrud.LinkPicker.Grid.Config {

    /// <summary>
    /// Class representing the types configuration of a link picker.
    /// </summary>
    public class GridEditorLinkPickerConfigTypes : GridJsonObject {

        #region Properties

        public bool Url { get; private set; }

        public bool Content { get; private set; }

        public bool Media { get; private set; }

        #endregion

        #region Constructors

        private GridEditorLinkPickerConfigTypes(JObject obj) : base(obj) {
            Url = obj.Property("url") == null || obj.GetBoolean("url");
            Content = obj.Property("content") == null || obj.GetBoolean("content");
            Media = obj.Property("media") == null || obj.GetBoolean("media");
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="obj"/> into an instance of <see cref="GridEditorLinkPickerConfigTypes"/>.
        /// </summary>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        /// <returns>An instance of <see cref="GridEditorLinkPickerConfigTypes"/>.</returns>
        public static GridEditorLinkPickerConfigTypes Parse(JObject obj) {
            return new GridEditorLinkPickerConfigTypes(obj ?? new JObject());
        }

        #endregion

    }

}