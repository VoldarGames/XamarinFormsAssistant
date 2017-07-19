using System;
using System.Collections.Generic;
using XamarinFormsAssistant.Assistant.Commandments.Executors.Handlers;
using XamarinFormsAssistant.Assistant.Commandments.Executors.Handlers.Specific;
using XamarinFormsAssistant.Prototype.Model;

namespace XamarinFormsAssistant.Assistant.Commandments.Executors.Manager
{
    public static class SearchHandlerManager
    {
        private static readonly Dictionary<Type,ISearchHandler> SearchHandlerDictionary = new Dictionary<Type, ISearchHandler>()
        {
            {typeof(PreceptoDto),new PreceptoDtoSearchHandler()},
            {typeof(Modelo),new ModeloSearchHandler()},
            {typeof(TipoVehiculo),new TipoVehiculoSearchHandler() },
            {typeof(Marca),new MarcasSearchHandler() },
            {typeof(ColorVehiculo),new ColorVehiculoSearchHandler() },
            {typeof(Calle),new CallesSearchHandler() },
           

        };

        public static SearchHandler<T> GetHandler<T>(Type propertyType) where T : EntityBase
        {
            return (SearchHandler<T>) SearchHandlerDictionary[propertyType];
        }
    }
}