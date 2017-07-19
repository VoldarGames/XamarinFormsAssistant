using XamarinFormsAssistant.Assistant.Fragments.Managers;

namespace XamarinFormsAssistant.Assistant.Fragments.Converters
{
    public static class SpeechConverter
    {
        public static SpeechFragment Convert(string s)
        {
            var command = CommandDictionaryManager.FindCommand(s);
            if(command != null) return command;
            return new SpeechParameter() {OriginalTextFragment = s};
        }
    }
}