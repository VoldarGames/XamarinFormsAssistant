using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using XamarinFormsAssistant.Assistant.Attributes;
using XamarinFormsAssistant.Assistant.SpeechNames.DenunciaTrafico;
using XamarinFormsAssistant.Prototype.Model;

namespace XamarinFormsAssistant.Assistant.SpeechNames.Manager
{
    public static class AssistantSpeechNamesManager
    {
        public static Dictionary<Type,Dictionary<string, AssistantSpeechNames>> SpeechNamesDictionary { get; set; } =
            new Dictionary<Type, Dictionary<string, AssistantSpeechNames>>()
            {
                {typeof(Prototype.Model.DenunciaTrafico),
                    new Dictionary<string, AssistantSpeechNames>()
                    {
                        {nameof(Prototype.Model.DenunciaTrafico.Precepto), new DenunciaTraficoPreceptoSpeechNames()},
                        {nameof(Prototype.Model.DenunciaTrafico.FetDenunciat), new DenunciaTraficoFetDenunciatSpeechNames()},
                    }
                },
                { typeof(VehiculoDto),
                    new Dictionary<string, AssistantSpeechNames>()
                    {
                        {nameof(VehiculoDto.Modelo), new VehiculoDtoModeloSpeechNames()},
                        {nameof(VehiculoDto.TipoVehiculo), new VehiculoDtoTipoVehiculoSpeechNames()},
                        {nameof(VehiculoDto.Marca), new VehiculoDtoMarcaSpeechNames()},
                        {nameof(VehiculoDto.Color), new VehiculoDtoColorSpeechNames()},
                        {nameof(VehiculoDto.Matricula), new VehiculoDtoMatriculaSpeechNames()},
                    }
                },
                { typeof(Persona),
                    new Dictionary<string, AssistantSpeechNames>()
                    {
                        {nameof(Persona.Nom), new PersonaNomSpeechNames()},
                        {nameof(Persona.Cognoms), new PersonaCognomSpeechNames()},
                        {nameof(Persona.DNI), new PersonaDniSpeechNames()},
                    }
                },
                { typeof(Ubicacion),
                    new Dictionary<string, AssistantSpeechNames>()
                    {
                        {nameof(Ubicacion.Numero), new UbicacionNumeroSpeechNames()},
                        {nameof(Ubicacion.Calle), new UbicacionCalleSpeechNames()},
                    }
                },
            };

        public static void InitializeSpeechNames()
        {
            foreach (var keyValuePair in SpeechNamesDictionary)
            {
                foreach (var assistantSpeechNames in keyValuePair.Value)
                {
                    assistantSpeechNames.Value.PopulateSpeechNames();   
                }
            }
        }
        //public static bool ExistsSpeechName(Type currentContextType, string propertyName, string targetToFind)
        //{
        //    return SpeechNamesDictionary[currentContextType].ContainsKey(propertyName) &&
        //           SpeechNamesDictionary[currentContextType][propertyName].PropertySpeechNames.FirstOrDefault(
        //               s => s.Equals(targetToFind)) != null;
        //}

        public static bool ExistsSpeechName(Type currentContextType, string targetToFind)
        {
            foreach (var keyValuePair in SpeechNamesDictionary)
            {
                foreach (var kvp in keyValuePair.Value)
                {
                    if (kvp.Value.PropertySpeechNames.Contains(targetToFind))
                    {
                        var exists = false;
                        ExistsPropertyOnContext(currentContextType, kvp.Key, ref exists);
                        if(exists) return true;
                    }
                }
            }
            return false;
        }

        private static bool ExistsPropertyOnContext(Type contextType, string propertyName, ref bool exists)
        {
            foreach (var propertyInfo in contextType.GetProperties().Where(info => info.GetCustomAttribute<AssistantRequiredFieldAttribute>() != null))
            {
                if (!propertyInfo.Name.Equals(propertyName)) continue;
                exists = true;
                return true;
            }

            var navigationFields =
                contextType.GetProperties()
                    .Where(info => info.GetCustomAttribute<AssistantRequiredNavigationField>() != null);
            foreach (var navigationField in navigationFields)
            {
                exists |= ExistsPropertyOnContext(navigationField.PropertyType,propertyName,ref exists);
            }
            return false;
        }

        //public static string GetPropertyHeader(Type currentContextType, string propertyName)
        //{
        //    if(currentContextType == null || string.IsNullOrEmpty(propertyName)) return string.Empty;
        //    return !SpeechNamesDictionary[currentContextType].ContainsKey(propertyName) ?
        //        string.Empty 
        //        :
        //        SpeechNamesDictionary[currentContextType][propertyName].PropertySpeechNames.FirstOrDefault();
        //}

        public static string GetPropertyHeader(string propertyInfoName)
        {
            if (string.IsNullOrEmpty(propertyInfoName)) return string.Empty;
            foreach (var keyValuePair in SpeechNamesDictionary)
            {
                foreach (var kvp in keyValuePair.Value)
                {
                    if (kvp.Key.ToUpper().Equals(propertyInfoName.ToUpper()))
                    {
                        return kvp.Value.PropertySpeechNames.FirstOrDefault();
                    }
                }
            }
            return string.Empty;
        }

        public static string GetKey(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName)) return string.Empty;
            foreach (var keyValuePair in SpeechNamesDictionary)
            {
                foreach (var kvp in keyValuePair.Value)
                {
                    if (kvp.Value.PropertySpeechNames.Contains(propertyName.ToUpper()))
                    {
                        return kvp.Key;
                    }
                }
            }
            return string.Empty;
        }
    }

    
}