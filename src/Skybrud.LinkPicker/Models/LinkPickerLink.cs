using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;

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

        /// <summary>
        /// Initializes a new link from the specified parameters.
        /// </summary>
        /// <param name="id">The numeric ID of the content or media item of the link.</param>
        /// <param name="udi">The UDI of the content or media item of the link.</param>
        /// <param name="name">The name of the link.</param>
        /// <param name="url">The URL of the link.</param>
        /// <param name="type">The type of the link.</param>
        /// <param name="target">The target value of the link.</param>
        /// <param name="anchor">The anchor value of the link.</param>
        public LinkPickerLink(int id, string udi, string name, string url, LinkPickerType type, string target, string anchor) {
            Id = id;
            Udi = udi;
            Name = name;
            Url = url;
            Type = type;
            Target = target;
            Anchor = anchor;
        }

        /// <summary>
        /// Initializes a new link the specified content or media <paramref name="item"/>.
        /// </summary>
        /// <param name="item">An instance of <see cref="IPublishedContent"/> representing a content or media item.</param>
        /// <param name="name">The name of the link.</param>
        /// <param name="target">The target value of the link.</param>
        /// <param name="anchor">The anchor value of the link.</param>
        public LinkPickerLink(IPublishedContent item, string name, string target, string anchor) {
            
            if (item == null) throw new ArgumentNullException(nameof(item));

            switch (item.ItemType) {

                case PublishedItemType.Content:
                    Id = item.Id;
                    Udi = new GuidUdi("document", item.Key).ToString();
                    Name = name ?? item.Name;
                    Url = item.Url;
                    Type = LinkPickerType.Content;
                    Target = target;
                    Anchor = anchor;
                    break;

                case PublishedItemType.Media:
                    Id = item.Id;
                    Udi = new GuidUdi("media", item.Key).ToString();
                    Name = name ?? item.Name;
                    Url = item.Url;
                    Type = LinkPickerType.Media;
                    Target = target;
                    Anchor = anchor;
                    break;

                default:
                    throw new ArgumentException($"Published item type {item.ItemType} is not supported.", nameof(item));

            }

        }

        #endregion

    }

}