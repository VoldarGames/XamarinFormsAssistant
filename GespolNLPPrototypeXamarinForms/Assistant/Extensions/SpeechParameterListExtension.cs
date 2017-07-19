using System.Collections.Generic;
using System.Linq;
using XamarinFormsAssistant.Assistant.Fragments;

namespace XamarinFormsAssistant.Assistant.Extensions
{
    public static class SpeechParameterListExtension
    {
        public static string ComposePhrase(this List<SpeechParameter> list)
        {
            return list.Aggregate("", (current, parameter) => current + $"{parameter.OriginalTextFragment} ");
        }
    }
}