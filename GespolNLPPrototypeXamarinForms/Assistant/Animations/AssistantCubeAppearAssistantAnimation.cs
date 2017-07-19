using Xamarin.Forms;
using XamarinFormsAssistant.Assistant.Views.Components;

namespace XamarinFormsAssistant.Assistant.Animations
{
    public class AssistantCubeAppearAssistantAnimation : AssistantAnimationBase<AssistantCube>{
        public override void AnimateOn(Page page, string animationName, AssistantCube control = default(AssistantCube))
        {
            if (control != null) control.IsVisible = true;
            page.Animate(animationName, new Animation(d =>
            {
                if (control != null) control.Scale = d;
            }));
        }

        public override void AnimateOff(Page page, string animationName, AssistantCube control = default(AssistantCube))
        {
            page.Animate(animationName, new Animation(d =>
            {
                if (control != null) control.Scale = 1 - d;
            }), 16U, 250U, null, (d, b) =>
            {
                if (!b) return;
                if (control != null) control.IsVisible = false;
            });
        }
    }
}