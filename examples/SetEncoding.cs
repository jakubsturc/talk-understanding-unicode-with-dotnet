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

            var encodings = Encoding.GetEncodings();
            for (int i = 0; i < encodings.Length; i++)
            {
                Console.WriteLine($"{i}. {encodings[i].Name}");
            }
                Console.Write("> ");

            var input = Console.ReadLine();

            if (int.TryParse(input, out int index))
            {
                Set(Encoding.GetEncoding(encodings[index].CodePage));
            }
            else
            {
                return;
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
