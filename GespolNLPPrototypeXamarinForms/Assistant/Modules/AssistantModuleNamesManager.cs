using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Plugin.TextToSpeech;
using XamarinFormsAssistant.Assistant.Modules.Manager;
using XamarinFormsAssistant.Prototype.Model;

namespace XamarinFormsAssistant.Assistant.Modules
{
    public static class AssistantModuleNamesManager
    {
        public static Dictionary<Type, IAssistantModuleNames> ModuleNamesDictionary { get; set; } =
            new Dictionary<Type, IAssistantModuleNames>()
            {
                {typeof(DenunciaTrafico),new DenunciaTraficoAssistantModuleNames()}
            };

        public static void InitializeModuleNames()
        {
            foreach (var keyValuePair in ModuleNamesDictionary)
            {
                keyValuePair.Value.PopulateModuleNames();
            }
        }

        public static Type GetModuleEntityType(string module)
        {
            foreach (var keyValuePair in ModuleNamesDictionary)
            {
                if (keyValuePair.Value.PropertyModuleNames.FirstOrDefault(s => s.Equals(module)) != null)
                    return keyValuePair.Key;
            }
            CrossTextToSpeech.Current.Speak("Módulo no encontrado.");
            return null;
        }

        public static ObservableCollection<object> GetModuleNames()
        {
            var result = new ObservableCollection<object>();
            foreach (var keyValuePair in ModuleNamesDictionary)
            {
                    // ReSharper disable once AssignNullToNotNullAttribute
                    result.Add(keyValuePair.Value?.PropertyModuleNames?.FirstOrDefault());
            }
            return result;
        }
    }
}
    
