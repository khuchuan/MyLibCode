using System.Collections;
using System.Security.Cryptography;
using System.Text;
namespace Helper
{
    public class SignatureService
    {
        private readonly string _PublicKey;
        private readonly string _PrivateKey;
        private readonly int _Keysize = 1024;

        public SignatureService(string privateKey, string publicKey)
        {
            _PrivateKey = privateKey;
            _PublicKey = publicKey;
        }

        public string SignHash(string originText)
        {
            using var rsa = new RSACryptoServiceProvider(_Keysize);
            rsa.FromXmlString(_PrivateKey);
            using var sha1 = SHA1.Create();
            var hashValue = sha1.ComputeHash(Encoding.UTF8.GetBytes(originText));
            var signedValue = rsa.SignHash(hashValue, CryptoConfig.MapNameToOID("SHA1"));
            return Convert.ToBase64String(signedValue);
        }

        public bool VerifyHash(string originText, string signedText)
        {
            using var rsa = new RSACryptoServiceProvider(_Keysize);
            using var sha1 = SHA1.Create();
            rsa.FromXmlString(_PublicKey);
            var originByte = sha1.ComputeHash(Encoding.UTF8.GetBytes(originText));
            var signedByte = Convert.FromBase64String(signedText);
            return rsa.VerifyHash(originByte, CryptoConfig.MapNameToOID("SHA1")!, signedByte);
        }

        public string Encrypt(string dataToEncrypt)
        {
            if (string.IsNullOrEmpty(dataToEncrypt) || string.IsNullOrEmpty(_PublicKey)) return "";
            try
            {
                //RSAParameters _publicKey = LoadRsaPublicKey(publicKey, false);
                var rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(_PublicKey);
                //InitRSAProvider(_publicKey);
                var keySize = rsa.KeySize / 8;
                var maxLength = keySize - 42;
                var bytes = Encoding.ASCII.GetBytes(dataToEncrypt);
                var dataLength = bytes.Length;
                var iterations = dataLength / maxLength;
                var stringBuilder = new StringBuilder();
                for (var i = 0; i <= iterations; i++)
                {
                    var tempBytes = new byte[(dataLength - maxLength * i > maxLength) ? maxLength : dataLength - maxLength * i];
                    Buffer.BlockCopy(bytes, maxLength * i, tempBytes, 0, tempBytes.Length);
                    var encryptedBytes = rsa.Encrypt(tempBytes, false);
                    // Be aware the RSACryptoServiceProvider reverses the order of encrypted bytes after encryption and before decryption.
                    // If you do not require compatibility with Microsoft Cryptographic API (CAPI) and/or other vendors.
                    // Comment out the next line and the corresponding one in the DecryptString function.
                    Array.Reverse(encryptedBytes);
                    // Why convert to base 64?
                    // Because it is the largest power-of-two base printable using only ASCII characters
                    stringBuilder.Append(Convert.ToBase64String(encryptedBytes));
                }
                var sEncrypted = stringBuilder.ToString();
                return sEncrypted;
            }
            catch { return ""; }
        }

        public string Decrypt(string base64EncryptedText)
        {
            string result;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(_Keysize))
            {
                rsa.FromXmlString(_PrivateKey);
                int base64BlockSize = ((_Keysize / 8) % 3 != 0) ? (((_Keysize / 8) / 3) * 4) + 4 : ((_Keysize / 8) / 3) * 4;
                var iterations = base64EncryptedText.Length / base64BlockSize;
                var arrayList = new ArrayList();
                for (var i = 0; i < iterations; i++)
                {
                    var encryptedBytes = Convert.FromBase64String(base64EncryptedText.Substring(base64BlockSize * i, base64BlockSize));
                    // Be aware the RSACryptoServiceProvider reverses the order of 
                    // encrypted bytes after encryption and before decryption.
                    // If you do not require compatibility with Microsoft Cryptographic 
                    // API (CAPI) and/or other vendors.
                    // Comment out the next line and the corresponding one in the 
                    // EncryptString function.
                    Array.Reverse(encryptedBytes);
                    arrayList.AddRange(rsa.Decrypt(encryptedBytes, false));
                }
                result = Encoding.ASCII.GetString(arrayList.ToArray(Type.GetType("System.Byte")) as byte[]);
            }
            return result;
        }

    }
}
