using System;
using System.Collections.Generic;
using System.IO;
using DateFix.Properties;

namespace DateFix
{
    internal static class Fixer
    {
        private static readonly Queue<DirectoryInfo> Paths = new Queue<DirectoryInfo>();

        public static void Run()
        {
            try
            {
                FileAttributes attr = File.GetAttributes(Args.Path);

                if (attr.HasFlag(FileAttributes.Directory))
                    ProcessPath(new DirectoryInfo(Args.Path));
                else
                    Touch(new FileInfo(Args.Path));

                while (Paths.Count > 0)
                {
                    DirectoryInfo dir = Paths.Dequeue();

                    ProcessContents(dir);
                }
            }
            catch (Exception e)
            {
                Log.Error(Resources.ErrorWhileProcessing, Args.Path, e.Message);
            }
        }

        private static void ProcessContents(DirectoryInfo dir)
        {
            try
            {
                Log.Info(Resources.Processing, dir.FullName);

                foreach (FileInfo file in dir.EnumerateFiles())
                {
                    Touch(file);
                }

                foreach (DirectoryInfo sub in dir.EnumerateDirectories())
                {
                    ProcessPath(sub);
                }
            }
            catch (Exception e)
            {
                Log.Error(Resources.ErrorWhileProcessing, dir.FullName, e.Message);
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
                FileAttributes attr = info.Attributes;

                bool wasReadonly = false;

                if (attr.HasFlag(FileAttributes.ReadOnly))
                {
                    if (!Args.TouchReadonly)
                        return;

                    wasReadonly = true;
                    info.Attributes = info.Attributes & ~FileAttributes.ReadOnly;
                }

                if (info.CreationTime > Args.SetTo ||
                    info.LastAccessTime > Args.SetTo ||
                    info.LastWriteTime > Args.SetTo ||
                    !Args.FutureOnly)
                {
                    Log.Info(Resources.Touching, info.FullName);

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
                Log.Error(Resources.ErrorWhileTouching, info.FullName, e.Message);
            }
        }
    }
}