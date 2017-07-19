using System;
using Xamarin.Forms;
using XamarinFormsAssistant.Assistant.Converters;
using XamarinFormsAssistant.Assistant.Global.Styles;

namespace XamarinFormsAssistant.Assistant.Views.Components
{
    public class AnimatableViewCell : ViewCell
    {
        public bool DoAnimation { get; set; }
        public AssistantCell AssistantCell { get; set; }

        public AnimatableViewCell()
        {
            AssistantCell = new AssistantCell();
            AssistantCell.MainLabel.SetBinding(Label.TextProperty, ".", BindingMode.Default,new AnimatableToStringConverter(this));
            AssistantCell.StatusImage.SetBinding(Image.SourceProperty, ".", BindingMode.Default,new SourcePropertyConverter());
            View = AssistantCell;
        }
        protected override void OnAppearing()
        {
            if (DoAnimation)
            {
                AssistantCell.Animate("PropertyChanged", d =>
                {
                    AssistantCell.MainFrame.BackgroundColor = Color.FromRgb(Convert.ToInt32(d%64), Convert.ToInt32(d%64), 64);
                }, 0D, 5*64D, 16U, 3000U, Easing.Linear,
                    (d, b) =>
                    {
                        AssistantCell.MainFrame.Style = AssistantStyles.AssistantCellFrame;
                        
                    });
                DoAnimation = false;
            }
            base.OnAppearing();
        }
    }
}