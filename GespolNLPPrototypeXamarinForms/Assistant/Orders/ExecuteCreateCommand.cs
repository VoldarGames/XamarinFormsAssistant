using Xamarin.Forms;
using XamarinFormsAssistant.Assistant.Commandments;
using XamarinFormsAssistant.Assistant.Commandments.Executors;
using XamarinFormsAssistant.Assistant.Enums;
using XamarinFormsAssistant.Assistant.Fragments;

namespace XamarinFormsAssistant.Assistant.Orders
{
    public class ExecuteCreateCommand : Order {
        private readonly string _selectedModuleString;

        public ExecuteCreateCommand(string selectedModuleString)
        {
            _selectedModuleString = selectedModuleString;
        }
        public override void Execute()
        {
            var executor = new CreateCommandmentExecutor();
            executor.Execute(new SpeechCommandment()
            {
                MainCommand = new SpeechCommand()
                {
                    SpeechCommandType = SpeechCommandType.Create
                },
                MainParameter = new SpeechParameter()
                {
                    SpeechParameterType = SpeechParameterType.Text,
                    OriginalTextFragment = _selectedModuleString
                }
            });

            Assistant.GetInstance().ClearWaitingResponse();
            MessagingCenter.Send(AssistantMessaging.ItemSelectedOnResults, AssistantMessaging.ItemSelectedOnResults);
        }
    }
}