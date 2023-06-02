using System.Collections.Generic;
using Skybrud.LinkPicker.Models;
using Skybrud.LinkPicker.PropertyEditors;
using Umbraco.Core;
using Umbraco.Core.Models.Editors;
using Umbraco.Core.PropertyEditors;

namespace Skybrud.LinkPicker.Factories {

    internal class LinkReferenceFactory : IDataValueReferenceFactory, IDataValueReference {

        public IDataValueReference GetDataValueReference() => this;

        public IEnumerable<UmbracoEntityReference> GetReferences(object value) {

            List<UmbracoEntityReference> references = new List<UmbracoEntityReference>();
            if (value is not string json) return references;

            LinkPickerLink link = LinkPickerLink.Deserialize(json);
            if (link == null) return references;

            switch (link.Type) {

                case LinkPickerType.Content:

                    references.Add(new UmbracoEntityReference(new GuidUdi(Constants.UdiEntityType.Document, link.Key)));
                    break;

                case LinkPickerType.Media:
                    references.Add(new UmbracoEntityReference(new GuidUdi(Constants.UdiEntityType.Media, link.Key)));
                    break;

            }

            return references;

        }

        public bool IsForEditor(IDataEditor dataEditor) => dataEditor.Alias.InvariantEquals(LinkEditor.EditorAlias);

    }

}