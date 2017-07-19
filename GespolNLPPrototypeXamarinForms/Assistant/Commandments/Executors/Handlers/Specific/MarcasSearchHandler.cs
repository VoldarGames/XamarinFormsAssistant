using System.Collections.Generic;
using XamarinFormsAssistant.Assistant.Extensions;
using XamarinFormsAssistant.Assistant.Fragments;
using XamarinFormsAssistant.Mocks;
using XamarinFormsAssistant.Prototype.Model;

namespace XamarinFormsAssistant.Assistant.Commandments.Executors.Handlers.Specific
{
    public class MarcasSearchHandler : SearchHandler<Marca>
    {
        public override IList<Marca> Search(List<SpeechParameter> additionalParameters)
        {
            return LevenshteinWrapper.GetLevenstheinContainsList(additionalParameters.ComposePhrase(), MockMarca.MarcasList, 0.35f);
        }
    }
}