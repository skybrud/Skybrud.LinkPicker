using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;

namespace Skybrud.LinkPicker.Models {

    public class LinkPickerLink {

        #region Properties

        [JsonProperty("id")]
        public int Id { get; }

        [JsonIgnore]
        public string Udi { get; }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("url")]
        public string Url { get; }

        [JsonProperty("type")]
        public LinkPickerType Type { get; }

        [JsonProperty("target", NullValueHandling = NullValueHandling.Ignore)]
        public string Target { get; }

        [JsonProperty("anchor", NullValueHandling = NullValueHandling.Ignore)]
        public string Anchor { get; }

        #endregion

        #region Constructors

        public LinkPickerLink(JObject obj) {
            Id = obj.GetInt32("id");
            Udi = obj.GetString("udi");
            Name = obj.GetString("name");
            Url = obj.GetString("url");
            Type = obj.GetEnum("type", LinkPickerType.Url);
            Target = obj.GetString("target");
            Anchor = obj.GetString("anchor");
        }

        #endregion

    }

}