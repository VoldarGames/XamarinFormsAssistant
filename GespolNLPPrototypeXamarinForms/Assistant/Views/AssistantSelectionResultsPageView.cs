using Xamarin.Forms;
using XamarinFormsAssistant.Assistant.Converters;
using XamarinFormsAssistant.Assistant.Global.Animations;
using XamarinFormsAssistant.Assistant.Global.Styles;
using XamarinFormsAssistant.Assistant.SpeechNames.Manager;
using XamarinFormsAssistant.Assistant.Viewmodels;
using XamarinFormsAssistant.Assistant.Views.Components;

namespace XamarinFormsAssistant.Assistant.Views
{

    public class AssistantSelectionResultsPageView : ContentPage
    {
        public HeaderLabelStack HeaderLabelStack { get; set; }
        public StackLayout SelectionResultsStackLayout { get; set; } = new StackLayout();
        public ListView SelectionResultsListView { get; set; } = new ListView();

        private readonly IAssistantSelectionPageViewModel _context;

        public AssistantSelectionResultsPageView(IAssistantSelectionPageViewModel context)
        {
            BindingContext = context;
            _context = context;
            BackgroundColor = Color.FromRgb(10, 10, 32);
            WireAnimations();
            SelectionResultsListView.HasUnevenRows = true;
            SelectionResultsListView.ItemTemplate = new DataTemplate(() =>
            {
                var cell = new ViewCell();
                var label = new Label
                {
                    FontSize = 16
                };
                label.SetBinding(Label.TextProperty, ".", BindingMode.Default, new ToStringConverter());
                cell.Appearing += (sender, args) =>
                {
                    
                };
                
                cell.View = label;
                return cell;
            });

            SelectionResultsListView.SetBinding(ListView.ItemsSourceProperty, nameof(context.SelectionResults));
            SelectionResultsListView.ItemTapped += context.OnSelectionResultsListViewOnItemTapped;

            var suffix = AssistantSpeechNamesManager.GetPropertyHeader(Assistant.GetInstance().CurrentContextPropertyTypeName);
            

            HeaderLabelStack = new HeaderLabelStack()
            {
                HeaderLabel =
                {
                    Text = $"{AssistantResources_ES.SelectHeader} {suffix}",
                    FontSize = 24,
                    HorizontalOptions = LayoutOptions.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                }
            };

            SelectionResultsStackLayout.Children.Add(HeaderLabelStack);
            SelectionResultsStackLayout.Children.Add(SelectionResultsListView);
            SelectionResultsStackLayout.Children.Add(new Button()
            {
                Text = AssistantResources_ES.CancelSearch,
                Command = context.CancelCommand,
                Style = AssistantStyles.CancelButton
            });

            Content = SelectionResultsStackLayout;
        }

        private void WireAnimations()
        {
            MessagingCenter.Subscribe<string>(this, AssistantAnimations.BackgroundColor.OnName, s =>
            {
                AssistantAnimations.BackgroundColor.AnimateOn(this, _context.BackgroundAnimationName);
            });

            MessagingCenter.Subscribe<string>(this, AssistantAnimations.BackgroundColor.OffName, s =>
            {
                AssistantAnimations.BackgroundColor.AnimateOff(this, _context.BackgroundAnimationName);
            });
        }
    }
}