namespace Paytm.Checksum.Security
{
    internal class Crypto
    {
        public static string Encrypt(string clearText, string masterKey)
        {
            return RijndaelCrypto.Encrypt(clearText, masterKey);
        }

        public static string Decrypt(string cipherText, string masterKey)
        {
            return RijndaelCrypto.Decrypt(cipherText, masterKey);
        }
    }
}