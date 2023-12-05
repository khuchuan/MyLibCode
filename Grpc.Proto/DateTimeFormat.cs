using Google.Protobuf.WellKnownTypes;

namespace Helper
{
    public static class DateTimeFormat
    {
        public static Timestamp ToTimestamp(DateTime d)
        {
            return Timestamp.FromDateTime(d.ToUniversalTime());
        }
        public static Timestamp? ToTimestamp(DateTime? d)
        {
            if (d == null)
            {
                return null;
            }
            else
            {
                return Timestamp.FromDateTime(d.Value.ToUniversalTime());
            }
        }

        public static DateTime ToDateTime(Timestamp t)
        {
            if (t == null)
            {
                return DateTime.MinValue;
            }
            return DateTimeOffset.FromUnixTimeSeconds(t.Seconds).LocalDateTime;
        }

    }
}
