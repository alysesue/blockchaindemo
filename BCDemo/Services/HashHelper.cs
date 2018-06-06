using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BCDemo.Services
{
    public static class HashHelper
    {
        private static byte[] GenerateSalt(int length)
        {
            var salt = new byte[length];

            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(salt);
            }

            return salt;
        }

        public static string CalculateHash(string input)
        {
            var salt = GenerateSalt(16);

            var bytes = KeyDerivation.Pbkdf2(input, salt, KeyDerivationPrf.HMACSHA512, 10000, 16);

            return $"{ Convert.ToBase64String(salt) }:{ Convert.ToBase64String(bytes) }";
        }

        public static string RandomNumberGen()
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            byte[] byteArray = new byte[4];
            provider.GetBytes(byteArray);
            UInt32 randomInteger = BitConverter.ToUInt32(byteArray, 0);
            return randomInteger.ToString();
        }

        public static string RandomNumberGen(string input)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            byte[] byteArray = new byte[4];
            provider.GetBytes(byteArray);
            UInt32 randomInteger = BitConverter.ToUInt32(byteArray, 0);
            return randomInteger.ToString();
        }


    }
}
