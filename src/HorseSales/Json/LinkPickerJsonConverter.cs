using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using HorseSales.Models;

namespace HorseSales.Json
{
    /// <summary>
    /// JSON coverter for serializing instances of either <see cref="LinkPickerList"/> or <see cref="LinkPickerItem"/>.
    /// </summary>
    public class LinkPickerJsonConverter : JsonConverter
    {
        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

            LinkPickerItem item = value as LinkPickerItem;

            if (item != null)
            {

                serializer.Serialize(writer, item.JObject);

            }

        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {

            // Skip if the reader is not at the start of an object
            if (reader.TokenType != JsonToken.StartObject) return null;

            // Load JObject from stream
            JObject obj = JObject.Load(reader);

            switch (objectType.FullName)
            {

                case "HorseSales.Models.LinkPickerList":
                    return LinkPickerList.Parse(obj);

                case "HorseSales.Models.LinkPickerItem":
                    return LinkPickerItem.Parse(obj);

                default:
                    return null;

            }

        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(LinkPickerItem);
        }
    }

}