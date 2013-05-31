using System;

namespace DateFix
{
    static class Expect
    {
        public static void NoValue(int iarg, string parg0)
        {
            if (iarg >= 0)
                throw new ApplicationException(string.Format(Messages.ErrorUnexpectedValue, parg0));
        }

        public static void Value(int iarg, string parg0)
        {
            if (iarg < 0)
                throw new ApplicationException(string.Format(Messages.ErrorExpectedValue, parg0));
        }
    }
}