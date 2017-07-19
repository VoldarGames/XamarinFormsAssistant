using XamarinFormsAssistant.Assistant.Viewmodels;

namespace XamarinFormsAssistant.Prototype.Application
{
    public class MyApp : Xamarin.Forms.Application
    {
        public MyApp()
        {
            var assistantViewModel = new AssistantPageViewModel();
            var mainPage = assistantViewModel.GetView();
            NavigationService.Navigation = mainPage.Navigation;
            MainPage = mainPage;
        }
    }
}