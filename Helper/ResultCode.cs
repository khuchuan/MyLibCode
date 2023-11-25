using System.Reflection;

namespace Helper
{
    public static class ResultCode
    {

        public static class Status
        {
            public const int Success = 9;
            public const int Failed = 7;
            public const int Timeout = 99;
            public const int Pending = 3;
            public const int NotProcessed = 0;
        }
        public static List<string?> ListStatus()
        {
            List<string?> staticconstList = typeof(Status).GetFields().Select(x => x.GetValue(null)?.ToString()).ToList();
            return staticconstList;
        }
        public static class Color
        {
            public const int Green = 9;
            public const int Red = 7;
            public const int Orange = 99;
        }
        public static class Message
        {
            public const string BadRequest = "Bad Request";
            public const string InternalServerError = "Internal error";
            public const string Success = "Success";
            public const string Pending = "Pending";
            public const string Failed = "Failed";
            public const string Accepted = "Accepted";
            public const string Decline = "Decline";
            public const string InvalidTrackingId = "Invalid TrackingId";
            public const string UserIdError = "User Error";
            public const string InvalidEmail = "Email Error";
            public const string InvalidPackCode = "PackId Error";
            public const string InvalidClientIp = "IP Client Error";
            public const string InvalidSignature = "Signature Error";
            public const string InvalidPhoneNumber = "Số điện thoại không hợp lệ";
            public const string InvalidProductCode = "ProductCode Error";
            public const string InvalidServiceId = "ServiceId Error";
            public const string ChannelIsLocked = "Channel Is Locked";
            public const string InvalidTraceNumber = "TraceNumber Error";
            public const string UnknownException = "Internal Error";
            public const string Exception = "Internal Error";
            public const string NotEnoughMoney = "Not enough money";
            public const string WalletResponseError = "Wallet Error";
            public const string WalletTimeout = "Wallet Timeout";
            public const string InternalSystemError = "System Error";
            public const string ProviderResponseError = "Provider Response Error";
            public const string Timeout = "Timeout";
            public const string Duplicate = "Transaction Duplicate";
            //public const string InvalidProduct = "Invalid Product";
            public const string InvalidAmount = "Invalid Amount";
            public const string NotInTemplate = "Not In Template";
            public const string InvalidPage = "Invalid Page";
            public const string InvalidSize = "Invalid Size";
            public const string ConnectChannelError = "Connect Channel Error";
            public const string ConnectGatewayError = "Connect Gateway Error";
            public const string NotEnoughSoftpin = "Not Enough Softpin";
            public const string NotFoundTransaction = "Not Found Transaction";
            public const string CanNotLogin = "Can Not Login";
            public const string GatewayException = "Gateway Exception";
            public const string NotHavePermission = "Not Have Permission";
            public const string DBError = "Database Error";
            public const string InvalidChannel = "Invalid Channel";
            public const string ProviderTimeout = "Provider Timeout";
            public const string DeserializeError = "Deserialize Error";
            public const string NotFoundProviderTransaction = "Not Found Provider Transaction";
            public const string CheckBillError = "Check bill error";
            public const string UnknownStatus = "Unknown Status";
            public const string AmountChanged = "Amount Changed";
            public const string ChannelNotSupportCheckBill = "Channel Don't Support CheckBill";
            public const string CheckBalanceError = "Check Balance Error";
            public const string ViettelReferenceCodeErrror = "Viettel ReferenceCode Errror";
            public const string InvalidTransactionId = "Invalid TransactionId";
            public const string MovePending = "Failed";
            public const string AmountNotInRange = "Under Allowable Amount";
            public const string InvalidBillingCode = "BillingCode Error";
            public const string InvalidPartner = "Invalid Partner";
            public const string NotFoundItem = "Not Found";

            public const string RequestSupportAlreadyExits = "Yêu cầu hỗ trợ đã tồn tại";
            public const string InvalidMessage = "Nội dung tối thiểu 16 ký tự tối đa 500 ký tự";
            public const string InvalidNote = "Ghi chú tối đa 500 ký tự";
            public const string InvalidStatus = "Trạng thái không hợp lệ";
            public const string InvalidName = "Tên không hợp lệ";
            public const string InvalidDescription = "Mô tả không hợp lệ";
            public const string InvalidCampaignType = "Loại chiết khấu không hợp lệ";
            public const string InvalidCode = "Code không hợp lệ";
            public const string ExistRefundOrder = "Đã tồn tại yêu cầu hoàn tiền";
            public const string ExistSendMail = "Đã gửi mail";
            public const string SendMailPending = "Đang trong quá trình gửi mail";

        }

