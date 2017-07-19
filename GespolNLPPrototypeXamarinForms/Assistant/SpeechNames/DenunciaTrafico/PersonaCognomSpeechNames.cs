namespace XamarinFormsAssistant.Assistant.SpeechNames.DenunciaTrafico
{
    public class PersonaCognomSpeechNames : AssistantSpeechNames
    {
        public override void PopulateSpeechNames()
        {
            //TODO: Feed de SQlite
            PropertySpeechNames.Add("APELLIDO");
            PropertySpeechNames.Add("APELLIDOS");
            PropertySpeechNames.Add("COGNOM");
            PropertySpeechNames.Add("COGNOMS");
        }
    }
}