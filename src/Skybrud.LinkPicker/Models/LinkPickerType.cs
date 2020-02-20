using Newtonsoft.Json;
using Skybrud.Essentials.Json.Converters.Enums;

namespace Skybrud.LinkPicker.Models {

    [JsonConverter(typeof(EnumLowerCaseConverter))]
    public enum LinkPickerType {
        Content,
        Media,
        Url
    }

}