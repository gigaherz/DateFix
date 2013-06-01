using System;

namespace DateFix
{
    static class Expect
    {
        public static void NoValue(bool hasValue, string parg0)
        {
            if (hasValue)
                throw new ApplicationException(string.Format(Messages.ErrorUnexpectedValue, parg0));
        }

        public static void Value(bool hasValue, string parg0)
        {
            if (!hasValue)
                throw new ApplicationException(string.Format(Messages.ErrorExpectedValue, parg0));
        }
    }
}