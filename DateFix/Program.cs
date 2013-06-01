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
                Messages.Error(e.Message);
            }
            catch (Exception e)
            {
                Messages.Error(Resources.ErrorParsingCommandLine, e.Message);
            }

            Messages.PrintHelp(Console.Error);
        }
    }
}