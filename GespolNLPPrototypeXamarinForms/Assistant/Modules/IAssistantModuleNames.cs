using System.Collections.Generic;

namespace XamarinFormsAssistant.Assistant.Modules
{
    public interface IAssistantModuleNames
    {
        IList<string> PropertyModuleNames { get; set; }
        void PopulateModuleNames();
    }
}