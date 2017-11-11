using System;

namespace EncryptionDecryptionHelper
{
    class Program
    {
        static void Main()
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
