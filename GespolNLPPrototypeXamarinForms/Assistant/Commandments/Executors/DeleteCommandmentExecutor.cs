using System;
using Xamarin.Forms;
using XamarinFormsAssistant.Assistant.SpeechNames.Manager;

namespace XamarinFormsAssistant.Assistant.Commandments.Executors
{
    public class DeleteCommandmentExecutor : CommandmentExecutorBase
    {
        public override void Execute(SpeechCommandment speechCommandment)
        {
            if (Assistant.GetInstance().CurrentContextType == null) return;

            if (Assistant.GetInstance().CurrentInstanceResult == null) return;
            
            foreach (var propertyInfo in Assistant.GetInstance().CurrentContextType.GetProperties())
            {
                if (string.Equals(propertyInfo.Name, speechCommandment.MainParameter.OriginalTextFragment, StringComparison.CurrentCultureIgnoreCase))
                {
                    propertyInfo.SetValue(Assistant.GetInstance().CurrentInstanceResult, null, null);
                    var headerText = AssistantSpeechNamesManager.GetPropertyHeader(propertyInfo.Name);
                    Assistant.GetInstance().CurrentInstanceResultChanged(headerText, $"{headerText}: {propertyInfo.GetValue(Assistant.GetInstance().CurrentInstanceResult) ?? "*Sin valor*"}");
                    DependencyService.Get<IOperatingSystemMethods>().Vibrate(500);
                }
            }
        }
    }
}