﻿using System.Collections.Generic;
using Skybrud.InnerContent.PropertyEditors;
using Umbraco.Core.Logging;
using Umbraco.Core.PropertyEditors;

namespace Skybrud.LinkPicker.PropertyEditors {

    [DataEditor("Skybrud.LinkPicker.Link", EditorType.PropertyValue, "Skybrud LinkPicker Link", "/App_Plugins/Skybrud.LinkPicker/Views/Editors/Link.html", ValueType = ValueTypes.Json, Group = "Skybrud.dk", Icon = "icon-anchor")]
    public class LinkPropertyEditor : DataEditor {

        public LinkPropertyEditor(ILogger logger) : base(logger) { }

        /// <inheritdoc />
        protected override IConfigurationEditor CreateConfigurationEditor() => new LinkConfigurationEditor();

    }
}