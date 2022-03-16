using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Globalization;
using System.Text.Json;

namespace AMWService.Converters
{
    public class DateConverter : JsonConverter<DateTime>
    {
        private string formatDate = "dd/MM/yyyy HH:mm";
        public override DateTime Read(ref Utf8JsonReader reader, Type
            typeToConvert,JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), formatDate,
                CultureInfo.InvariantCulture);
        }
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(formatDate));
        }
    }
}
