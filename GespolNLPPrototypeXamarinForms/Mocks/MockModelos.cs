using System.Collections.Generic;
using XamarinFormsAssistant.Prototype.Model;

namespace XamarinFormsAssistant.Mocks
{
    public static class MockModelos
    {
        public static List<Modelo> ModelosList { get; set; } = new List<Modelo>()
        {
            new Modelo() {Id = 1, Descripcion = "León"},
            new Modelo() {Id = 2, Descripcion = "Sandero"},
            new Modelo() {Id = 3, Descripcion = "Astra"},
            new Modelo() {Id = 4, Descripcion = "X6"},
            new Modelo() {Id = 5, Descripcion = "Z4"},
            new Modelo() {Id = 6, Descripcion = "Serie 1"},
            new Modelo() {Id = 7, Descripcion = "Serie 2"},
            new Modelo() {Id = 8, Descripcion = "Camaro"},

        };
        
    }
}