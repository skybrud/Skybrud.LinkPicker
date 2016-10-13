using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Skybrud.LinkPicker {

    /// <summary>
    /// Class representing a single link item.
    /// </summary>
    public class LinkPickerItem {

        private string _url;

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
        /// Gets the URL of the link. Since the URL of a content or media item may change over time (eg. if renamed or
        /// moved), this property will attempt to retrieve the current URL from the relevant Umbraco cache.
        /// 
        /// If <see cref="Mode"/> is <see cref="LinkPickerMode.Content"/>, the URL of the content item will be
        /// retrieved through the content cache (if available). In a similar way, if <see cref="Mode"/> is
        /// <see cref="LinkPickerMode.Media"/> the URL of the media item will be retrieved through the media cache (if
        /// available). The original URL as saved in Umbraco can be accessed through the <see cref="RawUrl"/> property.
        /// </summary>
        [JsonProperty("url")]
        public string Url {
            get { return _url ?? (_url = GetCalculatedUrl()); }
        }

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

        /// <summary>
        /// Gets the raw URL as saved in Umbraco. The URL may be wrong if referencing content or media that has been renamed, moved or similar.
        /// </summary>
        [JsonIgnore]
        public string RawUrl { get; private set; }

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
            RawUrl = url;
            Target = target;
            Mode = mode;
        }

        #endregion

        #region Member methods

        protected virtual string GetCalculatedUrl() {

            // If we dont have a valid UmbracoContext (eg. during Examine indexing), we simply return the raw URL
            if (UmbracoContext.Current == null) return RawUrl;
            
            // Look up the actual URL for content and media
            switch (Mode) {
                case LinkPickerMode.Content: {
                    IPublishedContent content = UmbracoContext.Current.ContentCache.GetById(Id);
                    return content == null ? RawUrl : content.Url;
                }
                case LinkPickerMode.Media: {
                    IPublishedContent media = UmbracoContext.Current.MediaCache.GetById(Id);
                    return media == null ? RawUrl : media.Url;
                }
            }
            
            // Use the raw URL as a fallback
            return RawUrl;
        
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <code>obj</code> into an instance of <see cref="LinkPickerItem"/>.
        /// </summary>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
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
                RawUrl = obj.GetString("url"),
                Target = obj.GetString("target"),
                Mode = mode
            };

        }

        /// <summary>
        /// Initializes a new link picker item from an instance of <see cref="IPublishedContent"/> representing a content item.
        /// </summary>
        /// <param name="content">An instance of <see cref="IPublishedContent"/> representing a content item.</param>
        /// <returns>Returns the created <see cref="LinkPickerItem"/> instance.</returns>
        public static LinkPickerItem GetFromContent(IPublishedContent content) {
            if (content == null) throw new ArgumentNullException("content");
            return new LinkPickerItem {
                Id = content.Id,
                Name = content.Name,
                _url = content.Url,
                RawUrl = content.Url,
                Mode = LinkPickerMode.Content
            };
        }

        /// <summary>
        /// Initializes a new link picker item from an instance of <see cref="IPublishedContent"/> representing a media item.
        /// </summary>
        /// <param name="media">An instance of <see cref="IPublishedContent"/> representing a media item.</param>
        /// <returns>Returns the created <see cref="LinkPickerItem"/> instance.</returns>
        public static LinkPickerItem GetFromMedia(IPublishedContent media) {
            if (media == null) throw new ArgumentNullException("media");
            return new LinkPickerItem {
                Id = media.Id,
                Name = media.Name,
                _url = media.Url,
                RawUrl = media.Url,
                Mode = LinkPickerMode.Media
            };
        }

        /// <summary>
        /// Initializes a new link picker item from the specified <code>url</code>, <code>name</code> and <code>target</code>.
        /// </summary>
        /// <param name="url">The URL of the link.</param>
        /// <param name="name">The name (text) of the link.</param>
        /// <param name="target">The target of the link.</param>
        /// <returns>Returns the created <see cref="LinkPickerItem"/> instance.</returns>
        public static LinkPickerItem GetFromUrl(string url, string name = null, string target = null) {
            if (String.IsNullOrWhiteSpace(url)) throw new ArgumentNullException("url");
            return new LinkPickerItem {
                Name = name + "",
                RawUrl = url,
                Mode = LinkPickerMode.Url
            };
        }

        #endregion

    }

}