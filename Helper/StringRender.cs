using System.Text;

namespace Helper
{
    public static class StringRender
    {
        public static string RenderPackTimeLimitLabel(int packTimeLitmit)
        {
            if (packTimeLitmit > 0)
            {
                return $"{DateTime.Now:dd/MM/yyyy} - {DateTime.Now.AddDays(packTimeLitmit):dd/MM/yyyy}";
            }
            else
            {
                return DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        public static string FormatPhoneNumber(string phoneNum)
        {
            StringBuilder builder = new(phoneNum);
            builder = builder.Replace(" ", "").Replace("+", "").Replace("-", "").Replace(".", "").Replace("/", "").Replace(",", "").Replace(";", "").Replace(":", "").Replace("_", "");

            if (phoneNum[..2].Equals("84"))
            {
                builder = builder.Remove(0, 2).Insert(0, "0");
            }
            return builder.ToString();
        }

        public static string RenderPackNameLabel(int packTimeLitmit)
        {
            return packTimeLitmit switch
            {
                0 or 1 => "Ngày",
                30 => "Tháng",
                _ => $"{packTimeLitmit} Ngày",
            };
        }
        public static string RenderStatusString(int? status)
        {
            return status switch
            {
                ResultCode.Status.Success => "Thành công",
                ResultCode.Status.Failed => "Thất bại",
                ResultCode.Status.Timeout or ResultCode.Status.Pending => "Đang xử lý",
                _ => "",
            };
        }
        public static string RenderStatusClass(int? status)
        {
            return status switch
            {
                ResultCode.Status.Success => "success",
                ResultCode.Status.Failed => "failed",
                ResultCode.Status.Timeout => "pending",
                _ => "",
            };
        }

        public static string GenerateString()
        {
            return Guid.NewGuid().ToString();
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Shared.Next(s.Length)]).ToArray());
        }
    }
}
