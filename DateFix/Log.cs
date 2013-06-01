using System;
using System.IO;
using DateFix.Annotations;
using DateFix.Properties;

namespace DateFix
{
    [LocalizationRequired(true)]
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

        public static void PrintHelp([NotNull] TextWriter output)
        {
            output.WriteLine(Resources.HelpMessage);
        }
    }
}