using System;
using DateFix.Annotations;
using DateFix.Properties;

namespace DateFix
{
    internal static class Expect
    {
        [ContractAnnotation("halt <= hasValue: true")]
        public static void NoValue(bool hasValue, [NotNull] string parg0)
        {
            if (hasValue)
                throw new ApplicationException(string.Format(Resources.ErrorUnexpectedValue, parg0));
        }

        [ContractAnnotation("halt <= hasValue: false")]
        public static void Value(bool hasValue, [NotNull] string parg0)
        {
            if (!hasValue)
                throw new ApplicationException(string.Format(Resources.ErrorExpectedValue, parg0));
        }
    }
}