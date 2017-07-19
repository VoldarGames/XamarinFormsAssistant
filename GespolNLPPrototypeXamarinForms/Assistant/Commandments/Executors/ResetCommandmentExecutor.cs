namespace XamarinFormsAssistant.Assistant.Commandments.Executors
{
    public class ResetCommandmentExecutor : CommandmentExecutorBase
    {
        public override void Execute(SpeechCommandment speechCommandment)
        {
            Assistant.GetInstance().Reset();
        }
    }
}