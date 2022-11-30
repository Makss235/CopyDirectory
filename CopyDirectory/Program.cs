using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CopyDirectory
{
    internal class Program
    {
        public static List<string> Folders = new List<string>()
        {
            "Resources",
            "Python310",
            "Speech"
        };

        static public string CopyFrom { get; } = Path.Combine(
                Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location));
        static public string CopyTo { get; } = Path.Combine(
                Path.GetDirectoryName(Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location)), 
                "SmartAssistant\\bin\\Debug\\net6.0-windows");

        static void Main(string[] args)
        {
            Console.WriteLine(CopyFrom);
            Console.WriteLine(CopyTo);
            foreach (string folder in Folders)
            {
                Copy(Path.Combine(CopyFrom, folder), Path.Combine(CopyTo, folder));
                Console.WriteLine($"Copying completed: {folder}");
                Console.WriteLine();
            }
        }

        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
}
