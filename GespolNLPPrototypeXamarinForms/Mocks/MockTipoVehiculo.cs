using System.Collections.Generic;
using XamarinFormsAssistant.Prototype.Model;

namespace XamarinFormsAssistant.Mocks
{
    public static class MockTipoVehiculo
    {
        public static List<TipoVehiculo> TipoVehiculosList { get; set; } = new List<TipoVehiculo>()
        {
            new TipoVehiculo() {Id = 1, Descripcion = "Turismo"},
            new TipoVehiculo() {Id = 2, Descripcion = "Camioneta"},
            new TipoVehiculo() {Id = 3, Descripcion = "Camión"},
            new TipoVehiculo() {Id = 4, Descripcion = "Motocicleta"},
            new TipoVehiculo() {Id = 5, Descripcion = "Deportivo"},
            new TipoVehiculo() {Id = 6, Descripcion = "Ambuláncia"},
            new TipoVehiculo() {Id = 7, Descripcion = "Barco"},
            new TipoVehiculo() {Id = 8, Descripcion = "Bicicleta"},

        };
        
    }
}