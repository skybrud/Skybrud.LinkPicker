﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.LinkPicker.PropertyEditors;
using Superpower;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Composing;

namespace Skybrud.LinkPicker.Models {

    public class LinkPickerLink {

        #region Properties

        [JsonProperty("id")]
        public int Id { get; }

        [JsonIgnore]
        public Guid Key { get; }

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

        public LinkPickerLink(JObject obj) : this(obj, Current.UmbracoContext, null) { }

        internal LinkPickerLink(JObject obj, UmbracoContext context, LinkConfiguration config) {

            // TODO: Should "Udi" property be of type Udi?

            Id = obj.GetInt32("id");
            Udi = obj.GetString("udi");
            Name = obj.GetString("name");
            Url = obj.GetString("url");
            Type = obj.GetEnum("type", LinkPickerType.Url);

            Key = string.IsNullOrWhiteSpace(Udi) ? Guid.Empty : GuidUdi.Parse(Udi).Guid;

            switch (Type) {

                // TODO: Update "Name" property as well if permitted by "config"

                case LinkPickerType.Content: {
                    var c = context?.Content.GetById(Key);
                    if (c != null) Url = c.Url;
                    break;
                }

                case LinkPickerType.Media: {
                    var m = context?.Media.GetById(Key);
                    if (m != null) Url = m.Url;
                    break;
                }

            }

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
            if (GuidUdi.TryParse(udi, out GuidUdi guidUdi)) Key = guidUdi.Guid;
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
                    Key = item.Key;
                    Udi = new GuidUdi("document", item.Key).ToString();
                    Name = name ?? item.Name;
                    Url = item.Url;
                    Type = LinkPickerType.Content;
                    Target = target;
                    Anchor = anchor;
                    break;

                case PublishedItemType.Media:
                    Id = item.Id;
                    Key = item.Key;
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

        #region Static methods
        
        /// <summary>
        /// Deseralizes the specified JSON string into an instance of <see cref="LinkPickerLink"/>.
        /// </summary>
        /// <param name="json">The raw JSON to be parsed.</param>
        public static LinkPickerLink Deserialize(string json) {
            if (json == null) return null;
            if (json.StartsWith("{") && json.EndsWith("}")) return JsonUtils.ParseJsonObject(json, x => new LinkPickerLink(x));
            return null;
        }

        #endregion

    }

}