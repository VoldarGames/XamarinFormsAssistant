using System.Collections.Generic;
using XamarinFormsAssistant.Prototype.Model;

namespace XamarinFormsAssistant.Mocks
{
    public static class MockColorVehiculo
    {
        public static List<ColorVehiculo> ColorList { get; set; } = new List<ColorVehiculo>()
        {
            new ColorVehiculo() {Id = 1, Descripcion = "Rojo"},
            new ColorVehiculo() {Id = 2, Descripcion = "Verde"},
            new ColorVehiculo() {Id = 3, Descripcion = "Azul"},
            new ColorVehiculo() {Id = 4, Descripcion = "Amarillo"},
            new ColorVehiculo() {Id = 5, Descripcion = "Morado"},
            new ColorVehiculo() {Id = 6, Descripcion = "Rosa"},
            new ColorVehiculo() {Id = 7, Descripcion = "Marrón"},
            new ColorVehiculo() {Id = 8, Descripcion = "Negro"},
            new ColorVehiculo() {Id = 9, Descripcion = "Blanco"},
            new ColorVehiculo() {Id = 10, Descripcion = "Naranja"},

        };
        
    }
}