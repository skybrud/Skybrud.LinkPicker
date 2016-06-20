using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData;
using Skybrud.Umbraco.GridData.Interfaces;

namespace Skybrud.LinkPicker.Grid.Values {
    
    public class GridControlLinkPickerValue : LinkPickerList, IGridControlValue {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent control.
        /// </summary>
        public GridControl Control { get; private set; }

        #endregion

        #region Constructors

        protected GridControlLinkPickerValue(GridControl control, JObject obj) : base(obj) {
            Control = control;
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets an instance of <see cref="GridControlLinkPickerValue"/> from the specified <see cref="JObject"/>.
        /// </summary>
        /// <param name="control">The parent control.</param>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        public static GridControlLinkPickerValue Parse(GridControl control, JObject obj) {
            return control == null ? null : new GridControlLinkPickerValue(control, obj ?? new JObject());
        }

        #endregion

    }

}