using System;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Json;

namespace Skybrud.LinkPicker.Grid.Config {

    /// <summary>
    /// Class representing the title configuration of a link picker.
    /// </summary>
    public class GridEditorLinkPickerConfigTitle : GridJsonObject {

        #region Properties

        /// <summary>
        /// Gets whether the title of the link picker list should be shown.
        /// </summary>
        public bool Show { get; private set; }

        /// <summary>
        /// Gets the placeholder title of the link picker.
        /// </summary>
        public string Placeholder { get; private set; }

        /// <summary>
        /// Gets whether the <see cref="Placeholder"/> property has a value.
        /// </summary>
        public bool HasPlaceholder {
            get { return !String.IsNullOrWhiteSpace(Placeholder); }
        }

        /// <summary>
        /// Gets the default (fallback) title of the link picker.
        /// </summary>
        public string Default { get; private set; }

        /// <summary>
        /// Gets whether the <see cref="Default"/> property has a value.
        /// </summary>
        public bool HasDefault {
            get { return !String.IsNullOrWhiteSpace(Default); }
        }

        #endregion

        #region Constructors

        private GridEditorLinkPickerConfigTitle(JObject obj) : base(obj) {
            Show = obj.GetBoolean("show");
            Placeholder = obj.GetString("placeholder");
            Default = obj.GetString("default");
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="obj"/> into an instance of <see cref="GridEditorLinkPickerConfigTitle"/>.
        /// </summary>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        /// <returns>An instance of <see cref="GridEditorLinkPickerConfigTitle"/>.</returns>
        public static GridEditorLinkPickerConfigTitle Parse(JObject obj) {
            return new GridEditorLinkPickerConfigTitle(obj ?? new JObject());
        }

        #endregion

    }

}