using System.Collections.Generic;
using System.Linq;
using XamarinFormsAssistant.Assistant.Commandments;
using XamarinFormsAssistant.Assistant.Fragments;

namespace XamarinFormsAssistant.Assistant.Extensions
{
    public static class SpeechCommandmentExtension
    {
        public static string ComposePhrase(this SpeechCommandment speechCommandment)
        {
            var list = new List<SpeechParameter> {speechCommandment.MainParameter};
            list.AddRange(speechCommandment.AdditionalParameters);
            return list.Aggregate("", (current, parameter) => current + $"{parameter.OriginalTextFragment} ");
        }
    }
}