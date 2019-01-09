using System;

namespace paytm.util
{
    internal class MessageConsole
    {
        private static bool MESSAGING_ON;

        public static void WriteLine()
        {
            if (!MessageConsole.MESSAGING_ON)
                return;
            Console.WriteLine();
        }

        public static void WriteLine(string value)
        {
            if (!MessageConsole.MESSAGING_ON)
                return;
            Console.WriteLine(value);
        }

        public static void Write(string value)
        {
            if (!MessageConsole.MESSAGING_ON)
                return;
            Console.Write(value);
        }

        public static void WriteLine(string value, object arg0, object arg1)
        {
            if (!MessageConsole.MESSAGING_ON)
                return;
            Console.WriteLine(value, arg0, arg1);
        }
    }
}