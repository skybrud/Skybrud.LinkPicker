using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Extensions.Json;
using Skybrud.Umbraco.GridData.Json;

namespace Skybrud.LinkPicker.Grid.Config {

    /// <summary>
    /// Class representing the title configuration of a link picker.
    /// </summary>
    public class GridEditorLinkPickerTitleConfig : GridJsonObject {

        #region Properties

        /// <summary>
        /// Gets whether the title of the link picker list should be shown.
        /// </summary>
        public bool Show { get; private set; }
            
        #endregion

        #region Constructors

        private GridEditorLinkPickerTitleConfig(JObject obj) : base(obj) {
            Show = obj.GetBoolean("show");
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets an instance of <see cref="GridEditorLinkPickerTitleConfig"/> from the specified <see cref="JObject"/>.
        /// </summary>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        public static GridEditorLinkPickerTitleConfig Parse(JObject obj) {
            return obj == null ? null : new GridEditorLinkPickerTitleConfig( obj);
        }

        #endregion

    }

}