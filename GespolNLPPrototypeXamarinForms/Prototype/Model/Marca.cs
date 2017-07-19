using System.ComponentModel;

namespace XamarinFormsAssistant.Prototype.Model
{
    public class Marca : EntityBase
    {
        private string _descripcion;

        public string Codi { get; set; }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; OnPropertyChanged(new PropertyChangedEventArgs("Descripion")); }
        }

        public override string ToString()
        {
            return Descripcion;
        }
    }
}