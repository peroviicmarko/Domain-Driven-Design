using System.Security.Cryptography;
using System.Text;

namespace DDD.Application.Utilities
{
    public class Security
    {
        public static string HashPasswodUsingSHA256(string password)
        {
            using SHA256Managed SHA256Managed = new();
            byte[] bytes = Encoding.ASCII.GetBytes(password);
            string hashedPassword = string.Empty;

            bytes = SHA256Managed.ComputeHash(bytes);

            foreach (byte b in bytes)
            {
                hashedPassword += b.ToString("x2");
            }

            return hashedPassword;
        }
    }
}
