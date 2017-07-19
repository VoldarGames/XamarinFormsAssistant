using System.Collections.Generic;
using XamarinFormsAssistant.Assistant.Extensions;
using XamarinFormsAssistant.Assistant.Fragments;
using XamarinFormsAssistant.Mocks;
using XamarinFormsAssistant.Prototype.Model;

namespace XamarinFormsAssistant.Assistant.Commandments.Executors.Handlers.Specific
{
    public class ColorVehiculoSearchHandler : SearchHandler<ColorVehiculo>
    {
        public override IList<ColorVehiculo> Search(List<SpeechParameter> additionalParameters)
        {
            return LevenshteinWrapper.GetLevenstheinContainsList(additionalParameters.ComposePhrase(), MockColorVehiculo.ColorList, 0.35f);
        }
    }
}