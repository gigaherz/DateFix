using System.IO;

namespace DateFix
{
    static class Messages
    {
        public static readonly string Processing = "Processing '{0}'...";
        public static readonly string Touching = "Touching '{0}'...";

        public static readonly string WarningDuplicateArgument = "WARNING: Duplicate argument '{0}', second value will take over.";
        public static readonly string WarningDateSpecifiedWithoutTime = "WARNING: Date specified without time, time will be set to 00:00:00.";

        public static readonly string ErrorWhileProcessing = "ERROR: While processing '{0}': {1}";
        public static readonly string ErrorWhileTouching = "ERROR: While touching '{0}': {1}";
        public static readonly string ErrorParsingCommandLine = "ERROR: While parsing the command line arguments: {0}";

        public static readonly string Error = "ERROR: {0}";
        public static readonly string ErrorUnrecognizedArgument = "Unrecognized argument: {0}";
        public static readonly string ErrorDirectorySpecifiedMoreThanOnce = "Directory specified more than once.";
        public static readonly string ErrorUnexpectedValue = "Unexpected value for argument: {0}";
        public static readonly string ErrorExpectedValue = "Expected value for argument: {0}";

        public static void PrintHelp(TextWriter output)
        {
            output.WriteLine("DateFix Help:");
            output.WriteLine();
            output.WriteLine("Parameters:");
            output.WriteLine("  -help               Shows this message.");
            output.WriteLine("  -recursive          Process subdirectories");
            output.WriteLine("  -date=<date>        Use the specified date instead of current.");
            output.WriteLine("  -time=<time>        Use the specified time instead of current.");
            output.WriteLine("  -future-only        Process only files with dates > specified.");
            output.WriteLine("  -touch-readonly     Temporarily remove the readonly attribute.");
            output.WriteLine("  -touch-directories  Process directory dates also, not just files.");
        }
    }
}
