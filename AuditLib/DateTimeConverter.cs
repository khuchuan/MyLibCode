using System.Text.Json;
using System.Text.Json.Serialization;

namespace AuditLib
{
    public sealed class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString()!);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            string formatted = FormattableString.Invariant($"new Date({((DateTimeOffset)value.ToUniversalTime()).ToUnixTimeMilliseconds()})");
            writer.WriteRawValue(formatted, true);
        }
    }
}
