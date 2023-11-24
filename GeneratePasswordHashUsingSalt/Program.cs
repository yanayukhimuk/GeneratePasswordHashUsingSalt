using BenchmarkDotNet.Running;
using System.Security.Cryptography;

namespace GeneratePasswordHashUsingSalt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string password = "Password";
            var array = new byte[32];

            var updatedHash = GeneratePasswordHashUsingSaltUpdated(password, array);
            var originalHash = GeneratePasswordHashUsingSalt(password, array);

            if(!updatedHash.Equals(originalHash))
            {
                throw new Exception("They are not equal!");
            }
            else
            {
                Console.WriteLine("Methods are equal!");
            }

            BenchmarkRunner.Run<Benchmark>();
        }

        public static string GeneratePasswordHashUsingSaltUpdated(string passwordText, byte[] salt)
        {

            var iterate = 10000;
            var saltSpan = salt.AsSpan();
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(passwordText.AsSpan(), saltSpan, iterate, HashAlgorithmName.SHA1, 20);


            Span<byte> hashBytes = stackalloc byte[36];

            for (int i = 0; i < 16; i++)
            {
                hashBytes[i] = saltSpan[i];
            }

            for (int i = 16; i < 36; i++)
            {
                hashBytes[i] = hash[i - 16];
            }


            var passwordHash = Convert.ToBase64String(hashBytes);

            return passwordHash;
        }

        public static string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
        {

            var iterate = 10000;
            var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            var passwordHash = Convert.ToBase64String(hashBytes);

            return passwordHash;

        }
    }
}