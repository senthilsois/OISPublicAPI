using System.Security.Cryptography;
using System.Text;

namespace OISPublic.Helper
{
    public static class PasswordGenerator
    {
        public static string GenerateRandomPassword(int length = 10)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890@#$_";
            StringBuilder password = new StringBuilder();
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (password.Length < length)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    var idx = num % (uint)validChars.Length;
                    password.Append(validChars[(int)idx]);
                }
            }

            return password.ToString();
        }
    }
}
