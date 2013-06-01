using System;
using DateFix.Annotations;
using DateFix.Properties;

namespace DateFix
{
    internal static class Log
    {
        [StringFormatMethod("message")]
        public static void Error([NotNull] string message, params object[] args)
        {
            Console.Error.WriteLine(Resources.ErrorTemplate, string.Format(message, args));
        }

        [StringFormatMethod("message")]
        public static void Warning([NotNull] string message, params object[] args)
        {
            Console.WriteLine(Resources.WarningTemplate, string.Format(message, args));
        }

        [StringFormatMethod("message")]
        public static void Info([NotNull] string message, params object[] args)
        {
            Console.Write(message, args);
        }
    }
}