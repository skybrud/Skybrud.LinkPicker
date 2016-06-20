using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData;
using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.Json;

namespace Skybrud.LinkPicker.Grid.Config {

    /// <summary>
    /// Class representing the configuration of a link picker.
    /// </summary>
    public class GridEditorLinkPickerConfig : GridJsonObject, IGridEditorConfig {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent editor.
        /// </summary>
        public GridEditor Editor { get; private set; }
            
        #endregion

        #region Constructors

        private GridEditorLinkPickerConfig(GridEditor editor, JObject obj) : base(obj) {
            Editor = editor;
        }

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