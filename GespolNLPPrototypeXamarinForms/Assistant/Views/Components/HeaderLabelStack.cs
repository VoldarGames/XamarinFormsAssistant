using Xamarin.Forms;

namespace XamarinFormsAssistant.Assistant.Views.Components
{
    public class HeaderLabelStack : StackLayout
    {
        public Label HeaderLabel { get; set; } = new Label();

        public HeaderLabelStack()
        {
            HeaderLabel.FontSize = 30;
            HeaderLabel.HorizontalTextAlignment = TextAlignment.Center;
            HeaderLabel.HorizontalOptions = LayoutOptions.CenterAndExpand;
            HeaderLabel.TextColor = Color.FromRgb(10, 10, 32);
            BackgroundColor = Color.Snow;
            Children.Add(HeaderLabel);
        }
        public HeaderLabelStack(string labelTextBindingProperty)
        {
            HeaderLabel.FontSize = 30;
            HeaderLabel.HorizontalTextAlignment = TextAlignment.Center;
            HeaderLabel.HorizontalOptions = LayoutOptions.CenterAndExpand;
            HeaderLabel.TextColor = Color.FromRgb(10, 10, 32);
            HeaderLabel.SetBinding(Label.TextProperty, labelTextBindingProperty);


            BackgroundColor = Color.Snow;
            Children.Add(HeaderLabel);
        }
    }
}