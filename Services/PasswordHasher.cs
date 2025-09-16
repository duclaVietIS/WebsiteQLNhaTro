using System.Security.Cryptography;
using System.Text;

namespace WebsiteQLNhaTro.Services
{
    /// <summary>
    /// PasswordHasher: provides password hashing and verification methods.
    /// </summary>
    public class PasswordHasher
    {
        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public bool VerifyPassword(string password, string hash)
        {
            // print password and hash
            Console.WriteLine($"Verifying password: {password} against hash: {hash}");
            return HashPassword(password) == hash;
        }
    }
}
