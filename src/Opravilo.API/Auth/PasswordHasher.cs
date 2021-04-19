using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Opravilo.API.Auth
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly string _salt;
        
        public PasswordHasher(IConfiguration configuration)
        {
            _salt = configuration.GetValue<string>("Salt");
        }
        
        public string HashPassword(string password)
        {
            var passwordBytes = Encoding.Default.GetBytes(password);
            var saltBytes = Encoding.Default.GetBytes(_salt);

            var hashed = GenerateSaltedHash(passwordBytes, saltBytes);

            return Convert.ToBase64String(hashed);
        }
        
        private byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            var plainTextWithSaltBytes = 
                new byte[plainText.Length + salt.Length];

            for (var i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            
            for (var i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);            
        }
    }
}