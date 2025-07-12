using System.Security.Cryptography;
using System.Text;

namespace OISPublic.Helper
{
    public static class AesEncryptionHelper
    {
  

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



        public static string DecryptForPayloadData(string encryptedData, string encryptedKey, string encryptedIV)
        {
            if (string.IsNullOrEmpty(encryptedData) || string.IsNullOrEmpty(encryptedKey) || string.IsNullOrEmpty(encryptedIV))
                throw new ArgumentException("Encrypted data, key, and IV must be provided.");

            var cipherBytes = Convert.FromBase64String(encryptedData);
            var keyBytes = Convert.FromBase64String(encryptedKey);
            var ivBytes = Convert.FromBase64String(encryptedIV);

            using var aes = Aes.Create();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var decryptor = aes.CreateDecryptor(keyBytes, ivBytes);
            using var ms = new MemoryStream(cipherBytes);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }


    }
}
