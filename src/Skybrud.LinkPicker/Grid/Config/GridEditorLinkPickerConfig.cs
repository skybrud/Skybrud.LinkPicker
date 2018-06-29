using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData;
using Skybrud.Umbraco.GridData.Config;

namespace Skybrud.LinkPicker.Grid.Config {

    /// <summary>
    /// Class representing the configuration of a link picker.
    /// </summary>
    public class GridEditorLinkPickerConfig : GridEditorConfigBase {

        #region Properties

        public GridEditorLinkPickerConfigTitle Title { get; }

        public int Limit { get; }

        public GridEditorLinkPickerConfigTypes Types { get; }

        public bool ShowTable { get; }

        public GridEditorLinkPickerConfigColumns Columns { get; }


        #endregion

        #region Constructors

        private GridEditorLinkPickerConfig(GridEditor editor, JObject obj) : base(editor, obj) {
            Title = obj.GetObject("title", GridEditorLinkPickerConfigTitle.Parse);
            Limit = obj.GetInt32("limit");
            Types = obj.GetObject("types", GridEditorLinkPickerConfigTypes.Parse);
            ShowTable = obj.GetBoolean("showTable");
            Columns = obj.GetObject("columns", GridEditorLinkPickerConfigColumns.Parse);
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="obj"/> into an instance of <see cref="GridEditorLinkPickerConfig"/>.
        /// </summary>
        /// <param name="editor">The parent editor.</param>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        /// <returns>An instance of <see cref="GridEditorLinkPickerConfig"/>.</returns>
        public static GridEditorLinkPickerConfig Parse(GridEditor editor, JObject obj) {
            return obj == null ? null : new GridEditorLinkPickerConfig(editor, obj);
        }

        #endregion

    }

}