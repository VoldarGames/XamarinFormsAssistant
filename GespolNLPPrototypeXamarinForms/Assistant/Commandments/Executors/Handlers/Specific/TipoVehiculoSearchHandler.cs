using System.Collections.Generic;
using XamarinFormsAssistant.Assistant.Extensions;
using XamarinFormsAssistant.Assistant.Fragments;
using XamarinFormsAssistant.Mocks;
using XamarinFormsAssistant.Prototype.Model;

namespace XamarinFormsAssistant.Assistant.Commandments.Executors.Handlers.Specific
{
    public class TipoVehiculoSearchHandler : SearchHandler<TipoVehiculo>
    {
        public override IList<TipoVehiculo> Search(List<SpeechParameter> additionalParameters)
        {
            return LevenshteinWrapper.GetLevenstheinContainsList(additionalParameters.ComposePhrase(), MockTipoVehiculo.TipoVehiculosList, 0.35f);
        }
    }
}