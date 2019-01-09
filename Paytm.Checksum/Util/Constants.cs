namespace Paytm.Checksum.Util
{
    internal class Constants
    {
        public static bool USE_UNICODE_ENCODING = false;
        public static byte[] CRYPTO_INIT_VECTOR = new byte[16]
        {
            (byte) 64,
            (byte) 64,
            (byte) 64,
            (byte) 64,
            (byte) 38,
            (byte) 38,
            (byte) 38,
            (byte) 38,
            (byte) 35,
            (byte) 35,
            (byte) 35,
            (byte) 35,
            (byte) 36,
            (byte) 36,
            (byte) 36,
            (byte) 36
        };
        public const string VALUE_SEPARATOR_TOKEN = "|";
        public const short SALT_LENGTH = 4;
    }
}
