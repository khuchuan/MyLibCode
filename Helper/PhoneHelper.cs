using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Helper
{
    public static class PhoneHelper
    {

        private static readonly IReadOnlyDictionary<string, Regex> _dicPattern = new Dictionary<string, Regex>
            {
                //Viettel: 8498, 098, 8497, 097, 8496, 096, 8486, 032, 033, 034, 035, 036, 037, 038, 039
                {  Constants.TelcoName.Viettel, new Regex("^\\+?(84|0)?((86|9[678]|3[23456789])\\d{7})$",RegexOptions.Compiled | RegexOptions.ExplicitCapture) },
                //Vinaphone: 8491, 091, 8494, 094, 081, 082, 083, 084, 085, 088
                {  Constants.TelcoName.Vinaphone,new Regex( "^\\+?(84|0)?((88|9[14]|8[12345])\\d{7})$",RegexOptions.Compiled | RegexOptions.ExplicitCapture)},
                //Mobifone: 8490, 8493, 090, 093, 070, 076, 077, 078, 079
                {Constants.TelcoName.Mobifone, new Regex("^\\+?(84|0)((89|9[03]|7[06789])\\d{7}|1210\\d{6})$",RegexOptions.Compiled | RegexOptions.ExplicitCapture) },
                //Vietnamobile: 092, 052, 056, 058
                { Constants.TelcoName.Vietnamobile, new Regex("^\\+?(84|0)?((92|5[268])\\d{7})$" ,RegexOptions.Compiled | RegexOptions.ExplicitCapture)},
                //GTel: 099, 059
                { Constants.TelcoName.GTel, new Regex("^\\+?(84|0)?((99|59)\\d{7})$",RegexOptions.Compiled| RegexOptions.ExplicitCapture ) },
                //I-Telecom: 087
                {Constants.TelcoName.ITelecom, new Regex("^\\+?(84|0)?((87)\\d{7})$",RegexOptions.Compiled| RegexOptions.ExplicitCapture ) }
            };
        private static readonly Regex _digitsOnly = new(@"[^\d]", RegexOptions.Compiled);
        public static string FormatDigitsOnly(this string phoneNum)
        {
            return _digitsOnly.Replace(phoneNum, "");
        }
        public static string To84xx(this string phoneNum)
        {
            var phone = phoneNum.FormatDigitsOnly();
            var phoneSpan = phone.AsSpan();
            if (phoneSpan.StartsWith("0") && phoneSpan.Length >= 10)
            {
                return string.Create(phone.Length + 1, phone, (chars, str) =>
                {
                    chars[0] = '8';
                    chars[1] = '4';
                    str.AsSpan(1).CopyTo(chars[2..]);
                });
            }
            else if ((!phoneSpan.StartsWith("84") && phoneSpan.Length >= 9) || (phoneSpan.StartsWith("84") && phoneSpan.Length == 9))
            {
                return string.Create(phone.Length + 2, phone, (chars, str) =>
                {
                    chars[0] = '8';
                    chars[1] = '4';
                    str.AsSpan().CopyTo(chars[2..]);
                });
            }
            else
            {
                return phone;
            }
        }

        public static string To0xx(this string phoneNum)
        {
            var phone = phoneNum.FormatDigitsOnly();
            var phoneSpan = phone.AsSpan();
            if (phoneSpan.StartsWith("84") && phoneSpan.Length >= 11)
            {
                return string.Create(phone.Length - 1, phone, (chars, str) =>
                {
                    chars[0] = '0';
                    str.AsSpan(2).CopyTo(chars[1..]);
                });
            }
            return phone;
        }

        public static string GetTelcoNameFromPhone(this string sPhoneNum)
        {
            foreach (KeyValuePair<string, Regex> kvp in _dicPattern)
            {
                if (kvp.Value.IsMatch(sPhoneNum))
                    return kvp.Key;
            }

            return string.Empty;
        }
        public static bool IsPhoneNumberNew(this string sPhoneNum)
        {
            foreach (KeyValuePair<string, Regex> kvp in _dicPattern)
            {
                if (kvp.Value.IsMatch(sPhoneNum))
                    return true;
            }
            return false;
        }
        public static string GetSubString(this string input, int length)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            if (input.Length < length) return input;

            return input[..length];
        }
    }

    public sealed class PhoneNumberValidator : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var phoneNumber = value?.ToString();
            if (phoneNumber != null && phoneNumber.IsPhoneNumberNew())
            {
                return true;
            }
            return false;
        }
        //protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        //{
        //    var phoneNumber = value?.ToString();
        //    if (phoneNumber != null && phoneNumber.IsPhoneNumberNew())
        //    {
        //        return null;
        //    }
        //    return new ValidationResult("Số điện thoại không hợp lệ", new[] { validationContext.MemberName ?? string.Empty });
        //}
    }
}
