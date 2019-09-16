using System;

namespace JakubSturc.Talks.UnicodeWithDotNet
{
    public class ThrowException : ICommand
    {
        public string Code => "throw";

        public string Name => "Throw exception";

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}
