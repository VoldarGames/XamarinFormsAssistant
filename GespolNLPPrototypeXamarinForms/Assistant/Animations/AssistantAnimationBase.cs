using Xamarin.Forms;

namespace XamarinFormsAssistant.Assistant.Animations
{
    public abstract class AssistantAnimationBase<TView> where TView : class
    {
        public virtual string OnName => $"{GetType().Name}On";
        public virtual string OffName => $"{GetType().Name}Off";
        public abstract void AnimateOn(Page page, string animationName, TView control = default(TView));
        public abstract void AnimateOff(Page page, string animataionName, TView control = default(TView));
    }
}