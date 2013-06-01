using System;
using System.IO;
using DateFix.Annotations;
using DateFix.Properties;

namespace DateFix
{
    internal static class Program
    {
        private static int Main([NotNull] string[] args)
        {
            try
            {
                if (Args.Parse(args))
                {
                    Fixer.Run();
                }

                return 0;
            }
            catch (ApplicationException e)
            {
                Log.Error(e.Message);
            }
            catch (Exception e)
            {
                Log.Error(Resources.ErrorParsingCommandLine, e.Message);
            }

            PrintHelp(Console.Error);
            return 1;
        }

        public static void PrintHelp([NotNull] TextWriter output)
        {
            output.WriteLine(Resources.HelpMessage);
        }
    }
}