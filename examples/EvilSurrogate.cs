using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JakubSturc.Talks.UnicodeWithDotNet
{
    /// <remarks>
    /// Source: https://codeblog.jonskeet.uk/2014/11/07/when-is-a-string-not-a-string/
    /// </remarks>
    [Description(Value)]
    public class EvilSurrogate : ICommand
    {
        public const string Value = "X\ud800Y";

        public string Code => "evil";

        public string Name => "Evil surrogate";

        public void Run()
        {
            var description = (DescriptionAttribute)typeof(EvilSurrogate)
                .GetCustomAttributes(typeof(DescriptionAttribute), true)[0];
            DumpString("Attribute", description.Description);
            DumpString("Constant", Value);
        }

        static void DumpString(string name, string text)
        {
            var utf16 = text.Select(c => ((uint)c).ToString("x4"));
            Console.WriteLine("{0}: {1}", name, string.Join(" ", utf16));
        }
    }
}
