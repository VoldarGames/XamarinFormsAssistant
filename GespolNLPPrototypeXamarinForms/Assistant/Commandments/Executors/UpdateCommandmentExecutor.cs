using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Plugin.TextToSpeech;
using Xamarin.Forms;
using XamarinFormsAssistant.Assistant.Attributes;
using XamarinFormsAssistant.Assistant.Commandments.Executors.Handlers;
using XamarinFormsAssistant.Assistant.Commandments.Executors.Manager;
using XamarinFormsAssistant.Assistant.Enums;
using XamarinFormsAssistant.Assistant.SpeechNames.Manager;
using XamarinFormsAssistant.Prototype.Model;

namespace XamarinFormsAssistant.Assistant.Commandments.Executors
{
    public class UpdateCommandmentExecutor : CommandmentExecutorBase
    {
        public static readonly string[] PlateFormatsRegex =
        {
            "[0-9]{4}[A-Z]{3}", "[A-Z]{1}[0-9]{4}[A-Z]{2}",
            "[A-Z]{2}[0-9]{4}[A-Z]{2}"
        };

        public override void Execute(SpeechCommandment speechCommandment)
        {
            if (Assistant.GetInstance().CurrentContextType == null) return;

            IsSearchProperty(speechCommandment);
            ActivateCurrentInstanceResultIfNecessary();
            HandleRequestedProperty(speechCommandment);
        }

        private void HandleRequestedProperty(SpeechCommandment speechCommandment)
        {
            var propertyInfo =
                Assistant.GetInstance()
                    .CurrentContextType.GetPropertyInChildrenWithAttribute<AssistantRequiredFieldAttribute>(
                        speechCommandment.MainParameter.OriginalTextFragment);
            
            HandlePropertyAttributes(propertyInfo, speechCommandment);

        }

        private static void ActivateCurrentInstanceResultIfNecessary()
        {
            if (Assistant.GetInstance().CurrentInstanceResult == null)
            {
                Assistant.GetInstance().CurrentInstanceResult =
                    Activator.CreateInstance(Assistant.GetInstance().CurrentContextType);
            }
        }

        private static void IsSearchProperty(SpeechCommandment speechCommandment)
        {
            Assistant.GetInstance().CurrentContextPropertyTypeName =
                speechCommandment.MainParameter.OriginalTextFragment;
        }

