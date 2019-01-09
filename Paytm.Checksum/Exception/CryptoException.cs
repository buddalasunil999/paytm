namespace Paytm.Checksum.Exception
{
    public class CryptoException : System.Exception
    {
        public CryptoException()
        {
        }

        public CryptoException(string message)
            : base(message)
        {
        }

        public CryptoException(string message, System.Exception e)
            : base(message, e)
        {
        }

        public CryptoException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public CryptoException(string format, System.Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }
    }
}