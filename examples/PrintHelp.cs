namespace JakubSturc.Talks.UnicodeWithDotNet
{
    public class PrintHelp : ICommand
    {
        public string Code => "help";

        public string Name => "Print help";

        public void Run()
        {
            Program.PrintHelp();
        }
    }
}
