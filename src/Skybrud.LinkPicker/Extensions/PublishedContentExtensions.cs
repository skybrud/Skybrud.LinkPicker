using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Skybrud.LinkPicker.Extensions {

    /// <summary>
    /// Various extension methods for <see cref="IPublishedContent"/> and the LinkPicker.
    /// </summary>
    public static class PublishedContentExtensions {

        /// <summary>
        /// Gets the first link item from the <see cref="LinkPickerList"/> of the property with the specified
        /// <paramref name="propertyAlias"/>.
        /// 
        /// If the property isn't a link picker (or the list is empty), an empty item
        /// will be returned instead. You can use the <see cref="LinkPickerItem.IsValid"/> property to check whether
        /// the returned item is valid.
        /// </summary>
        /// <param name="content">The published content to read the property from.</param>
        /// <param name="propertyAlias">The alias of the property.</param>
        /// <returns>An instance of <see cref="LinkPickerItem"/>.</returns>
        public static LinkPickerItem GetLinkPickerItem(this IPublishedContent content, string propertyAlias) {
            LinkPickerList list = content.GetPropertyValue(propertyAlias) as LinkPickerList;
            LinkPickerItem item = list?.Items.FirstOrDefault();
            return item ?? new LinkPickerItem();
        }

        /// <summary>
        /// Gets the <see cref="LinkPickerList"/> of the property with the specified <paramref name="propertyAlias"/>.
        /// 
        /// If the property isn't a link picker, an empty list will be returned instead. You can use the
        /// <see cref="LinkPickerList.IsValid"/> property to check whether the returned list is valid (the list is also
        /// considered invalid if it doesn't have any items).
        /// </summary>
        /// <param name="content">The published content to read the property from.</param>
        /// <param name="propertyAlias">The alias of the property.</param>
        /// <returns>An instance of <see cref="LinkPickerList"/>.</returns>
        public static LinkPickerList GetLinkPickerList(this IPublishedContent content, string propertyAlias) {
            return content?.GetPropertyValue<LinkPickerList>(propertyAlias) ?? new LinkPickerList {
                Items = new LinkPickerItem[0]
            };
        }

    }

}