using System;
using System.Collections.Generic;
using System.Text;

namespace JakubSturc.Talks.UnicodeWithDotNet
{
    public class ClearConsole : ICommand
    {
        public string Code => "cls";

        public string Name => "Clear console";

        public void Run()
        {
            Console.Clear();
        }
    }
}
