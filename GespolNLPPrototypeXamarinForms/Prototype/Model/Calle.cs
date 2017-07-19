using System;
using System.ComponentModel;

namespace XamarinFormsAssistant.Prototype.Model
{
    public class Calle : EntityBase
    {
        private string _descripcion;
        private PoblacioDto _poblacion;
        public const String PropDescripcion = "Descripcion";


        //public string Id { get; set; }
        public string Codi { get; set; }

        public Calle()
        {

        }
        public string Descripcion
        {
            get { return _descripcion; }
            set
            {
                _descripcion = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Descripcion"));
            }
        }

        public PoblacioDto Poblacio
        {
            get { return _poblacion; }
            set
            {
                _poblacion = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Poblacio)));
                /*OnPropertyChanged(new PropertyChangedEventArgs(nameof(PoblacionDescripcion)));*/
            }
        }

        public int? PoblacionId { get; set; }

        private string _poblacionDescripcion;

        //public string PoblacionDescripcion => Poblacio?.Descripcio;
        //}

        public string PoblacionDescripcion
        {
            get { return _poblacionDescripcion; }
            set
            {
                _poblacionDescripcion = value;
                if (Poblacio != null)
                {
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(PoblacionDescripcion)));
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Poblacio)));
                    Poblacio.Descripcio = _poblacionDescripcion;
                }

                //Poblacio.Descripcio = _poblacionDescripcion;

            }
            //public string PoblacionDescripcion => _poblacion?.Descripcio;
        }

        public override string ToString()
        {
            return Descripcion;
        }
    }
}