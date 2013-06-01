using System;
using System.Collections.Generic;
using DateFix.Annotations;
using DateFix.Properties;

namespace DateFix
{
    internal static class Args
    {
        private static bool pathSet;
        private static bool dateSet;
        private static bool timeSet;
        private static DateTime setToDate;
        private static TimeSpan setToTime;
        public static string Path { get; set; }
        public static bool Recursive { get; set; }
        public static bool FutureOnly { get; set; }
        public static bool TouchReadonly { get; set; }
        public static bool TouchDirectories { get; set; }
        public static DateTime SetTo { get; set; }

        public static bool Parse([NotNull] IEnumerable<string> args)
        {
            var argsSeen = new HashSet<string>();

            Path = Environment.CurrentDirectory;
            SetTo = DateTime.Now;

            pathSet = false;
            dateSet = false;
            timeSet = false;

            setToDate = SetTo.Date;
            setToTime = SetTo.AddSeconds(-1).TimeOfDay;

            foreach (string arg in args)
            {
                if (arg.StartsWith("-"))
                {
                    int iarg = arg.IndexOf('=');
                    string parg0 = iarg < 0 ? arg : arg.Substring(0, iarg);
                    string parg1 = iarg < 0 ? null : arg.Substring(iarg + 1);

                    if (argsSeen.Contains(parg0))
                    {
                        Messages.Warning(Resources.WarningDuplicateArgument, parg0);
                    }

                    if (!ParseArg(iarg >= 0, parg0, parg1))
                        return false;
                }
                else
                {
                    if (pathSet)
                    {
                        throw new ApplicationException(Resources.ErrorDirectorySpecifiedMoreThanOnce);
                    }

                    pathSet = true;
                    Path = arg;
                }
            }

            if (dateSet && !timeSet)
            {
                Messages.Warning(Resources.WarningDateSpecifiedWithoutTime);
                SetTo = setToDate.Date;
            }
            else
            {
                SetTo = setToDate.Date + setToTime;
            }

            return true;
        }

        [ContractAnnotation("hasValue: false => parg1: null")]
        private static bool ParseArg(bool hasValue, [NotNull] string parg0, [CanBeNull] string parg1)
        {
            switch (parg0)
            {
                case "-help":
                    Messages.PrintHelp(Console.Out);
                    return false;

                case "-recursive":
                    Expect.NoValue(hasValue, parg0);

                    Recursive = true;
                    break;

                case "-date":
                    Expect.Value(hasValue, parg0);

                    setToDate = DateTime.Parse(parg1);
                    dateSet = true;

                    if (setToDate.TimeOfDay.TotalSeconds > 0)
                    {
                        setToTime = setToDate.TimeOfDay;
                        setToDate = setToDate.Date;
                        timeSet = true;
                    }
                    break;

                case "-time":
                    Expect.Value(hasValue, parg0);

                    setToTime = TimeSpan.Parse(parg1);
                    timeSet = true;
                    break;

                case "-future-only":
                    Expect.NoValue(hasValue, parg0);

                    FutureOnly = true;
                    break;

                case "-touch-readonly":
                    Expect.NoValue(hasValue, parg0);

                    TouchReadonly = true;
                    break;

                case "-touch-directories":
                    Expect.NoValue(hasValue, parg0);

                    TouchDirectories = true;
                    break;

                default:
                    throw new ApplicationException(string.Format(Resources.ErrorUnrecognizedArgument, parg0));
            }

            return true;
        }
    }
}