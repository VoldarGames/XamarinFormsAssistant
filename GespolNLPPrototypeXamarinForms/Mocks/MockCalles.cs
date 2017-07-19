using System.Collections.Generic;
using XamarinFormsAssistant.Prototype.Model;

namespace XamarinFormsAssistant.Mocks
{
    public static class MockCalles
    {
        public static List<Calle> CalleList { get; set; } = new List<Calle>()
        {
            new Calle() {Id = 1, Descripcion = "Fray Juniper Serra"},
            new Calle() {Id = 2, Descripcion = "Paseo de Gracia"},
            new Calle() {Id = 3, Descripcion = "Gran de gracia"},
            new Calle() {Id = 4, Descripcion = "Mayor"},
            new Calle() {Id = 5, Descripcion = "Principal"},
            new Calle() {Id = 6, Descripcion = "Cadí"},
            new Calle() {Id = 7, Descripcion = "Teide"},
            new Calle() {Id = 8, Descripcion = "Travau"},
            new Calle() {Id = 9, Descripcion = "Paseo de la Peira"},
            new Calle() {Id = 10, Descripcion = "Vilapicina"},

        };
        
    }
}