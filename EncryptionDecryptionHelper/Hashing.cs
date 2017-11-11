using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionDecryptionHelper
{
    class Hashing
    {

        private static Hashing _instance;
        public static Hashing GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Hashing();
                }
                return _instance;
            }
        }
        private Hashing(){}
        public string GenerateSha256String(string inputString)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        public string GenerateSha512String(string inputString)
        {
            SHA512 sha512 = SHA512.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        public bool VerifySha256Strings(string firstInput, string secondInput)
        {
            return firstInput.Equals(secondInput);
        }

        public bool VerifySha512Strings(string firstInput, string seconndInput)
        {
            return firstInput.Equals(seconndInput);
        }

        private string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            foreach (byte h in hash)
            {
                result.Append(h.ToString("X2"));
            }
            return result.ToString();
        }
    }
}
