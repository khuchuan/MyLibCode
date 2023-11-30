using System.Text;
using System.Text.RegularExpressions;
namespace Helper
{
    public static class TelcoHelper
    {
        static readonly Dictionary<string, string> _dicPattern;
        static readonly Regex _regexPattern = new Regex(@"\D+", RegexOptions.Compiled);
        static TelcoHelper()
        {
            _dicPattern = new Dictionary<string, string>
            {
                //Vinaphone: 8491, 091, 8494, 094, 081, 082, 083, 084, 085, 088
                { Constants.TelcoName.Vinaphone, "^\\+?(84|0)((88|9[14]|8[12345])\\d{7})$" },
                //Telco name, Regex Pattern
                //Viettel: 8498, 098, 8497, 097, 8496, 096, 8486, 032, 033, 034, 035, 036, 037, 038, 039
                { Constants.TelcoName.Viettel, "^\\+?(84|0)((86|9[678]|3[23456789])\\d{7})$" },
                //Telco name, Regex Pattern
                //Mobifone: 8490, 8493, 090, 093, 070, 076, 077, 078, 079
                { Constants.TelcoName.Mobifone, "^\\+?(84|0)((89|9[03]|7[06789])\\d{7})$" },
                //Telco name, Regex Pattern
                //Vietnamobile: 092, 052, 056, 058
                { Constants.TelcoName.Vietnamobile, "^\\+?(84|0)((92|5[268])\\d{7})$" },
                //Telco name, Regex Pattern
                //GTel: 099, 059
                { Constants.TelcoName.GTel, "^\\+?(84|0)((99|59)\\d{7})$" },
                //I-Telecom: 087
                { Constants.TelcoName.ITelecom, "^\\+?(84|0)((87)\\d{7})$" },

                { Constants.TelcoName.Reddi, "^\\+?(84|0)((55)\\d{7})$" }
            };
        }
        public static bool IsPhoneNumber(this string phoneNumber)
        {
            if (phoneNumber != null)
            {
                foreach (KeyValuePair<string, string> kvp in _dicPattern)
                {
                    if (Regex.IsMatch(phoneNumber, kvp.Value))
                        return true;
                }
            }

            return false;
        }
        public static string FormatPhoneNumber(this string phoneNum)
        {
            phoneNum = _regexPattern.Replace(phoneNum, "");
            StringBuilder builder = new(phoneNum);
            if (phoneNum[..2].Equals("84"))
            {
                builder = builder.Remove(0, 2).Insert(0, "0");
            }
            return builder.ToString();
        }
        public static string ReformatPhoneNumber(this string phoneNum)
        {
            phoneNum = _regexPattern.Replace(phoneNum, "");
            StringBuilder builder = new(phoneNum);
            if (phoneNum.StartsWith("0"))
            {
                builder = builder.Remove(0, 1).Insert(0, "84", 1);
            }
            return builder.ToString();
        }
    }
}
