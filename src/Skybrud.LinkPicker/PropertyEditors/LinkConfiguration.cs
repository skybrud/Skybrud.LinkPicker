using Umbraco.Core.PropertyEditors;

namespace Skybrud.LinkPicker.PropertyEditors {

    public class LinkConfiguration {

        [ConfigurationField("hideTarget", "Hide target?", "boolean", Description = "Select whether the target option should be hidden.")]
        public bool HideTarget { get; set; }

    }

}