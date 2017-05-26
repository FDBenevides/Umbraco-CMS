using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace HorseSales.Models
{

    /// <summary>
    /// Class representing a single link item.
    /// </summary>
    public class LinkPickerItemComment
    {

        #region Properties

        /// <summary>
        /// Gets a reference to the <see cref="JObject"/> the item was parsed from.
        /// </summary>
        [JsonIgnore]
        public JObject JObject { get; set; }

        /// <summary>
        /// Gets the author of the comment.
        /// </summary>
        [JsonProperty("author")]
        public string Author { get; set; }

        /// <summary>
        /// Gets the text in the comment.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets the datetime of the comment .
        /// </summary>
        [JsonProperty("datetime")]
        public string Datetime { get; set; }

        #endregion

        #region Constructors

        internal LinkPickerItemComment() { }

        /// <summary>
        /// Initializes a new link picker item.
        /// </summary>
        public LinkPickerItemComment(string author, string datetime, string text)
        {
            Author = author;
            Datetime = datetime;
            Text = text;
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <code>obj</code> into an instance of <see cref="LinkPickerItemComment"/>.
        /// </summary>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        public static LinkPickerItemComment Parse(JObject obj)
        {

            if (obj == null) return null;

            // Parse remaining properties
            return new LinkPickerItemComment
            {
                JObject = obj,
                Author = obj.GetString("author"),
                Datetime = obj.GetString("datetime"),
                Text = obj.GetString("text")
            };

        }

        #endregion

    }

}