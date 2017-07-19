using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinFormsAssistant.Assistant.Modules;
using XamarinFormsAssistant.Assistant.Views;

namespace XamarinFormsAssistant.Assistant.Viewmodels
{
    public class AssistantSelectionModulesPageViewModel : IAssistantSelectionPageViewModel
    {
        public string HeaderSuffix { get; } = AssistantResources_ES.Module;
        public string BackgroundAnimationName { get; } = "BackgroundColorModal2";
        public ICommand CancelCommand { get; set; } = new Command(() => MessagingCenter.Send(AssistantMessagingOrder.CancelSearch, AssistantMessagingOrder.CancelSearch));
        public void OnSelectionResultsListViewOnItemTapped(object sender, ItemTappedEventArgs args)
        {
            MessagingCenter.Send(args.Item.ToString(), AssistantMessagingOrder.ExecuteCreateCommand);
        }

        public ObservableCollection<object> SelectionResults { get; set; } = AssistantModuleNamesManager.GetModuleNames();

        public ContentPage GetView()
        {
            return new AssistantSelectionResultsPageView(this);
        }
        
    }
}