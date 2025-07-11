using System.Security.Cryptography;
using System.Text;

namespace OISPublic.Helper
{
    public static class AesEncryptionHelper
    {
  
        private static readonly string MasterKey = "your-32-byte-static-key-123456789012";
        private static readonly string MasterIV = "your-16-byte-static";

        public class EncryptedResult
        {
            public string EncryptedData { get; set; } = string.Empty;
            public string EncryptedKey { get; set; } = string.Empty;
            public string EncryptedIV { get; set; } = string.Empty;
        }

        public static EncryptedResult EncryptWithRandomKey(string plainText)
        {
            using var aes = Aes.Create();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            aes.GenerateKey(); 
            aes.GenerateIV();

            var key = aes.Key;
            var iv = aes.IV;

            using var encryptor = aes.CreateEncryptor(key, iv);
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }

            return new EncryptedResult
            {
                EncryptedData = Convert.ToBase64String(ms.ToArray()),
                EncryptedKey = Convert.ToBase64String(key),
                EncryptedIV = Convert.ToBase64String(iv)
            };
        }
        private static string EncryptInternal(string input)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(MasterKey);
            aes.IV = Encoding.UTF8.GetBytes(MasterIV);

            using var encryptor = aes.CreateEncryptor();
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using var sw = new StreamWriter(cs);
            sw.Write(input);

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string DecryptInternal(string base64Input)
        {
            var buffer = Convert.FromBase64String(base64Input);

            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(MasterKey);
            aes.IV = Encoding.UTF8.GetBytes(MasterIV);

            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(buffer);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
    }
}
