using System;

namespace DateFix
{
    static class Program
    {
        static void Main(string[] args)
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
                Console.Error.WriteLine(Messages.Error, e.Message);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(Messages.ErrorParsingCommandLine, e.Message);
            }

            Messages.PrintHelp(Console.Error);
        }
    }
}