        private void HandlePropertyAttributes(PropertyInfo propertyInfo, SpeechCommandment speechCommandment)
        {
            //TODO: REFACTOR

            #region SearchFieldAttribute

            if (propertyInfo.GetCustomAttribute(typeof(AssistantSearchFieldAttribute)) != null)
            {
                var getHandlerMethod =
                    typeof(SearchHandlerManager).GetMethod(nameof(SearchHandlerManager.GetHandler))
                        .MakeGenericMethod(propertyInfo.PropertyType);
                var handler = getHandlerMethod.Invoke(null, new object[] {propertyInfo.PropertyType});
                var results = handler.GetType()
                    .GetMethod(nameof(SearchHandler<EntityBase>.Search))
                    .Invoke(handler, new object[] {speechCommandment.AdditionalParameters});

                if (((IList) results).Count > 1)
                {
                    Assistant.GetInstance().SelectionResults = ((IList) results).Cast<EntityBase>().ToList();
                    //CastListToTypedList<EntityBase>(results);

                    Assistant.GetInstance().CurrentWaitingResponseType = WaitingResponseType.Filter;
                }
                if (((IList) results).Count == 1)
                {
                    propertyInfo.SetChildrenValue(((IList) results)[0]);
                    //propertyInfo.SetValue(Assistant.GetInstance().CurrentInstanceResult, ((IList) results)[0]);
                    Assistant.GetInstance().ClearWaitingResponse();
                    DependencyService.Get<IOperatingSystemMethods>().Vibrate(500);
                }
                if (((IList) results).Count == 0)
                {
                    CrossTextToSpeech.Current.Speak(
                        $"{speechCommandment.MainParameter.OriginalTextFragment} no encontrado");
                }

            }

            #endregion


            #region PlateFieldAttribute

            if (propertyInfo.GetCustomAttribute(typeof(AssistantPlateFieldAttribute)) != null)
            {
                var plate = FindPlate(speechCommandment);
                if (!string.IsNullOrEmpty(plate))
                {
                    propertyInfo.SetChildrenValue(plate);
                    DependencyService.Get<IOperatingSystemMethods>().Vibrate(500);
                }
                else
                {
                    CrossTextToSpeech.Current.Speak("Repita la matrícula por favor.");
                }
            }

            #endregion


            #region RawStringFieldAttribute

            if (propertyInfo.GetCustomAttribute(typeof(AssistantRawStringFieldAttribute)) != null)
            {

                //propertyInfo.SetValue(Assistant.GetInstance().CurrentInstanceResult,speechCommandment.AdditionalParameters.FirstOrDefault().OriginalTextFragment);
                propertyInfo.SetChildrenValue(speechCommandment.AdditionalParameters.FirstOrDefault()?.OriginalTextFragment);
                DependencyService.Get<IOperatingSystemMethods>().Vibrate(500);
            }

            #endregion
            
            #region RawStringPhraseFieldAttribute

            if (propertyInfo.GetCustomAttribute(typeof(AssistantRawStringPhraseFieldAttribute)) != null)
            {
                var setValue = speechCommandment.AdditionalParameters.Aggregate(string.Empty, (current, additionalParameter) => current + $"{additionalParameter.OriginalTextFragment} ");


                propertyInfo.SetChildrenValue(setValue);
                DependencyService.Get<IOperatingSystemMethods>().Vibrate(500);
            }

            #endregion


            #region RawIntFieldAttribute

            if (propertyInfo.GetCustomAttribute(typeof(AssistantRawIntFieldAttribute)) != null)
            {
                int setValue;
                int.TryParse(speechCommandment.AdditionalParameters.FirstOrDefault().OriginalTextFragment,out setValue);
                propertyInfo.SetChildrenValue(setValue);
                DependencyService.Get<IOperatingSystemMethods>().Vibrate(500);
            }

            #endregion

            //var headerText = AssistantSpeechNamesManager.GetPropertyHeader(Assistant.GetInstance().CurrentContextType, propertyInfo.Name);
            var headerText = AssistantSpeechNamesManager.GetPropertyHeader(propertyInfo.Name);
            var currentInstance = Assistant.GetInstance().CurrentInstanceResult;
            Assistant.GetInstance().CurrentInstanceResultChanged(headerText,$"{headerText}: {propertyInfo.GetChildrenValue() ?? "*Sin valor*"}");

        }

        /// <summary>
        /// Implementation of Cast<>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <returns></returns>
        private IList<T> CastListToTypedList<T>(object results)
        {
            var result = new List<T>();

            foreach (var obj in (IList) results)
            {
                result.Add((T) obj);
            }
            return result;
        }

        private string FindPlate(SpeechCommandment speechCommandment)
        {
            var possiblePlate = "";
            foreach (var additionalParameter in speechCommandment.AdditionalParameters)
            {
                possiblePlate += $"{additionalParameter.OriginalTextFragment.ToUpper()} ";
            }

            possiblePlate = FilterSpeechTextResult(possiblePlate);

            return FindPlateResults(possiblePlate) ? possiblePlate : string.Empty;
        }

        private string FilterSpeechTextResult(string speechTextResult)
        {
            var result = "";

            var splitted = speechTextResult.Split(' ');
            foreach (var s in splitted)
            {
                if (s.Length > 3 && !char.IsDigit(s.FirstOrDefault())) //Palabra, cogemos solo el primero
                {
                    result += s.FirstOrDefault();
                }
                else
                {
                    result += s;
                }

            }
            return result;
        }

        public static bool FindPlateResults(string text)
        {
            text = text.ToUpper();

            foreach (var plateFormat in PlateFormatsRegex)
            {
                var matchCollectionList = Regex.Matches(text, plateFormat);

                if (matchCollectionList.Count > 0) return true;
            }

            return false;

        }

    }
}