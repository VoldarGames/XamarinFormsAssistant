using System.Linq;

namespace XamarinFormsAssistant.Assistant.Extensions
{
    public static class StringExtension
    {
        public static string ReplaceAllSymbols(this string s, string sustitute)
        {
            return s.Aggregate("", (current, character) => current + (char.IsLetterOrDigit(character) ? character.ToString() : sustitute));
        }
    }
}