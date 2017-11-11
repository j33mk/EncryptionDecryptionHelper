using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionDecryptionHelper
{
    public class Aes256BitEncryption
    {
        private static Aes256BitEncryption _instance;
        public static Aes256BitEncryption GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Aes256BitEncryption();
                }
                return _instance;
            }
        }
        private Aes256BitEncryption() { }
        private byte[] AesEncrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
                {
                    rijndaelManaged.KeySize = 256;
                    rijndaelManaged.BlockSize = 128;
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    rijndaelManaged.Key = key.GetBytes(rijndaelManaged.KeySize / 8);
                    rijndaelManaged.IV = key.GetBytes(rijndaelManaged.BlockSize / 8);
                    rijndaelManaged.Mode = CipherMode.CBC;
                    using (var cs = new CryptoStream(ms, rijndaelManaged.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }
        private byte[] AesDecrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes;
            // Set your salt here, change it to meet your flavor: 
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = { 1, 2, 3, 4, 5, 6, 7, 8 };
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    aes.KeySize = 256;
                    aes.BlockSize = 128;
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    aes.Key = key.GetBytes(aes.KeySize / 8);
                    aes.IV = key.GetBytes(aes.BlockSize / 8);
                    aes.Mode = CipherMode.CBC;
                    using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }
            return decryptedBytes;
        }
        public string EncryptText(string input, string password)
        {
            try
            {
                // Get the bytes of the string
                byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                // Hash the password with SHA256
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
                byte[] bytesEncrypted = AesEncrypt(bytesToBeEncrypted, passwordBytes);
                string result = Convert.ToBase64String(bytesEncrypted);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            
        }
        public string DecryptText(string input, string password)
        {
            try
            {
                // Get the bytes of the string
                byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                // Hash the password with SHA256
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
                byte[] bytesDecrypted = AesDecrypt(bytesToBeDecrypted, passwordBytes);
                string result = Encoding.UTF8.GetString(bytesDecrypted);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
           
        }

        public bool EncryptFile(string path,string password,string pathToStoreEncryptedFile)
        {
            try
            {
                byte[] bytesToBeEncrypted = File.ReadAllBytes(path);
                byte[] passwordBytesTobeEncrypted = Encoding.UTF8.GetBytes(password);
                // Hash the password with SHA256
                passwordBytesTobeEncrypted = SHA256.Create().ComputeHash(passwordBytesTobeEncrypted);
                byte[] bytesEncryped = AesEncrypt(bytesToBeEncrypted, passwordBytesTobeEncrypted);
                File.WriteAllBytes(pathToStoreEncryptedFile, bytesEncryped);
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            
        }
        public bool DecryptFile(string path, string password, string pathToStoreDecryptedFile)
        {
            try
            {
                byte[] bytesToBeDecrypted = File.ReadAllBytes(path);
                byte[] passwordBytesTobeEncrypted = Encoding.UTF8.GetBytes(password);
                // Hash the password with SHA256
                passwordBytesTobeEncrypted = SHA256.Create().ComputeHash(passwordBytesTobeEncrypted);
                byte[] bytesEncryped = AesDecrypt(bytesToBeDecrypted, passwordBytesTobeEncrypted);
                File.WriteAllBytes(pathToStoreDecryptedFile, bytesEncryped);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            
        }
    }
}
