using Skybrud.LinkPicker.Factories;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Skybrud.LinkPicker.Composers {
    
    internal class LinkPickerComposer : IUserComposer {
        
        public void Compose(Composition composition) {
            composition.DataValueReferenceFactories().Append<LinkReferenceFactory>();
        }

    }

}