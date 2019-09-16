using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JakubSturc.Talks.UnicodeWithDotNet
{
    class Program
    {
        private static IDictionary<string, ICommand> _commands;

        static void Main(string[] args)
        {
            Init();

            while (true)
            {
                try
                {
                    string str = GetInput();
                    Execute(str);
                }
                catch (Exception e)
                {
                    Print(e);
                }
            }
        }

        private static void Execute(string input)
        {
            if (_commands.TryGetValue(input, out ICommand cmd))
            {
                cmd.Run();
            }
            else
            {
                Console.WriteLine($"\"{input}\"is not supported");
                PrintHelp();
            }
        }

        internal static void PrintHelp()
        {
            Console.WriteLine("Supported commands");
            var orderedCommands = _commands.OrderBy(_ => _.Key).Select(_ => _.Value);
            foreach (var cmd in orderedCommands)
            {
                Console.WriteLine($"  {cmd.Code}: {cmd.Name}");
            }
            Console.WriteLine();
        }

        private static string GetInput()
        {
            Console.Write("> ");
            var line = Console.ReadLine();
            return line;
        }

        private static void Init()
        {
            _commands = LoadCommands();
        }

        private static void Print(Exception e)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(e);
            Console.ForegroundColor = originalColor;
        }

        private static bool IsExit(string str) => String.Compare(str, "exit", StringComparison.CurrentCultureIgnoreCase) == 0;

        private static IDictionary<string, ICommand> LoadCommands()
        {
            var icommand = typeof(ICommand);
            var implementations = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(t => t.IsClass & !t.IsAbstract)
                .Where(c => icommand.IsAssignableFrom(c));
            var instances = implementations.Select(c => (ICommand)Activator.CreateInstance(c));
            return instances.ToDictionary(i => i.Code);
        }
    }
}
