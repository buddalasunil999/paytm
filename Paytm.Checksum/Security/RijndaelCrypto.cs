using System;
using System.IO;
using System.Security.Cryptography;
using Paytm.Checksum.Util;

namespace Paytm.Checksum.Security
{
    internal class RijndaelCrypto
    {
        private static bool USE_CUSTOM_INIT_VECTOR = true;

        public static string Encrypt(string clearText, string masterKey)
        {
            byte[] bytesFromString1 = StringUtils.GetBytesFromString(clearText);
            byte[] bytesFromString2 = StringUtils.GetBytesFromString(masterKey);
            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(masterKey, new byte[13]
            {
        (byte) 73,
        (byte) 118,
        (byte) 97,
        (byte) 110,
        (byte) 32,
        (byte) 77,
        (byte) 101,
        (byte) 100,
        (byte) 118,
        (byte) 101,
        (byte) 100,
        (byte) 101,
        (byte) 118
            });
            byte[] IV = !RijndaelCrypto.USE_CUSTOM_INIT_VECTOR ? passwordDeriveBytes.GetBytes(16) : Constants.CRYPTO_INIT_VECTOR;
            return Convert.ToBase64String(RijndaelCrypto.Encrypt(bytesFromString1, bytesFromString2, IV));
        }

        private static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
        {
            MemoryStream memoryStream = new MemoryStream();
            Rijndael rijndael = Rijndael.Create();
            rijndael.Key = Key;
            rijndael.IV = IV;
            rijndael.Mode = CipherMode.CBC;
            rijndael.Padding = PaddingMode.PKCS7;
            CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(clearData, 0, clearData.Length);
            cryptoStream.Close();
            return memoryStream.ToArray();
        }

        public static string Decrypt(string cipherText, string masterKey)
        {
            byte[] cipherData = Convert.FromBase64String(cipherText);
            byte[] bytesFromString = StringUtils.GetBytesFromString(masterKey);
            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(masterKey, new byte[13]
            {
        (byte) 73,
        (byte) 118,
        (byte) 97,
        (byte) 110,
        (byte) 32,
        (byte) 77,
        (byte) 101,
        (byte) 100,
        (byte) 118,
        (byte) 101,
        (byte) 100,
        (byte) 101,
        (byte) 118
            });
            byte[] IV = !RijndaelCrypto.USE_CUSTOM_INIT_VECTOR ? passwordDeriveBytes.GetBytes(16) : Constants.CRYPTO_INIT_VECTOR;
            return StringUtils.GetStringFromBytes(RijndaelCrypto.Decrypt(cipherData, bytesFromString, IV));
        }

        private static byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV)
        {
            MemoryStream memoryStream = new MemoryStream();
            Rijndael rijndael = Rijndael.Create();
            rijndael.Key = Key;
            rijndael.IV = IV;
            rijndael.Mode = CipherMode.CBC;
            rijndael.Padding = PaddingMode.PKCS7;
            CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(cipherData, 0, cipherData.Length);
            cryptoStream.Close();
            return memoryStream.ToArray();
        }
    }
}