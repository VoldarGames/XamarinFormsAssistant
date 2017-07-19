using System;
using System.ComponentModel;
using XamarinFormsAssistant.Assistant.Attributes;

namespace XamarinFormsAssistant.Prototype.Model
{
    public class Ubicacion : EntityBase
    {
        public const string PropNumero = "Numero";
        public const string PropDireccion = "Direccion";
        public const string PropCalle = "Calle";
        public const string PropLocalizacion = "Localizacion";

        private Calle _calle;
        private string _numero;
        private string _localización;

        public Ubicacion()
        {
            Calle = new Calle();
            Coordenada = new Coordenada();
        }

        [AssistantRequiredField]
        [AssistantRawStringField]
        public string Numero
        {
            get { return _numero; }
            set
            {
                _numero = value;
                OnPropertyChanged(new PropertyChangedEventArgs(PropNumero));
                OnPropertyChanged(new PropertyChangedEventArgs(PropDireccion));
                OnPropertyChanged(new PropertyChangedEventArgs(PropLocalizacion));
            }
        }

        [AssistantRequiredField]
        [AssistantSearchField]
        public Calle Calle
        {
            get { return _calle; }
            set
            {
                _calle = value;
                OnPropertyChanged(new PropertyChangedEventArgs(PropCalle));
                OnPropertyChanged(new PropertyChangedEventArgs(PropDireccion));
                OnPropertyChanged(new PropertyChangedEventArgs(PropLocalizacion));
            }
        }

        public int CalleId { get; set; }

        public int CoordenadaId { get; set; }

        public Coordenada Coordenada { get; set; }

        public string Localizacion
        {
            get { return _localización; }
            set
            {
                _localización = value;
                OnPropertyChanged(new PropertyChangedEventArgs(PropNumero));
                OnPropertyChanged(new PropertyChangedEventArgs(PropDireccion));
                OnPropertyChanged(new PropertyChangedEventArgs(PropLocalizacion));
            }
        }

        public string Direccion
        {
            get
            {
                if (!String.IsNullOrEmpty(Calle?.Descripcion))
                {
                    var retorno = $"{Calle.Descripcion} , {Numero}";
                    if (!string.IsNullOrEmpty(Localizacion))
                    {
                        retorno = $"{retorno} - {Localizacion}";
                    }
                    return retorno;
                }
                else
                {
                    return $"{Localizacion}";
                }
            }
        }
    }
}