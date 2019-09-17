using System;
using System.Diagnostics;
using System.IO;

namespace JakubSturc.Talks.UnicodeWithDotNet
{
    public class DisplayDebugInfo : ICommand
    {
        public string Code => "debug";

        public string Name => "Display debug info";

        public void Run()
        {
            Console.WriteLine($"Current process ID is {Process.GetCurrentProcess().Id}");
            Console.WriteLine($"Current directory is '{Directory.GetCurrentDirectory()}'");
        }
    }
}
