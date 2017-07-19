using XamarinFormsAssistant.Assistant.Animations;

namespace XamarinFormsAssistant.Assistant.Global.Animations
{
    public static class AssistantAnimations
    {
        public static BackgroundColorAssistantAnimation BackgroundColor { get; set; } = new BackgroundColorAssistantAnimation();
        public static AssistantCubeAssistantAnimation AssistantCube { get; set; } = new AssistantCubeAssistantAnimation();
        public static AssistantCubeAppearAssistantAnimation AssistantCubeAppear { get; set; } = new AssistantCubeAppearAssistantAnimation();
    }
}