using System.ComponentModel;

namespace XamarinFormsAssistant.Prototype.Model
{
    public class ColorVehiculo : EntityBase
    {
        private string _descripcion;

        public string Codi { get; set; }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; OnPropertyChanged(new PropertyChangedEventArgs("Descripcion")); }
        }

        public override string ToString()
        {
            return Descripcion;
        }
    }
}