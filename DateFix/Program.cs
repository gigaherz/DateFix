using System;
using DateFix.Properties;

namespace DateFix
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                if (Args.Parse(args))
                {
                    Fixer.Run();
                }
            }
            catch (ApplicationException e)
            {
                Log.Error(e.Message);
            }
            catch (Exception e)
            {
                Log.Error(Resources.ErrorParsingCommandLine, e.Message);
            }

            Log.PrintHelp(Console.Error);
        }
    }
}