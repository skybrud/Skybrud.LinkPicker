using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Json;

namespace Skybrud.LinkPicker.Grid.Config {

    /// <summary>
    /// Class representing the columns configuration of a link picker.
    /// </summary>
    public class GridEditorLinkPickerConfigColumns : GridJsonObject {

        #region Properties

        public bool Type { get; }

        public bool Id { get; }

        public bool Name { get; }

        public bool Url { get; }

        public bool Target { get; }

        #endregion

        #region Constructors

        private GridEditorLinkPickerConfigColumns(JObject obj) : base(obj) {
            Type = obj.Property("type") == null || obj.GetBoolean("type");
            Id = obj.Property("id") == null || obj.GetBoolean("id");
            Name = obj.Property("name") == null || obj.GetBoolean("name");
            Url = obj.Property("url") == null || obj.GetBoolean("url");
            Target = obj.Property("target") == null || obj.GetBoolean("target");
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="obj"/> into an instance of <see cref="GridEditorLinkPickerConfigColumns"/>.
        /// </summary>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        /// <returns>An instance of <see cref="GridEditorLinkPickerConfigColumns"/>.</returns>
        public static GridEditorLinkPickerConfigColumns Parse(JObject obj) {
            return new GridEditorLinkPickerConfigColumns(obj ?? new JObject());
        }

        #endregion

    }

}