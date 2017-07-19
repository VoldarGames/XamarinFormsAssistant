using System;
using Xamarin.Forms;
using XamarinFormsAssistant.Assistant.Views.Components;

namespace XamarinFormsAssistant.Assistant.Animations
{
    public class BackgroundColorAssistantAnimation : AssistantAnimationBase<AssistantCube>{
        public override void AnimateOn(Page page, string animationName, AssistantCube control = default(AssistantCube))
        {
        page.Animate(animationName,
            new Animation(d => page.BackgroundColor = Color.FromRgb(0, 0, Math.Abs(Math.Cos(d * (Math.PI / 180))) / 1.5D)
            , 45D, 135D), 16U, 2000U, null, null, () => true);
        }

        public override void AnimateOff(Page page, string animationName, AssistantCube control = default(AssistantCube))
        {
            page.AbortAnimation(animationName);
            page.BackgroundColor = Color.FromRgb(0, 0, 32);
        }
    }
}