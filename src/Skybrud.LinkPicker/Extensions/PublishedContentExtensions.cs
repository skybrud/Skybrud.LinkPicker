using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Skybrud.LinkPicker.Extensions {

    /// <summary>
    /// Various extension methods for <code>IPublishedContent</code> and the LinkPicker.
    /// </summary>
    public static class PublishedContentExtensions {

        /// <summary>
        /// Gets the first link item from a LinkPicker model from the property with the specified
        /// <code>propertyAlias</code>. If property isn't a link picker (or the list is empty),
        /// an empty item will be returned instead.
        /// </summary>
        /// <param name="content">The published content to read the property from.</param>
        /// <param name="propertyAlias">The alias of the property.</param>
        public static LinkPickerItem GetLinkPickerItem(this IPublishedContent content, string propertyAlias) {
            LinkPickerList list = content.GetPropertyValue(propertyAlias) as LinkPickerList;
            LinkPickerItem item = (list == null ? null : list.Items.FirstOrDefault());
            return item ?? new LinkPickerItem();
        }

        /// <summary>
        /// Gets the LinkPicker model from the property with the specified <code>propertyAlias</code>.
        /// </summary>
        /// <param name="content">The published content to read the property from.</param>
        /// <param name="propertyAlias">The alias of the property.</param>
        public static LinkPickerList GetLinkPickerList(this IPublishedContent content, string propertyAlias) {
            return (content == null ? null : content.GetPropertyValue<LinkPickerList>(propertyAlias)) ?? new LinkPickerList {
                Items = new LinkPickerItem[0]
            };
        }

    }

}