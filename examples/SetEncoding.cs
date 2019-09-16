using System;
using System.Collections.Generic;
using System.Text;

namespace JakubSturc.Talks.UnicodeWithDotNet
{
    public class SetEncoding : ICommand
    {
        public string Code => "enc";

        public string Name => "Set console encoding";

        public void Run()
        {
            Console.WriteLine($"Current input encoding: {Console.InputEncoding.WebName}");
            Console.WriteLine($"Current output encoding: {Console.OutputEncoding.WebName}");
            Console.WriteLine();
            Console.WriteLine("Select endcoding:");
            Console.WriteLine("1. ASCII");
            Console.WriteLine("2. UTF8");
            Console.WriteLine("3. UTF16");
            Console.WriteLine("4. UTF32");
            Console.WriteLine("0. Cancel");
            Console.Write("> ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    return;
                case "1":
                    Set(Encoding.ASCII);
                    break;
                case "2":
                    Set(Encoding.UTF8);
                    break;
                case "3":
                    Set(Encoding.Unicode);
                    break;
                case "4":
                    Set(Encoding.UTF32);
                    break;
                default:
                    Run();
                    break;
            }

            Console.WriteLine($"Current input encoding: {Console.InputEncoding.WebName}");
            Console.WriteLine($"Current output encoding: {Console.OutputEncoding.WebName}");
        }

        private void Set(Encoding enc)
        {
            Console.WriteLine($"Setting: {enc.WebName}");
            Console.OutputEncoding = enc;
            Console.InputEncoding = enc;
        }
    }
}
