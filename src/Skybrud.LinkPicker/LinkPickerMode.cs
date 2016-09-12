using Newtonsoft.Json;
using Skybrud.LinkPicker.Json.Converters;

namespace Skybrud.LinkPicker {
    
    /// <summary>
    /// Enum describing the type of the link.
    /// </summary>
    [JsonConverter(typeof(LinkPickerEnumConverter))]
    public enum LinkPickerMode {
    
        /// <summary>
        /// Describes a link that is an external URL.
        /// </summary>
        Url,

        /// <summary>
        /// Describes a link that is a reference to an internal content node in Umbraco.
        /// </summary>
        Content,

        /// <summary>
        /// Describes a link that is a reference to an internal media node in Umbraco.
        /// </summary>
        Media
    
    }

}