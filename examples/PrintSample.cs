using System;
using System.Collections.Generic;
using System.Text;

namespace JakubSturc.Talks.UnicodeWithDotNet
{
    public class PrintSample : ICommand
    {
        public string Code => "sample";

        public string Name => "Print sample text";

        public void Run()
        {
            Console.WriteLine($"Slovak Alphabet: {SlovakAlphabet}");
            Console.WriteLine($"Greek Alphabet: {GreekAlphabet}");
            Console.WriteLine($"Hebrew Alphabet: {HebrewAlphabet}");
            Console.WriteLine($"Some Hangul: {SomeHangul}");
            Console.WriteLine($"Simplified Chinese: {SimplifiedChinese}");
            Console.WriteLine($"Basic Emoji: {BasicEmoji}");
            Console.WriteLine($"Emoji Flags: {EmojiFlags}");
            Console.WriteLine($"Emoji Skins: {EmojiSkins}");
        }

        private static string SlovakAlphabet = "AÁÄBCČDĎDzDžEÉFGHChIÍJKLĹĽMNŇOÓÔPQRŔSŠTŤUÚVWXYÝZŽ";
        private static string GreekAlphabet = "αβγδεζηθικλμνξοπρστυφχψω";
        private static string HebrewAlphabet = GetUtf32Range(0x05D0, 0x05EA);
        private static string SomeHangul = "한글은 발음기관과 하늘";
        private static string SimplifiedChinese = "簡化字";
        private static string BasicEmoji = "🎰 🎱 🎲 🔮 ✨";
        private static string EmojiFlags = "🇪🇺 🇸🇰 🏳️‍🌈";
        private static string EmojiSkins = "🧙 🧙🏻 🧙🏼 🧙🏽 🧙🏾 🧙🏿";


        private static string GetUtf32Range(int v1, int v2)
        {
            var sb = new StringBuilder(v2-v1+1);
            for (int i = 0; i + v1 <= v2; i++)
            {
                sb.Append(Char.ConvertFromUtf32(i + v1));
            }
            return sb.ToString();
        }
    }
}
