using System.Security.Cryptography;
using System.Text;

namespace Auction.User.Common.Helpers
{
    public static class PasswordHelper
    {
        private static int keySize = 64;
        private static int iterations = 350000;
        private static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        public static string HashPasword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);
        }

        public static bool VerifyPassword(string incomingPassword, string hash) 
        { 
            
        }
    }
}
