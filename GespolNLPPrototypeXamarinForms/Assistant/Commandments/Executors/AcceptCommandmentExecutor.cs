namespace XamarinFormsAssistant.Assistant.Commandments.Executors
{
    public class AcceptCommandmentExecutor : CommandmentExecutorBase
    {
        public override void Execute(SpeechCommandment speechCommandment)
        {
            //Open detail window...with:
            //Assistant.GetInstance().CurrentInstanceResult;
        }

        //private string GetTextFromTextView(string propertyName)
        //{
        //    if (string.Equals(propertyName, "MATRICULA"))
        //    {
        //        return ((MainActivity)Assistant.GetInstance().CurrentContext).MatriculaTextView.Text;
        //    }
        //    else
        //    {
        //        return ((MainActivity)Assistant.GetInstance().CurrentContext).PreceptoTextView.Text;
        //    }
        //}
    }
}