        public static class Code
        {
            public const string Success = "00";
            public const string Timeout = "99";
            public const string Pending = "03";
            public const string BadRequest = "400";
            public const string InternalServerError = "500";
            public const string Accepted = "01";
            public const string InvalidEmail = "04";
            public const string Decline = "18";// insert log DB error
            public const string UserIdError = "15";
            public const string InvalidTrackingId = "09";
            public const string InvalidPackCode = "10";
            public const string InvalidClientIp = "17";
            public const string InvalidSignature = "11";
            public const string InvalidPhoneNumber = "13";
            public const string InvalidProductCode = "14";
            public const string InvalidServiceId = "12";
            public const string ChannelIsLocked = "16";
            public const string Exception = "19";
            public const string NotEnoughMoney = "21";
            public const string WalletResponseError = "22";
            public const string WalletTimeout = "25";
            public const string InternalSystemError = "23";
            public const string ProviderResponseError = "24";
            public const string Duplicate = "31";
            public const string NotHavePermission = "37";
            public const string NotFoundTransaction = "39";
            public const string NotInTemplate = "32";
            public const string ProviderTimeout = "43";
            public const string ConnectGatewayError = "34";

            public const string DBError = "41";
            public const string NotFoundProviderTransaction = "44";
            public const string InvalidAmount = "45";
            public const string CheckBillError = "46";
            public const string UnknownStatus = "47";
            public const string AmountChanged = "48";

            public const string ChannelNotSupportCheckBill = "50";
            public const string CheckBalanceError = "51";
            public const string MovePending = "54";
            public const string AmountNotInRange = "55";
            public const string InvalidBillingCode = "56";
            public const string InvalidPartner = "57";
            public const string InvalidPage = "58";
            public const string InvalidSize = "59";
            public const string NotFoundItem = "60";
            public const string RequestSupportAlreadyExits = "61";
            public const string InvalidMessage = "62";
            public const string InvalidStatus = "63";

            public const string InvalidName = "64";
            public const string InvalidDescription = "65";
            public const string InvalidCampaignType = "66";
            public const string InvalidCode = "67";
            public const string ExistRefundOrder = "68";
            public const string ExistSendMail = "69";
            public const string SendMailPending = "70";
            

        }
        public static int GetStatus(string? code)
        {
            return code switch
            {
                Code.Success => Status.Success,
                Code.Timeout or Code.WalletTimeout or Code.ProviderTimeout or Code.AmountChanged or null => Status.Timeout,
                _ => Status.Failed,
            };
        }
        public static string GetMessage(string code)
        {
            try
            {
                var name = typeof(Code).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                    .Single(fi => fi.IsLiteral && !fi.IsInitOnly && fi.GetValue(null)!.ToString() == code).Name;
                return typeof(Message).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                    .Single(fi => fi.IsLiteral && !fi.IsInitOnly && fi.Name == name).GetRawConstantValue()!.ToString()!;
            }
            catch
            {
                return "Unknown Error Code";
            }
        }

        public static string GetStatusMessage(int? status)
        {
            try
            {
                var name = typeof(Status).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                    .Single(fi => fi.IsLiteral && !fi.IsInitOnly && fi.GetValue(null)!.ToString() == status.ToString()).Name;
                return typeof(Message).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                    .Single(fi => fi.IsLiteral && !fi.IsInitOnly && fi.Name == name).GetRawConstantValue()!.ToString()!;
            }
            catch
            {
                return "Unknown Status";
            }
        }

        public static string GetColor(int status)
        {
            try
            {
                return typeof(Color).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                  .Single(fi => fi.IsLiteral && !fi.IsInitOnly && fi.GetValue(null)?.ToString() == status.ToString()).Name;
            }
            catch
            {
                return "Unknown Error Code";
            }
        }

    }

}
