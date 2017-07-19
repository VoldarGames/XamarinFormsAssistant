using Plugin.TextToSpeech;
using Xamarin.Forms;

namespace XamarinFormsAssistant.Assistant.Orders
{
    public class CancelSearchOrder : Order
    {
        public override void Execute()
        {
            Assistant.GetInstance().ClearWaitingResponse();
            Assistant.GetInstance().CurrentInstanceResultChanged();
            CrossTextToSpeech.Current.Speak("Búsqueda cancelada.");
            MessagingCenter.Send(AssistantMessaging.ItemSelectedOnResults, AssistantMessaging.ItemSelectedOnResults);
        }
    }
}