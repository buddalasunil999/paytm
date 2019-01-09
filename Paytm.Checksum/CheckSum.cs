using System;
using System.Collections.Generic;
using paytm.util;
using Paytm.Checksum.Exception;
using Paytm.Checksum.Security;
using Paytm.Checksum.Util;

namespace Paytm.Checksum
{
    public class CheckSum
    {
        public static string GenerateCheckSum(string masterKey, Dictionary<string, string> parameters)
        {
            CheckSum.ValidateGenerateCheckSumInput(masterKey);
            Dictionary<string, string> parameters1 = new Dictionary<string, string>();
            try
            {
                foreach (string key in parameters.Keys)
                {
                    if (parameters[key].Trim().ToUpper().Contains("REFUND") || parameters[key].Trim().Contains("|"))
                        parameters1.Add(key.Trim(), "");
                    else
                        parameters1.Add(key.Trim(), parameters[key].Trim());
                }
                string checkSumString = SecurityUtils.CreateCheckSumString(parameters1);
                string randomString = StringUtils.GenerateRandomString(4);
                string inputValue = checkSumString + randomString;
                MessageConsole.WriteLine();
                MessageConsole.WriteLine("Final CheckSum String:::: " + inputValue);
                string hashedString = SecurityUtils.GetHashedString(inputValue);
                MessageConsole.WriteLine();
                MessageConsole.WriteLine("HashedCheckSum String:::: " + hashedString);
                string clearText = hashedString + randomString;
                MessageConsole.WriteLine();
                MessageConsole.WriteLine("HashedCheckSum String with Salt:::: " + clearText);
                return Crypto.Encrypt(clearText, masterKey);
            }
            catch (System.Exception ex)
            {
                throw new CryptoException("Exception occurred while generating CheckSum. " + ex.Message);
            }
        }

        public static string GenerateCheckSumForRefund(string masterKey, Dictionary<string, string> parameters)
        {
            CheckSum.ValidateGenerateCheckSumInput(masterKey);
            try
            {
                string checkSumString = SecurityUtils.CreateCheckSumString(parameters);
                string randomString = StringUtils.GenerateRandomString(4);
                string inputValue = checkSumString + randomString;
                MessageConsole.WriteLine();
                MessageConsole.WriteLine("Final CheckSum String:::: " + inputValue);
                string hashedString = SecurityUtils.GetHashedString(inputValue);
                MessageConsole.WriteLine();
                MessageConsole.WriteLine("HashedCheckSum String:::: " + hashedString);
                string clearText = hashedString + randomString;
                MessageConsole.WriteLine();
                MessageConsole.WriteLine("HashedCheckSum String with Salt:::: " + clearText);
                return Crypto.Encrypt(clearText, masterKey);
            }
            catch (System.Exception ex)
            {
                throw new CryptoException("Exception occurred while generating CheckSum. " + ex.Message);
            }
        }

        public static string GenerateCheckSumByJson(string masterKey, string json)
        {
            CheckSum.ValidateGenerateCheckSumInput(masterKey);
            try
            {
                string str = json;
                string randomString = StringUtils.GenerateRandomString(4);
                return Crypto.Encrypt(SecurityUtils.GetHashedString(str + "|" + randomString) + randomString, masterKey);
            }
            catch (System.Exception ex)
            {
                throw new CryptoException("Exception occurred while generating CheckSum. " + ex.Message);
            }
        }

        public static bool VerifyCheckSumByjson(string masterKey, string json, string checkSum)
        {
            CheckSum.ValidateVerifyCheckSumInput(masterKey, checkSum);
            try
            {
                string str1 = Crypto.Decrypt(checkSum, masterKey);
                if (str1 == null || str1.Length < 4)
                    return false;
                string str2 = str1.Substring(str1.Length - 4, 4);
                MessageConsole.WriteLine("Salt:::: " + str2);
                MessageConsole.WriteLine();
                MessageConsole.WriteLine("Input CheckSum:::: " + checkSum);
                string str3 = json;
                MessageConsole.WriteLine();
                MessageConsole.WriteLine("GeneratedCheckSum String:::: " + str3);
                string inputValue = str3 + "|" + str2;
                MessageConsole.WriteLine();
                MessageConsole.WriteLine("GeneratedCheckSum String with Salt:::: " + inputValue);
                string hashedString = SecurityUtils.GetHashedString(inputValue);
                MessageConsole.WriteLine();
                MessageConsole.WriteLine("HashedGeneratedCheckSum String:::: " + hashedString);
                string str4 = hashedString + str2;
                MessageConsole.WriteLine();
                MessageConsole.WriteLine("HashedGeneratedCheckSum String with Salt:::: " + str4);
                return str4.Equals(str1);
            }
            catch (System.Exception ex)
            {
                throw new CryptoException("Exception occurred while verifying CheckSum. " + ex.Message);
            }
        }

        public static bool VerifyCheckSum(string masterKey, Dictionary<string, string> parameters, string checkSum)
        {
            CheckSum.ValidateVerifyCheckSumInput(masterKey, checkSum);
            try
            {
                string str1 = Crypto.Decrypt(checkSum, masterKey);
                if (str1 == null || str1.Length < 4)
                    return false;
                string str2 = str1.Substring(str1.Length - 4, 4);
                MessageConsole.WriteLine("Salt:::: " + str2);
                MessageConsole.WriteLine();
                MessageConsole.WriteLine("Input CheckSum:::: " + checkSum);
                string checkSumString = SecurityUtils.CreateCheckSumString(parameters);
                MessageConsole.WriteLine();
                MessageConsole.WriteLine("GeneratedCheckSum String:::: " + checkSumString);
                string inputValue = checkSumString + str2;
                MessageConsole.WriteLine();
                MessageConsole.WriteLine("GeneratedCheckSum String with Salt:::: " + inputValue);
                string hashedString = SecurityUtils.GetHashedString(inputValue);
                MessageConsole.WriteLine();
                MessageConsole.WriteLine("HashedGeneratedCheckSum String:::: " + hashedString);
                string str3 = hashedString + str2;
                MessageConsole.WriteLine();
                MessageConsole.WriteLine("HashedGeneratedCheckSum String with Salt:::: " + str3);
                return str3.Equals(str1);
            }
            catch (System.Exception ex)
            {
                throw new CryptoException("Exception occurred while verifying CheckSum. " + ex.Message);
            }
        }

        private static void ValidateGenerateCheckSumInput(string masterKey)
        {
            if (masterKey == null)
                throw new ArgumentNullException("Parameter cannot be null", nameof(masterKey));
        }

        private static void ValidateVerifyCheckSumInput(string masterKey, string checkSum)
        {
            if (masterKey == null)
                throw new ArgumentNullException("Parameter cannot be null", nameof(masterKey));
            if (checkSum == null)
                throw new ArgumentNullException("Parameter cannot be null", nameof(checkSum));
        }

        public static string Encrypt(string CardDetails, string masterKey)
        {
            return Crypto.Encrypt(CardDetails, masterKey);
        }

        public static string Decrypt(string carddetails, string masterKey)
        {
            return Crypto.Decrypt(carddetails, masterKey);
        }
    }
}
