using System.Security.Cryptography;
using System.Text;

namespace Helper
{
    public static class Security
    {
        public static byte[] HashSHA256(string strValue)
        {
            return SHA256.HashData(Encoding.ASCII.GetBytes(strValue));
        }
        public static string ToHex(byte[] ba)
        {
            StringBuilder hex = new(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}
