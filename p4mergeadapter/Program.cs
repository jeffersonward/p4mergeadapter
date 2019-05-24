using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace p4mergeadapter
{
    internal class Program
    {
        private static readonly string WorkingDirectory = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));

        private static void Main(string[] args)
        {
#if DEBUG
            foreach (var arg in args)
            {
                File.AppendAllText("C:\\temp\\perforce.log", arg + "\r\n");
            }

            File.AppendAllText("C:\\temp\\perforce.log", WorkingDirectory + "\r\n");
#endif

            try
            {
                using (var fs = File.Create(args[3]))
                {
                }

                var process = new Process
                {
                    StartInfo =
                    {
                        FileName = "p4merge.exe",
                        WorkingDirectory = WorkingDirectory,
                        Arguments = string.Format("-nb \"{6}\" -nl \"{4}\" -nr \"{5}\" -nm \"{7}\" \"{2}\" \"{0}\" \"{1}\" \"{3}\"", args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7])
                    }
                };

                process.Start();
                process.WaitForExit();
            }
            catch (Exception e)
            {
                File.AppendAllText("C:\\temp\\perforce.log", e + "\r\n");
                throw;
            }
        }
    }
}