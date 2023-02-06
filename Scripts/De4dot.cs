using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace PiePopper
{
    /// <summary>
    /// De4dot wrapper.
    /// </summary>
    public static class De4dot
    {
        /// <summary>
        /// Cleans a dll.
        /// </summary>
        public static bool Clean(string source, out string cleanedFile)
        {
            ProcessStartInfo info = new ProcessStartInfo()
            {
                FileName = Path.Combine(Directory.GetParent(typeof(De4dot).Assembly.Location).FullName, "binaries", "de4dot.exe"),
                Arguments = $"\"{source}\"",

                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardInput = true
            };

            Process de4 = Process.Start(info);
            Thread.Sleep(TimeSpan.FromSeconds(3));
            de4.StandardInput.WriteLine("q");
            
            cleanedFile = Path.Combine(Directory.GetParent(source).FullName, $"{Path.GetFileNameWithoutExtension(source)}-cleaned.dll");
            return true;
        }
    }
}