using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JakubSturc.Talks.UnicodeWithDotNet
{
    public class InspectFile : ICommand
    {
        private Dictionary<int, string> _files;

        public string Code => "ins";

        public string Name => "Inspect a file";

        public void Run()
        {
            Console.WriteLine("Select a file:");

            Init();

            var file = EnterFile();
            var text = File.ReadAllText(file);
            var bytes = File.ReadAllBytes(file);

            Console.WriteLine(text);

            foreach (var b in bytes)
            {
                Console.Write("{0:x2} ", b);
            }

            Console.WriteLine();

        }

        private string EnterFile()
        {
            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out int fileIndex))
                {
                    if (fileIndex == 0)
                    {
                        return null;
                    }

                    if (_files.TryGetValue(fileIndex, out var file))
                    {
                        return file;
                    }
                }
            }
        }

        private void Init()
        {
            var currentDir = Directory.GetCurrentDirectory();
            int i = 0;
            _files = Directory
                .GetFiles(currentDir, "*.txt", SearchOption.TopDirectoryOnly)
                .ToDictionary(_ => ++i, file => file);

            foreach (var kvp in _files)
            {
                Console.WriteLine($"{kvp.Key}. {kvp.Value}");
            }

            Console.WriteLine($"0. Cancel");
        }
    }
}
