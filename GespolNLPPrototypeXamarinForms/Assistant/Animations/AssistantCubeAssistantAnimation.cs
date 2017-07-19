using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsAssistant.Assistant.Views.Components;

namespace XamarinFormsAssistant.Assistant.Animations
{
    public class AssistantCubeAssistantAnimation : AssistantAnimationBase<AssistantCube>{
        public override void AnimateOn(Page page, string animationName, AssistantCube control = default(AssistantCube))
        {
            page.Animate(animationName, new Animation(async d =>
            {
                for (var index = 0; index < control?.Buttons.Count; index++)
                {
                    await Task.Delay(index * 50);
                    var button = control.Buttons[index];
                    button.Scale = (d > 1) ? (2 - d) : 1 - d;
                }
            }, 0D, 2D), 16U, 2250U, Easing.CubicIn, null, () => true);
        }

        public override void AnimateOff(Page page, string animationName, AssistantCube control = default(AssistantCube))
        {
            page.AbortAnimation(animationName);
        }
    }
}