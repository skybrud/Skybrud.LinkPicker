using Umbraco.Core.Logging;
using Umbraco.Core.PropertyEditors;

namespace Skybrud.LinkPicker.PropertyEditors {

    [DataEditor(EditorAlias, EditorType.PropertyValue, "Skybrud LinkPicker Link", EditorView, ValueType = ValueTypes.Json, Group = "Skybrud.dk", Icon = "icon-link")]
    public class LinkEditor : DataEditor {

        #region Constants

        /// <summary>
        /// Gets the alias of the editor.
        /// </summary>
        public const string EditorAlias = "Skybrud.LinkPicker.Link";

        /// <summary>
        /// Gets the view of the editor.
        /// </summary>
        public const string EditorView = "/App_Plugins/Skybrud.LinkPicker/Views/Editors/Link.html";

        #endregion

        #region Constructors

        public LinkEditor(ILogger logger) : base(logger) { }

        #endregion

        #region Member methods

        /// <inheritdoc />
        protected override IConfigurationEditor CreateConfigurationEditor() => new LinkConfigurationEditor();

        #endregion

    }

}