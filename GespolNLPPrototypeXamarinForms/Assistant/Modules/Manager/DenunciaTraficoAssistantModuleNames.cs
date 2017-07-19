using System.Collections.Generic;

namespace XamarinFormsAssistant.Assistant.Modules.Manager
{
    public class DenunciaTraficoAssistantModuleNames : IAssistantModuleNames {
        public IList<string> PropertyModuleNames { get; set; } = new List<string>();
        public void PopulateModuleNames()
        {
            PropertyModuleNames.Add("DENUNCIA");
            PropertyModuleNames.Add("RENUNCIA");
        }
    }
}