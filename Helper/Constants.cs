using System.Reflection;

namespace Helper
{
    public static class Constants
    {
        public static class Partner
        {
            public const string MBBank = "MBBANK";
            public const string Sacombank = "SACOMBANK";
        }

        public static List<string?> GetListPartner()
        {
            List<string?> staticconstList = typeof(Partner).GetFields().Select(x => x.GetValue(null)?.ToString()).ToList();
            return staticconstList;
        }

        public static class TelcoName
        {
            public const string Vinaphone = "Vinaphone";
            public const string Viettel = "Viettel";
            public const string Mobifone = "Mobifone";
            public const string Vietnamobile = "Vietnamobile";
            public const string GTel = "GTel";
            public const string ITelecom = "ITelecom";
            public const string Reddi = "Reddi";
        }

        public static List<string?> GetListTelcoName()
        {
            List<string?> staticconstList = typeof(TelcoName).GetFields().Select(x => x.GetValue(null)?.ToString()).ToList();
            return staticconstList;
        }
        public static class ServiceType
        {
            public const string TOPUP = "TOPUP";
            public const string CARD = "CARD";
            public const string COMBO = "COMBO";
        }
        public static class Service
        {
            public const string TOPUPDATA = "DATA";
            public const string PHONECARD = "PHONECARD";
            public const string BILL = "BILL";
            public const string POSTPAID = "POSTPAID";
            public const string TOPUP = "TOPUP";
            public const string COMBO = "COMBO";
            public const string GAMECARD = "GAMECARD";
            public const string DATACARD = "DATACARD";
        }

        public static class ServiceLabel
        {
            public const string POSTPAID = "Thuê bao trả sau";
            public const string BILL = "Hóa đơn";
            public const string TOPUPDATA = "Nạp Data";
            public const string DATACARD = "Thẻ Data";
            public const string PHONECARD = "Thẻ điện thoại";
            public const string TOPUP = "Nạp điện thoại";
            public const string COMBO = "Combo";
            public const string GAMECARD = "Thẻ Game 365";
        }


        public static string GetServiceLabel(this string code)
        {
            try
            {
                var name = typeof(Service).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                    .Single(fi => fi.IsLiteral && !fi.IsInitOnly && fi.GetValue(null)!.ToString() == code).Name;
                return typeof(ServiceLabel).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                    .Single(fi => fi.IsLiteral && !fi.IsInitOnly && fi.Name == name).GetRawConstantValue()!.ToString()!;
            }
            catch
            {
                return "Unknown Service";
            }
        }

        public static List<string?> GetListService()
        {
            List<string?> staticconstList = typeof(Service).GetFields().Select(x => x.GetValue(null)?.ToString()).ToList();
            return staticconstList;
        }

        public static Dictionary<string, string> GetListProduct()
        {
            Dictionary<string, string> staticconstList = new Dictionary<string, string>();
            foreach (var service in GetListService())
            {
                foreach (var telco in GetListTelcoName())
                {
                    var key = $"{service?.ToUpper()}_{telco?.ToUpper()}";
                    var label = $"{GetServiceLabel(service)} {telco}";
                    staticconstList.Add(key, label);
                }
            }
            return staticconstList;
        }

        public static string GetProductLabel(string product, string service = "")
        {
            string? label = "";
            switch (service)
            {
                case Service.TOPUP:
                    GetListProduct().TryGetValue($"{service}_{product}", out label);
                    break;
                case Service.TOPUPDATA:
                case Service.COMBO:
                    GetListProduct().TryGetValue(product, out label);
                    break;
                default:
                    GetListProduct().TryGetValue($"{service}_{product}", out label);
                    break;
            }
            return string.IsNullOrEmpty(label) ? "Unknown Product" : label;
        }

        public static class RequestSupportStatus
        {
            public const int ChoXuLy = 1;
            public const int DangXuLy = 2;
            public const int DaXuLy = 3;
        }
        public static class RequestSupportStatusLabel
        {
            public const string ChoXuLy = "Chờ xử lý";
            public const string DangXuLy = "Tiếp nhận";
            public const string DaXuLy = "Hoàn thành";
        }

        public static string GetRequestSupportStatusLabel(int code)
        {
            try
            {
                var name = typeof(RequestSupportStatus).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                    .Single(fi => fi.IsLiteral && !fi.IsInitOnly && fi.GetValue(null)!.ToString() == code.ToString()).Name;
                return typeof(RequestSupportStatusLabel).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                    .Single(fi => fi.IsLiteral && !fi.IsInitOnly && fi.Name == name).GetRawConstantValue()!.ToString()!;
            }
            catch
            {
                return "Unknown Status";
            }
        }

        public static class PromotionCampaign

        {
            public static class Status
            {
                public const int ChuaApDung = -1;
                public const int Tat = 0;
                public const int DangApDung = 1;
                public const int DaApDung = 2;
                public const int TamNgung = 3;
            }

            public static class Label
            {
                public const string Tat = "Chưa kích hoạt";
                public const string ChuaApDung = "Chưa áp dụng";
                public const string DangApDung = "Đang áp dụng";
                public const string DaApDung = "Đã áp dụng";
                public const string TamNgung = "Tạm ngưng";
            }
            public static string GetMessage(int code)
            {
                try
                {
                    var name = typeof(Status).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                        .Single(fi => fi.IsLiteral && !fi.IsInitOnly && fi.GetValue(null)!.ToString() == code.ToString()).Name;
                    return typeof(Label).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                        .Single(fi => fi.IsLiteral && !fi.IsInitOnly && fi.Name == name).GetRawConstantValue()!.ToString()!;
                }
                catch
                {
                    return "Unknown Status";
                }
            }

            public static class Active
            {
                public const int Tat = 0;
                public const int Bat = 1;
                public const int TamNgung = 2;
            }

            public static class ActiveLabel
            {
                public const string Tat = "Tắt";
                public const string Bat = "Bật";
                public const string TamNgung = "Tạm ngưng";
            }

            public static string GetActiveMessage(int code)
            {
                try
                {
                    var name = typeof(Status).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                        .Single(fi => fi.IsLiteral && !fi.IsInitOnly && fi.GetValue(null)!.ToString() == code.ToString()).Name;
                    return typeof(Label).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                        .Single(fi => fi.IsLiteral && !fi.IsInitOnly && fi.Name == name).GetRawConstantValue()!.ToString()!;
                }
                catch
                {
                    return "Unknown Status";
                }
            }
        }

        public enum DiscountType
        {
            PhanTram = 0,
            CoDinh = 1
        }

        public static class ResSendMail
        {
            public static class Status
            {
                public const int UnSent = 0;
                public const int Pending = -1;
                public const int Success = 1;
                public const int Faile = 2;
            }


            public static class Label
            {
                public const string UnSent = "Chưa gửi";
                public const string Pending = "Đang gửi";
                public const string Success = "Thành công";
                public const string Faile = "Thất bại";
            }

            public static string GetMessage(int code)
            {
                try
                {
                    var name = typeof(Status).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                        .Single(fi => fi.IsLiteral && !fi.IsInitOnly && fi.GetValue(null)!.ToString() == code.ToString()).Name;
                    return typeof(Label).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                        .Single(fi => fi.IsLiteral && !fi.IsInitOnly && fi.Name == name).GetRawConstantValue()!.ToString()!;
                }
                catch
                {
                    return "Unknown Status";
                }
            }
        }

    }


}
