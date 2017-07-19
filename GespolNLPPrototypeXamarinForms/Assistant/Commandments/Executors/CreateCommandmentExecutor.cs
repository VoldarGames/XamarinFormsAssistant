using Xamarin.Forms;
using XamarinFormsAssistant.Assistant.Enums;
using XamarinFormsAssistant.Assistant.Modules;

namespace XamarinFormsAssistant.Assistant.Commandments.Executors
{
    public class CreateCommandmentExecutor : CommandmentExecutorBase
    {
        public override void Execute(SpeechCommandment speechCommandment)
        {

            var newContextType = AssistantModuleNamesManager.GetModuleEntityType(
                speechCommandment.MainParameter.OriginalTextFragment.ToUpper());

            if (newContextType == null)
            {
                Assistant.GetInstance().CurrentWaitingResponseType = WaitingResponseType.ModuleFilter;
                MessagingCenter.Send(AssistantMessaging.ModuleNotFound, AssistantMessaging.ModuleNotFound);
                return;
            }
            Assistant.GetInstance().CurrentContextType = newContextType;
            Assistant.GetInstance().CurrentContextTypeName =
                speechCommandment.MainParameter.OriginalTextFragment.ToUpper();
        }
    }
}