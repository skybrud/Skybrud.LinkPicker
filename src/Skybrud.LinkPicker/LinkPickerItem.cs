using System;
using AutoMapper.Mappers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.LinkPicker.Extensions.Json;
using Skybrud.LinkPicker.Json.Converters;
using umbraco.cms.helpers;
using Umbraco.Core.Models;

namespace Skybrud.LinkPicker {

    /// <summary>
    /// Class representing a single link item.
    /// </summary>
    public class LinkPickerItem {

        #region Properties

        /// <summary>
        /// Gets a reference to the <see cref="JObject"/> the item was parsed from.
        /// </summary>
        [JsonIgnore]
        public JObject JObject { get; private set; }

        /// <summary>
        /// Gets the ID of the selected content or media. If an URL has been selected, this will return <code>0</code>.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; private set; }

        /// <summary>
        /// Gets the name of the link.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; private set; }

        /// <summary>
        /// Gets the URL of the link.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; private set; }

        /// <summary>
        /// Gets the link target.
        /// </summary>
        [JsonProperty("target")]
        public string Target { get; private set; }

        /// <summary>
        /// Gets the mode (or type) of the link.
        /// </summary>
        [JsonProperty("mode")]
        public LinkPickerMode Mode { get; private set; }

        /// <summary>
        /// Gets whether the link is valid.
        /// </summary>
        [JsonIgnore]
        public bool IsValid {
            get { return !String.IsNullOrWhiteSpace(Url); }
        }

        #endregion

        #region Constructors

        internal LinkPickerItem() { }

        /// <summary>
        /// Initializes a new link picker item.
        /// </summary>
        /// <param name="id">The ID of the content or media item.</param>
        /// <param name="name">The name (text) of the link.</param>
        /// <param name="url">The URL of the link.</param>
        /// <param name="target">The target of the link.</param>
        /// <param name="mode">The mode of the link - either <see cref="LinkPickerMode.Content"/>,
        /// <see cref="LinkPickerMode.Media"/> or <see cref="LinkPickerMode.Url"/>.</param>
        public LinkPickerItem(int id, string name, string url, string target, LinkPickerMode mode) {
            Id = id;
            Name = name;
            Url = url;
            Target = target;
            Mode = mode;
        }

        #endregion

        #region Static methods
        
        /// <summary>
        /// Parses the specified <code>obj</code> into an instance of <code>LinkPickerItem</code>.
        /// </summary>
        /// <param name="obj">The instance of <code>JObject</code> to be parsed.</param>
        public static LinkPickerItem Parse(JObject obj) {

            if (obj == null) return null;

            // Get the ID
            int id = obj.GetInt32("id");

            // Parse the link mode
            LinkPickerMode mode;
            if (obj.GetValue("mode") == null) {
                if (obj.GetBoolean("isMedia")) {
                    mode = LinkPickerMode.Media;
                } else if (id > 0) {
                    mode = LinkPickerMode.Content;
                } else {
                    mode = LinkPickerMode.Url;
                }
            } else {
                mode = (LinkPickerMode) Enum.Parse(typeof(LinkPickerMode), obj.GetString("mode"), true);
            }
            
            // Parse remaining properties
            return new LinkPickerItem {
                JObject = obj,
                Id = id,
                Name = obj.GetString("name"),
                Url = obj.GetString("url"),
                Target = obj.GetString("target"),
                Mode = mode
            };

        }

        public static LinkPickerItem GetFromContent(IPublishedContent content) {
            if (content == null) throw new ArgumentNullException("content");
            return new LinkPickerItem {
                Id = content.Id,
                Name = content.Name,
                Url = content.Url,
                Mode = LinkPickerMode.Content
            };
        }

        public static LinkPickerItem GetFromMedia(IPublishedContent media) {
            if (media == null) throw new ArgumentNullException("media");
            return new LinkPickerItem {
                Id = media.Id,
                Name = media.Name,
                Url = media.Url,
                Mode = LinkPickerMode.Media
            };
        }

        public static LinkPickerItem GetFromUrl(string url, string name = null, string target = null) {
            if (String.IsNullOrWhiteSpace(url)) throw new ArgumentNullException("url");
            return new LinkPickerItem {
                Name = name + "",
                Url = url,
                Mode = LinkPickerMode.Url
            };
        }

        #endregion

    }

}