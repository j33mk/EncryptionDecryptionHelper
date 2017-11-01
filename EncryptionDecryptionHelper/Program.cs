using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionDecryptionHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            string encryptedUrl = Aes256BitEncryption.GetInstance.EncryptText("userId = 34 & cookiesEnabled = false & Registration = true & EmailAuthenticate = true & email = something@yahoo.com & verbose = true","web2@app");
            string decryptedUrl = Aes256BitEncryption.GetInstance.DecryptText(encryptedUrl, "web2@app");
            Console.WriteLine("enc="+encryptedUrl);
            Console.WriteLine(decryptedUrl);
            Console.ReadLine();
        }
    }
}
