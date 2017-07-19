using System.Collections.Generic;
using XamarinFormsAssistant.Assistant.Extensions;
using XamarinFormsAssistant.Assistant.Fragments;
using XamarinFormsAssistant.Mocks;
using XamarinFormsAssistant.Prototype.Model;

namespace XamarinFormsAssistant.Assistant.Commandments.Executors.Handlers.Specific
{
    public class CallesSearchHandler : SearchHandler<Calle>
    {
        public override IList<Calle> Search(List<SpeechParameter> additionalParameters)
        {
            return LevenshteinWrapper.GetLevenstheinContainsList(additionalParameters.ComposePhrase(), MockCalles.CalleList, 0.35f);
        }
    }
}