using System.Collections.Generic;
using XamarinFormsAssistant.Assistant.Extensions;
using XamarinFormsAssistant.Assistant.Fragments;
using XamarinFormsAssistant.Mocks;
using XamarinFormsAssistant.Prototype.Model;

namespace XamarinFormsAssistant.Assistant.Commandments.Executors.Handlers.Specific
{
    public class ModeloSearchHandler : SearchHandler<Modelo>
    {
        public override IList<Modelo> Search(List<SpeechParameter> additionalParameters)
        {
            return LevenshteinWrapper.GetLevenstheinContainsList(additionalParameters.ComposePhrase(), MockModelos.ModelosList, 0.35f);
        }
    }
}