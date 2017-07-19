using System;
using System.Collections.Generic;
using Plugin.TextToSpeech;
using XamarinFormsAssistant.Assistant.Enums;

namespace XamarinFormsAssistant.Assistant
{
    public static class WaitingResponseFeedbackHandler
    {
        private static readonly Dictionary<WaitingResponseType, Action> WaitingResponseDictionary = new Dictionary<WaitingResponseType, Action>()
        {
            {WaitingResponseType.None, () => { CrossTextToSpeech.Current.Speak("Esperando comando"); } },
            {WaitingResponseType.YesNo, () => { CrossTextToSpeech.Current.Speak("�Est� usted seguro?"); } },
            {WaitingResponseType.Number, () =>
            {
                var speakText =
                    $"Diga un n�mero para escoger {Assistant.GetInstance().CurrentContextPropertyTypeName}";
                CrossTextToSpeech.Current.Speak(speakText);
            }},
            {WaitingResponseType.Filter, () =>
            {
                var speakText =
                    $"Concrete {Assistant.GetInstance().CurrentContextPropertyTypeName}";
                CrossTextToSpeech.Current.Speak(speakText);
            }},
            {WaitingResponseType.ModuleFilter, () =>
            {
                var speakText = $"Seleccione un m�dulo";
                CrossTextToSpeech.Current.Speak(speakText);
            }}

        };
        public static void Handle(WaitingResponseType value)
        {
            WaitingResponseDictionary[value].Invoke();
        }
    }
}