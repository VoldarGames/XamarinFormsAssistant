using System.ComponentModel;

namespace XamarinFormsAssistant.Prototype.Model
{
    public class TipoVehiculo : EntityBase
    {
        public string Codi { get; set; }
        private string _descripcion { get; set; }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; OnPropertyChanged(new PropertyChangedEventArgs("Descripcion")); }
        }
        private string _pathFotoPlantilla;

        public string PathFotoPlantilla
        {
            get
            {
                return _pathFotoPlantilla;
            }
            set
            {
                _pathFotoPlantilla = value;
                OnPropertyChanged(new PropertyChangedEventArgs("PathFotoPlantilla"));
            }
        }
        public override string ToString()
        {
            return Descripcion;
        }
    }
}