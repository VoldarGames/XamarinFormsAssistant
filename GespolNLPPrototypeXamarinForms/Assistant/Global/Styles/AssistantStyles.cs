using Xamarin.Forms;

namespace XamarinFormsAssistant.Assistant.Global.Styles
{
    public static class AssistantStyles
    {
        public static Style CancelButton { get; set; } = new Style(typeof(Button))
        {
            Setters =
            {
                new Setter() {Property = VisualElement.BackgroundColorProperty,Value = Color.SlateGray},
                new Setter() {Property = Button.TextColorProperty, Value = Color.Snow},
                new Setter() {Property = View.HorizontalOptionsProperty, Value = LayoutOptions.FillAndExpand},
                new Setter() {Property = Button.TextProperty, Value = AssistantResources_ES.Cancel}
            }
        };

        public static Style AcceptButton { get; set; } = new Style(typeof(Button))
        {
            Setters =
            {
                new Setter() {Property = VisualElement.BackgroundColorProperty,Value = Color.Snow},
                new Setter() {Property = Button.TextColorProperty, Value = Color.SlateGray},
                new Setter() {Property = View.HorizontalOptionsProperty, Value = LayoutOptions.FillAndExpand},
                new Setter() {Property = Button.TextProperty, Value = AssistantResources_ES.Accept}
            }
        };

        public static Style AssistantCube { get; set; } = new Style(typeof(Frame))
        {
            Setters =
            {
                new Setter() {Property = Frame.CornerRadiusProperty, Value = 5},
                new Setter() {Property = View.HorizontalOptionsProperty, Value = LayoutOptions.CenterAndExpand},
                new Setter() {Property = View.VerticalOptionsProperty, Value = LayoutOptions.End},
                new Setter() {Property = View.MarginProperty, Value = new Thickness(0)},
                new Setter() {Property = Frame.OutlineColorProperty, Value = Color.FromRgb(10,10,10)},
                new Setter() {Property = VisualElement.BackgroundColorProperty, Value = Color.FromRgb(15,15,15)},
                new Setter() {Property = VisualElement.IsVisibleProperty, Value = false}
                
            }
        };

        public static Style AssistantCellStackLayout { get; set; } = new Style(typeof(StackLayout))
        {
            Setters =
            {
                new Setter() {Property = StackLayout.OrientationProperty, Value = StackOrientation.Horizontal}
            }
        };

        public static Style AssistantBorderStackLayout { get; set; } = new Style(typeof(StackLayout))
        {
            Setters =
            {
                new Setter() {Property = StackLayout.OrientationProperty, Value = StackOrientation.Horizontal},
                new Setter() {Property = VisualElement.BackgroundColorProperty, Value = Color.Snow},
                new Setter() {Property = Layout.PaddingProperty, Value = new Thickness(1)}
            }
        };
        public static Style AssistantCellLabel { get; set; } = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter(){Property = Label.TextColorProperty, Value = Color.Snow},
                new Setter(){Property = View.HorizontalOptionsProperty, Value = LayoutOptions.Start},
                new Setter(){Property = Label.FontSizeProperty, Value = 22},
            }
        };
        public static Style AssistantCellImage { get; set; } = new Style(typeof(Image))
        {
            Setters =
            {
                new Setter() {Property = View.HorizontalOptionsProperty, Value = LayoutOptions.EndAndExpand}
            }
        };
        public static Style AssistantCellFrame { get; set; } = new Style(typeof(Frame))
        {
            Setters =
            {
                new Setter() {Property = VisualElement.BackgroundColorProperty, Value = Color.FromRgb(20,20,64)},
                new Setter() {Property = Frame.CornerRadiusProperty, Value = 0},
                new Setter() {Property = View.HorizontalOptionsProperty, Value = LayoutOptions.FillAndExpand},
                //new Setter() {Property = Frame.OutlineColorProperty, Value = Color.Snow},
            }
        };
    }
}