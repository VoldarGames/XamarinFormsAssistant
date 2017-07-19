using System.Collections.Generic;

namespace XamarinFormsAssistant.Assistant.SpeechNames
{
    public abstract class AssistantSpeechNames
    {
        public IList<string> PropertySpeechNames { get; set; } = new List<string>();
        public abstract void PopulateSpeechNames();
    }
}