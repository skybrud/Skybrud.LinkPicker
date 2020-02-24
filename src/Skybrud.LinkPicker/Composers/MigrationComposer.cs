using Skybrud.LinkPicker.Components;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Skybrud.LinkPicker.Composers {
    
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class MigrationComposer : IUserComposer {
        
        public void Compose(Composition composition) {
            composition.Components().Append<MigrationComponent>();
        }

    }

}