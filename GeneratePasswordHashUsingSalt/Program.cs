using System.Security.Cryptography;

namespace GeneratePasswordHashUsingSalt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string password = "Password";
            var array = new byte[32];

            while(true)
            {
                GeneratePasswordHashUsingSalt(password, array);
            }
        }

        public static string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
        {

            var iterate = 10000;
            byte[] hash;

            using (var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate))
            {
                hash = pbkdf2.GetBytes(20);
            }

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            var passwordHash = Convert.ToBase64String(hashBytes);

            return passwordHash;

        }
    }
}