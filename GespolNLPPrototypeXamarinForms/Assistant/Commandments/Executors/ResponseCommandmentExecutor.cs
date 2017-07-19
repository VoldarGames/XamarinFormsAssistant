using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Plugin.TextToSpeech;
using Xamarin.Forms;
using XamarinFormsAssistant.Assistant.Attributes;
using XamarinFormsAssistant.Assistant.Commandments.Executors.Manager;
using XamarinFormsAssistant.Assistant.Enums;
using XamarinFormsAssistant.Assistant.Extensions;
using XamarinFormsAssistant.Assistant.Modules;
using XamarinFormsAssistant.Assistant.SpeechNames.Manager;

namespace XamarinFormsAssistant.Assistant.Commandments.Executors
{
    public class ResponseCommandmentExecutor : CommandmentExecutorBase
    {
        public override void Execute(SpeechCommandment speechCommandment)
        {
            ResponseCommandmentExecutorHandler.Handle(Assistant.GetInstance().CurrentWaitingResponseType, speechCommandment);
        }
    }

    public static class ResponseCommandmentExecutorHandler
    {
        private static readonly Dictionary<WaitingResponseType,Action<SpeechCommandment>> ResponseCommandmentDictionary = new Dictionary<WaitingResponseType, Action<SpeechCommandment>>()
        {
            {WaitingResponseType.None, commandment => {} },
            {WaitingResponseType.Number, WaitingResponseNumberAction },
            {WaitingResponseType.YesNo, commandment => {throw new NotImplementedException();}},
            {WaitingResponseType.Filter, WaitingResponseFilterAction},
            {WaitingResponseType.ModuleFilter, WaitingResponseModuleFilterAction }
        };

        private static void WaitingResponseModuleFilterAction(SpeechCommandment speechCommandment)
        {
            var givenText = speechCommandment.MainParameter.OriginalTextFragment.ToUpper();
            if (string.Equals(givenText, "CANCELAR")
                || string.Equals(givenText, "CANCELA"))
            {
                MessagingCenter.Send(AssistantMessagingOrder.CancelSearch, AssistantMessagingOrder.CancelSearch);
                return;
            }

            var selectedModule = AssistantModuleNamesManager.GetModuleNames().FirstOrDefault(o => o.ToString().Equals(givenText));
            if (selectedModule != null)
            {
                MessagingCenter.Send(selectedModule.ToString(), AssistantMessagingOrder.ExecuteCreateCommand);
            }

        }

        public static void Handle(WaitingResponseType waitingResponseType ,SpeechCommandment speechCommandment)
        {
            ResponseCommandmentDictionary[waitingResponseType].Invoke(speechCommandment);
        }

        private static void WaitingResponseNumberAction(SpeechCommandment commandment)
        {
            if (commandment.MainParameter.OriginalTextFragment.Length != 1 || !char.IsDigit(commandment.MainParameter.OriginalTextFragment[0]))
            {
                WaitingResponseFeedbackHandler.Handle(Assistant.GetInstance().CurrentWaitingResponseType);
                return;
            }
            var index = int.Parse(commandment.MainParameter.OriginalTextFragment);
            if (index > 4) return;
            var currentInstanceType = Assistant.GetInstance().CurrentInstanceResult.GetType();
            var currentInstanceTypePrperty = currentInstanceType.GetProperty(Assistant.GetInstance().CurrentContextPropertyTypeName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            currentInstanceTypePrperty.SetValue(Assistant.GetInstance().CurrentInstanceResult, Assistant.GetInstance().SelectionResults[index]);
            DependencyService.Get<IOperatingSystemMethods>().Vibrate(500);


            Assistant.GetInstance().ClearWaitingResponse();

            var headerText = AssistantSpeechNamesManager.GetPropertyHeader(currentInstanceTypePrperty.Name);
            Assistant.GetInstance().CurrentInstanceResultChanged(headerText, $"{headerText}: {currentInstanceTypePrperty.GetValue(Assistant.GetInstance().CurrentInstanceResult) ?? "*Sin valor*"}");
        }

        private static void WaitingResponseFilterAction(SpeechCommandment commandment)
        {

            if(string.Equals(commandment.MainParameter.OriginalTextFragment.ToUpper(),"CANCELAR") 
                || string.Equals(commandment.MainParameter.OriginalTextFragment.ToUpper(), "CANCELA"))
            {
                MessagingCenter.Send(AssistantMessagingOrder.CancelSearch, AssistantMessagingOrder.CancelSearch);
                return;
            }


            var filteredSelectionResults =
                LevenshteinWrapper.GetLevenstheinContainsList(commandment.ComposePhrase(), Assistant.GetInstance().SelectionResults, 0.35f);
            //Assistant.GetInstance()
            //    .SelectionResults.Where(
            //        o => o.ToString().ToUpper().Contains(commandment.MainParameter.OriginalTextFragment.ToUpper())).ToList();
            
            if (filteredSelectionResults.Count() > 1)
            {
                Assistant.GetInstance().SelectionResults = filteredSelectionResults;
                Assistant.GetInstance().CurrentWaitingResponseType = WaitingResponseType.Filter;
            }
            else if (filteredSelectionResults.Count() == 1)
            {
                var currentInstanceTypePrperty =
                Assistant.GetInstance()
                    .CurrentContextType.GetPropertyInChildrenWithAttribute<AssistantRequiredFieldAttribute>(Assistant.GetInstance().CurrentContextPropertyTypeName);

                currentInstanceTypePrperty.SetChildrenValue(filteredSelectionResults.FirstOrDefault());

                //var currentInstanceTypePrperty =
                //    currentInstanceType
                //        .GetProperty(Assistant.GetInstance().CurrentContextPropertyTypeName,
                //            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                //currentInstanceTypePrperty
                //    .SetValue(Assistant.GetInstance().CurrentInstanceResult,
                //        filteredSelectionResults.FirstOrDefault());
                DependencyService.Get<IOperatingSystemMethods>().Vibrate(500);
                Assistant.GetInstance().ClearWaitingResponse();
                var headerText = AssistantSpeechNamesManager.GetPropertyHeader(currentInstanceTypePrperty.Name);
                Assistant.GetInstance().CurrentInstanceResultChanged(headerText, $"{headerText}: {currentInstanceTypePrperty.GetChildrenValue() ?? "*Sin valor*"}");
                MessagingCenter.Send(AssistantMessaging.ItemSelectedOnResults, AssistantMessaging.ItemSelectedOnResults);
            }
            else
            {
                CrossTextToSpeech.Current.Speak("Palabra no encontrada.");
            }
        }

        
    }
}