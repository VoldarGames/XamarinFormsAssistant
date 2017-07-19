using System.Collections.Generic;
using XamarinFormsAssistant.Assistant.Extensions;
using XamarinFormsAssistant.Assistant.Fragments;
using XamarinFormsAssistant.Mocks;
using XamarinFormsAssistant.Prototype.Model;

namespace XamarinFormsAssistant.Assistant.Commandments.Executors.Handlers.Specific
{
    public class PreceptoDtoSearchHandler : SearchHandler<PreceptoDto>
    {
        public override IList<PreceptoDto> Search(List<SpeechParameter> additionalParameters)
        {
            //return MockPreceptos.PreceptoList.FindAll(pruebas => pruebas.Descripcion.ToUpper()
            //    .Contains(additionalParameters.FirstOrDefault().OriginalTextFragment.ToUpper())).ToList();
            
            return LevenshteinWrapper.GetLevenstheinContainsList(additionalParameters.ComposePhrase(), MockPreceptos.PreceptoList, 0.35f);
        }
    }
}