using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinFormsAssistant.Assistant.Viewmodels
{
    public interface IAssistantSelectionPageViewModel
    {
        string HeaderSuffix { get; }
        string BackgroundAnimationName{ get; }
        ICommand CancelCommand { get; set; }
        void OnSelectionResultsListViewOnItemTapped(object sender, ItemTappedEventArgs args);
        ObservableCollection<object> SelectionResults { get; set; }
    }
}