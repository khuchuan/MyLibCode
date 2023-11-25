using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Helper
{
    public static class DateTimeExtend
    {
        public static DateTime EndOfDay(DateTime t)
        {
            return t.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);
        }

        public static DateTime FirstDayOfMonth(DateTime t)
        {
            return new DateTime(t.Year, t.Month, 1);
        }
        public static DateTime LastDayOfMonthStartTime(DateTime t)
        {
            return FirstDayOfMonth(t).AddMonths(1).AddDays(-1);
        }
        public static DateTime LastDayOfMonthEndTime(DateTime t)
        {
            return EndOfDay(FirstDayOfMonth(t).AddMonths(1).AddDays(-1));
        }
        public static DateTime FirstDayOfYear(DateTime t)
        {
            return new DateTime(t.Year, 1, 1);
        }
        public static DateTime LastDayOfYearStartTime(DateTime t)
        {
            return new DateTime(t.Year, 12, 31);
        }
        public static DateTime LastDayOfYearEndTime(DateTime t)
        {
            return EndOfDay(new DateTime(t.Year, 12, 31));
        }

        public static DateTime DateTimeFromTimeOnly(string timeOnly)
        {
            DateTime.TryParse(timeOnly, out var today);
            return today;
        }
    }

    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private const string Format = "yyyy-MM-dd";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateOnly.ParseExact(reader.GetString(), Format, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
        }
    }

    public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
    {
        private const string Format = "HH:mm";

        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return TimeOnly.ParseExact(reader.GetString(), Format, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
        }
    }
}
