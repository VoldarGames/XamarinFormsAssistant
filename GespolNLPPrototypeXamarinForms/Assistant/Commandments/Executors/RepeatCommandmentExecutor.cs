namespace XamarinFormsAssistant.Assistant.Commandments.Executors
{
    public class RepeatCommandmentExecutor : CommandmentExecutorBase
    {
        public override void Execute(SpeechCommandment speechCommandment)
        {
            Assistant.GetInstance().CurrentWaitingResponseType =
                Assistant.GetInstance().CurrentWaitingResponseType;
        }
    }
}