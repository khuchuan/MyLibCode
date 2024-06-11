// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;
using System.Text;


string key = "12345678901234561234567890123456";
string plaintexxt = "E23412345678 ha noi mua mua";

Console.WriteLine("plaintexxt: " + plaintexxt);
Console.WriteLine("-----------------");


for (int i = 1; i < 10; i++)
{
    string txtEncrypt = AESEncryptClient(plaintexxt, key);
    Console.WriteLine(txtEncrypt);

    Console.WriteLine("Giai ma: " +AESDecryptClient(txtEncrypt, key));
}
Console.ReadKey();


string AESEncryptClient(string plainText, string secretKey)
{
    //aes 128
    byte[] encrypted;
    string iv = secretKey.Substring(0, 16);
    string key = secretKey.Substring(16);
    using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
    {
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = Encoding.UTF8.GetBytes(iv);
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        ICryptoTransform enc = aes.CreateEncryptor(aes.Key, aes.IV);

        using (MemoryStream ms = new MemoryStream())
        {
            using (CryptoStream cs = new CryptoStream(ms, enc, CryptoStreamMode.Write))
            {
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(plainText);
                }

                encrypted = ms.ToArray();
            }
        }
    }
    return Convert.ToBase64String(encrypted);
}


string AESDecryptClient(string encrypted, string secretKey)
{
    //aes 128
    string iv = secretKey.Substring(0, 16);
    string key = secretKey.Substring(16);
    byte[] encryptedBytes = Convert.FromBase64String(encrypted);
    AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
    aes.Mode = CipherMode.CBC;
    aes.Padding = PaddingMode.PKCS7;
    aes.Key = Encoding.UTF8.GetBytes(key);//key
    aes.IV = Encoding.UTF8.GetBytes(iv); //2314345645678765
    ICryptoTransform crypto = aes.CreateDecryptor(aes.Key, aes.IV);
    byte[] secret = crypto.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
    crypto.Dispose();
    return Encoding.UTF8.GetString(secret);
}