using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData;
using Skybrud.Umbraco.GridData.Config;

namespace Skybrud.LinkPicker.Grid.Config {

    /// <summary>
    /// Class representing the configuration of a link picker.
    /// </summary>
    public class GridEditorLinkPickerConfig : GridEditorConfigBase {

        #region Constructors

        private GridEditorLinkPickerConfig(GridEditor editor, JObject obj) : base(editor, obj) { }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets an instance of <see cref="GridEditorLinkPickerConfig"/> from the specified <see cref="JObject"/>.
        /// </summary>
        /// <param name="editor">The parent editor.</param>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        public static GridEditorLinkPickerConfig Parse(GridEditor editor, JObject obj) {
            return obj == null ? null : new GridEditorLinkPickerConfig(editor, obj);
        }

        #endregion

    }

}