using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;

namespace Skybrud.LinkPicker
{

    internal class LinkPickerPropertyValueConverter : PropertyValueConverterBase, IPropertyValueConverterMeta
    {

        public override bool IsConverter(PublishedPropertyType propertyType)
        {
            return propertyType.PropertyEditorAlias == "Skybrud.LinkPicker";
        }

        public override object ConvertDataToSource(PublishedPropertyType propertyType, object data, bool preview)
        {
            return LinkPickerList.Deserialize(data as string);
        }

        public override object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview)
        {
            return source as LinkPickerList;
        }

        public override object ConvertSourceToXPath(PublishedPropertyType propertyType, object source, bool preview)
        {
            return null;
        }

        public PropertyCacheLevel GetPropertyCacheLevel(PublishedPropertyType propertyType, PropertyCacheValue cacheValue)
        {
            PropertyCacheLevel level;

            switch (cacheValue)
            {
                case PropertyCacheValue.Object:
                    level = PropertyCacheLevel.ContentCache;
                    break;
                case PropertyCacheValue.Source:
                    level = PropertyCacheLevel.Content;
                    break;
                case PropertyCacheValue.XPath:
                    level = PropertyCacheLevel.Content;
                    break;
                default:
                    level = PropertyCacheLevel.None;
                    break;
            }

            return level;
        }

        public System.Type GetPropertyValueType(PublishedPropertyType propertyType)
        {
            return typeof(LinkPickerList);
        }
    }

}