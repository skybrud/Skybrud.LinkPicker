using Skybrud.LinkPicker.Tracking;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Skybrud.LinkPicker.Composers {
    
    public class LinkPickerComposer : IUserComposer {
        
        public void Compose(Composition composition) {
            composition.DataValueReferenceFactories().Append<LinkMediaTracking>();
        }

    }

}