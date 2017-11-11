using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionDecryptionHelper
{
    class Program
    {
        static void Main(string[] args)
        {

            string password = "bravo@0334";
            string computedHash = Hashing.GetInstance.GenerateSha512String(password);
            string passwrod = "bravo@0334";
            string computedHash2 = Hashing.GetInstance.GenerateSha512String(passwrod);
            Console.WriteLine(Hashing.GetInstance.VerifySha512Strings(computedHash,computedHash2));
            Console.ReadLine();
        }
    }
}
