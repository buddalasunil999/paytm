using System;
using System.Text;

namespace Paytm.Checksum.Util
{
    internal class StringUtils
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);
        private const string CHARACTER_SET = "@#!abcdefghijklmonpqrstuvwxyz#@01234567890123456789#@ABCDEFGHIJKLMNOPQRSTUVWXYZ#@";

        public static string GenerateRandomString(int length)
        {
            if (length <= 0)
                return "";
            StringBuilder stringBuilder = new StringBuilder("");
            for (int index = 0; index < length; ++index)
            {
                int startIndex = StringUtils.random.Next("@#!abcdefghijklmonpqrstuvwxyz#@01234567890123456789#@ABCDEFGHIJKLMNOPQRSTUVWXYZ#@".Length);
                stringBuilder.Append("@#!abcdefghijklmonpqrstuvwxyz#@01234567890123456789#@ABCDEFGHIJKLMNOPQRSTUVWXYZ#@".Substring(startIndex, 1));
            }
            return stringBuilder.ToString();
        }

        public static string GetStringFromBytes(byte[] byteArr)
        {
            if (Constants.USE_UNICODE_ENCODING)
                return Encoding.Unicode.GetString(byteArr);
            return Encoding.ASCII.GetString(byteArr);
        }

        public static byte[] GetBytesFromString(string strInput)
        {
            if (Constants.USE_UNICODE_ENCODING)
                return Encoding.Unicode.GetBytes(strInput);
            return Encoding.ASCII.GetBytes(strInput);
        }
    }
}