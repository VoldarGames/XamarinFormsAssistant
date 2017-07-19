using System.Collections.Generic;
using XamarinFormsAssistant.Prototype.Model;

namespace XamarinFormsAssistant.Mocks
{
    public static class MockMarca
    {
        public static List<Marca> MarcasList { get; set; } = new List<Marca>()
        {
            new Marca() {Id = 1, Descripcion = "Opel"},
            new Marca() {Id = 2, Descripcion = "Seat"},
            new Marca() {Id = 3, Descripcion = "BMW"},
            new Marca() {Id = 4, Descripcion = "Fiat"},
            new Marca() {Id = 5, Descripcion = "Alfa Romeo"},
            new Marca() {Id = 6, Descripcion = "Ferrari"},
            new Marca() {Id = 7, Descripcion = "Audi"},
            new Marca() {Id = 8, Descripcion = "Nissan"},
            new Marca() {Id = 9, Descripcion = "Renault"},
            new Marca() {Id = 10, Descripcion = "Peugeot"},

        };
        
    }
}