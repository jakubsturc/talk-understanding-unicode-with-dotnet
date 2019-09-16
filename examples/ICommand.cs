namespace JakubSturc.Talks.UnicodeWithDotNet
{
    interface ICommand
    {
        string Code { get; }
        string Name { get; }
        void Run();
    }
}
