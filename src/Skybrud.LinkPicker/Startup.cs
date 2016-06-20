using Skybrud.LinkPicker.Grid.Converters;
using Skybrud.Umbraco.GridData;
using Umbraco.Core;

namespace Skybrud.LinkPicker {
   
    public class Startup : ApplicationEventHandler {

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext) {
            GridContext.Current.Converters.Add(new LinkPickerGridConverter());
        }
    
    }

}