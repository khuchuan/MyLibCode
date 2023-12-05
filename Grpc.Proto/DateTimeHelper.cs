using Google.Protobuf.WellKnownTypes;

namespace Helper
{
    public static class DateTimeHelper
    {
        public static DateTime ToLocalTime(this Timestamp timestamp)
        {
            if (timestamp == null)
            {
                return DateTime.MinValue;
            }
            return timestamp.ToDateTime().ToLocalTime();
        }


        public static Timestamp? ToUTCTime(this DateTime? timestamp)
        {
            if (timestamp == null)
            {
                return null;
            }
            return Timestamp.FromDateTime(timestamp.Value.ToUniversalTime());
        }
        public static Timestamp ToUTCTime(this DateTime timestamp)
        {
            return Timestamp.FromDateTime(timestamp.ToUniversalTime());
        }
    }
}
