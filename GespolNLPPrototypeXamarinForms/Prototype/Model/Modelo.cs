using System.ComponentModel;

namespace XamarinFormsAssistant.Prototype.Model
{
    public class Modelo : EntityBase
    {
        private string _descripcion;

        public string Codi { get; set; }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; OnPropertyChanged(new PropertyChangedEventArgs("Descripion")); }
        }


        public Marca Marca { get; set; }

        public int MarcaId { get; set; }
        public override string ToString()
        {
            return Descripcion;
        }
    }
}