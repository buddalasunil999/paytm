using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using paytm.util;

namespace Paytm.Checksum.Util
{
    internal class SecurityUtils
    {
        public static string CreateCheckSumString(Dictionary<string, string> parameters)
        {
            if (parameters == null)
                return "";
            MessageConsole.WriteLine();
            MessageConsole.WriteLine("Input Dict::::");
            SecurityUtils.PrintDictionary(parameters);
            SortedDictionary<string, string> dict = new SortedDictionary<string, string>((IDictionary<string, string>)parameters, (IComparer<string>)StringComparer.Ordinal);
            MessageConsole.WriteLine();
            MessageConsole.WriteLine("Sorted Dict::::");
            SecurityUtils.PrintSortedDictionary(dict);
            StringBuilder stringBuilder = new StringBuilder("");
            foreach (KeyValuePair<string, string> keyValuePair in dict)
            {
                string str = keyValuePair.Value;
                if (str == null || str.Trim().Equals("NULL"))
                    str = "";
                stringBuilder.Append(str).Append("|");
            }
            return stringBuilder.ToString();
        }

        public static string GetHashedString(string inputValue)
        {
            return BitConverter.ToString(new SHA256Managed().ComputeHash(StringUtils.GetBytesFromString(inputValue))).Replace("-", "").ToLower();
        }

        private static void PrintDictionary(Dictionary<string, string> dict)
        {
            if (dict == null)
                return;
            foreach (KeyValuePair<string, string> keyValuePair in dict)
                MessageConsole.WriteLine("{0}, {1}", (object)keyValuePair.Key, (object)keyValuePair.Value);
        }

        private static void PrintSortedDictionary(SortedDictionary<string, string> dict)
        {
            if (dict == null)
                return;
            foreach (KeyValuePair<string, string> keyValuePair in dict)
                MessageConsole.WriteLine("{0}, {1}", (object)keyValuePair.Key, (object)keyValuePair.Value);
        }
    }
}