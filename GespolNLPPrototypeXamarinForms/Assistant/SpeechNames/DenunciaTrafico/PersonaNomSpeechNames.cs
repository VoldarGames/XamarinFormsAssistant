namespace XamarinFormsAssistant.Assistant.SpeechNames.DenunciaTrafico
{
    public class PersonaNomSpeechNames : AssistantSpeechNames
    {
        public override void PopulateSpeechNames()
        {
            //TODO: Feed de SQlite
            PropertySpeechNames.Add("NOMBRE");
            PropertySpeechNames.Add("NOM");
        }
    }
}