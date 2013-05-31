using System;
using System.Collections.Generic;
using System.IO;

namespace DateFix
{
    static class Fixer
    {
        private static readonly Queue<DirectoryInfo> Paths = new Queue<DirectoryInfo>();

        public static void Run()
        {
            try
            {
                var attr = File.GetAttributes(Args.Path);

                if (attr.HasFlag(FileAttributes.Directory))
                    ProcessPath(new DirectoryInfo(Args.Path));
                else
                    Touch(new FileInfo(Args.Path));

                while (Paths.Count > 0)
                {
                    var dir = Paths.Dequeue();

                    ProcessContents(dir);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(Messages.ErrorWhileProcessing, Args.Path, e.Message);
            }
        }

        private static void ProcessContents(DirectoryInfo dir)
        {
            try
            {
                Console.WriteLine(Messages.Processing, dir.FullName);

                foreach (var file in dir.EnumerateFiles())
                {
                    Touch(file);
                }

                foreach (var sub in dir.EnumerateDirectories())
                {
                    ProcessPath(sub);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(Messages.ErrorWhileProcessing, dir.FullName, e.Message);
            }
        }

        private static void ProcessPath(DirectoryInfo info)
        {
            if (Args.TouchDirectories)
                Touch(info);

            if (Args.Recursive)
                Paths.Enqueue(info);
        }

        private static void Touch(FileSystemInfo info)
        {
            try
            {
                var attr = info.Attributes;

                var wasReadonly = false;

                if (attr.HasFlag(FileAttributes.ReadOnly))
                {
                    if (!Args.TouchReadonly)
                        return;

                    wasReadonly = true;
                    info.Attributes = info.Attributes & ~FileAttributes.ReadOnly;
                }

                if (info.CreationTime > Args.SetTo || info.LastAccessTime > Args.SetTo || info.LastWriteTime > Args.SetTo ||
                    !Args.FutureOnly)
                {
                    Console.WriteLine(Messages.Touching, info.FullName);

                    info.CreationTime = Args.SetTo;
                    info.LastAccessTime = Args.SetTo;
                    info.LastWriteTime = Args.SetTo;
                }

                if (wasReadonly)
                {
                    info.Attributes = info.Attributes | FileAttributes.ReadOnly;
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(Messages.ErrorWhileTouching, info.FullName, e.Message);
            }
        }
    }
}