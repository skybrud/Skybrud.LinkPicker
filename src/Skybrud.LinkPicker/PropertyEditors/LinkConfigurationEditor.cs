using Umbraco.Core.PropertyEditors;

namespace Skybrud.LinkPicker.PropertyEditors {

    public class LinkConfigurationEditor : ConfigurationEditor<LinkConfiguration> {

        public LinkConfigurationEditor() {
            //Field(nameof(ElementsConfiguration.View)).Config = new Dictionary<string, object> {
            //    { "view", "/App_Plugins/Skybrud.InnerContent/Views/Partials/Multiple/Default.html" }
            //};
        }

    }

}