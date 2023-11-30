using Google.Protobuf.WellKnownTypes;
using System.ComponentModel.DataAnnotations;

namespace Helper
{
    public static class DateTimeHelper
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
        public static DateTime? ToDateTimeNullable(Timestamp t)
        {
            if (t == null)
            {
                return null;
            }
            var value = DateTimeOffset.FromUnixTimeSeconds(t.Seconds).LocalDateTime;
            if (value == new DateTimeOffset().LocalDateTime)
            {
                return null;
            }
            return value;
        }
        public static DateTime? ToDateTime(string t)
        {
            if (string.IsNullOrEmpty(t))
            {
                return null;
            }
            var value = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(t)).LocalDateTime;
            if (value == new DateTimeOffset().LocalDateTime)
            {
                return null;
            }
            return value;
        }
        public class DateRangeValidator : ValidationAttribute
        {
            public int MaxDays { get; set; }
            public DateRangeValidator(int maxDays) : base("Your Error Message goes here")
            {
                MaxDays = maxDays;
            }
            protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            {
                if (value is DateTime?[] dateRange && dateRange.Length == 2 && dateRange[0] != null && dateRange[1] != null)
                {
                    var durationDays = dateRange[1] - dateRange[0];
                    if (durationDays!.Value.TotalDays + 1 > MaxDays)
                        return new ValidationResult($"Khoảng thời gian tối đa {MaxDays} ngày", new[] { validationContext.MemberName ?? string.Empty });
                    else
                        return null;
                }
                return new ValidationResult("Khoảng thời gian không hợp lệ", new[] { validationContext.MemberName ?? string.Empty });
            }


        }
    }
}
