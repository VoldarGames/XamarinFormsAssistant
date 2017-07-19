using Xamarin.Forms;
using XamarinFormsAssistant.Assistant.Global.Styles;

namespace XamarinFormsAssistant.Assistant.Views.Components
{
    public class AssistantCell : StackLayout
    {
        public StackLayout MainStack { get; set; } = new StackLayout() { Style = AssistantStyles.AssistantBorderStackLayout };
        public Frame MainFrame { get; set; } = new Frame() {Style = AssistantStyles.AssistantCellFrame};
        public StackLayout SecondaryStack { get; set; } = new StackLayout() {Style = AssistantStyles.AssistantCellStackLayout};
        public Label MainLabel { get; set; } = new Label() {Style = AssistantStyles.AssistantCellLabel};
        public Image StatusImage { get; set; } = new Image() { Style = AssistantStyles.AssistantCellImage};
        public string OldValue { get; set; } = string.Empty; 

        public AssistantCell()
        {
            SecondaryStack.Children.Add(MainLabel);
            SecondaryStack.Children.Add(StatusImage);
            MainFrame.Content = SecondaryStack;
            MainStack.Children.Add(MainFrame);
            Children.Add(MainStack);
        }
    }
}