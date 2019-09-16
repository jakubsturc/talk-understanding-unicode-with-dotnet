using System;

namespace JakubSturc.Talks.UnicodeWithDotNet
{
    class ExitApplication : ICommand
    {
        public string Code => "exit";

        public string Name => "Exit application";

        public void Run()
        {
            Environment.Exit(0);
        }
    }
}
