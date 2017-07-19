using System;
using System.Collections.Generic;
using System.ComponentModel;
using XamarinFormsAssistant.Assistant.Attributes;

namespace XamarinFormsAssistant.Prototype.Model
{
    public class VehiculoDto : EntityBase
    {
        private Modelo _modelo;
        private ColorVehiculo _color;
        private Marca _marca;
        private TipoVehiculo _tipoVehiculo;
        private Persona _titular;
        private string _matricula;
        private string _infractorTitularTxt;

        public VehiculoDto()
        {
            TipoVehiculo = new TipoVehiculo();
            Color = new ColorVehiculo();
            Modelo = new Modelo();
            Marca = new Marca();
            Requisitorias = new List<int>();
            Titular = new Persona();
            Dgt = false;
            IsSeguroVigente = false;
            FechatItv = DateTime.MinValue;
            FechaMatriculacion = DateTime.MinValue;
            Domicilio = new DomiciliDto();
        }

        #region OtherEntitiesRelationship

        public int ServicioId { get; set; }
        #endregion

        [AssistantRequiredField]
        [AssistantSearchField]
        public Modelo Modelo
        {
            get { return _modelo; }
            set
            {
                _modelo = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Modelo)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ModeloVehiculo)));
            }
        }

        public int? ModeloId { get; set; }


        [AssistantRequiredField]
        [AssistantSearchField]
        public TipoVehiculo TipoVehiculo
        {
            get { return _tipoVehiculo; }
            set
            {
                _tipoVehiculo = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(TipoVehiculo)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(TipoVehiculoTxt)));
            }

        }

        public int? TipoVehiculoId { get; set; }

        [AssistantRequiredField]
        [AssistantSearchField]
        public Marca Marca
        {
            get { return _marca; }
            set
            {
                _marca = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Marca)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(MarcaVehiculo)));
            }

        }

        public int? MarcaId { get; set; }

        [AssistantRequiredField]
        [AssistantSearchField]
        public ColorVehiculo Color
        {
            get { return _color; }
            set
            {
                _color = value; OnPropertyChanged(new PropertyChangedEventArgs(nameof(Color)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ColorVehiculo)));
            }
        }

        public int? ColorVehiculoId { get; set; }

        [AssistantRequiredField]
        [AssistantPlateField]
        public string Matricula
        {
            get { return _matricula; }
            set
            {

                _matricula = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Matricula)));
            }
        }


        public List<int> Requisitorias { get; set; }

        public Persona Titular
        {
            get { return _titular; }
            set
            {
                _titular = value; OnPropertyChanged(new PropertyChangedEventArgs(nameof(Titular)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(TitularNombre)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(TitularCognoms)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(TitularDni)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(GetFormattedTitular)));
            }
        }


        public int? PersonaId { get; set; }
        public bool Dgt { get; set; }
        public bool IsSeguroVigente { get; set; }
        public DateTime FechatItv { get; set; }

        public int? SeguroDtoId { get; set; }
        public string Bastidor { get; set; }
        public DateTime FechaMatriculacion { get; set; }
        public int? Tara { get; set; }
        public int? MasaOrdenMarcha { get; set; }

        public DomiciliDto Domicilio { get; set; }
        public int? DomiciliDtoId { get; set; }

        public string InfractorTitularTxt
        {
            get { return _infractorTitularTxt; }
            set
            {
                _infractorTitularTxt = value; OnPropertyChanged(new PropertyChangedEventArgs(nameof(InfractorTitularTxt)));
                //OnPropertyChanged(new PropertyChangedEventArgs(nameof(TitularDni)));
                //OnPropertyChanged(new PropertyChangedEventArgs(nameof(TitularNombre)));
                //OnPropertyChanged(new PropertyChangedEventArgs(nameof(TitularCognoms)));
            }

        }
        public string TitularNombre => _titular?.Nom;
        public string TitularCognoms => _titular?.Cognoms;
        public string TitularDni => _titular?.DNI;

        private string _colorVehiculo;
        public string ColorVehiculo
        {
            get { return _colorVehiculo; }
            set
            {
                _colorVehiculo = value;
                if (Color != null)
                {
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(ColorVehiculo)));
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Color)));
                    Color.Descripcion = _colorVehiculo;
                }
            }
        }

        private string _modeloVehiculo;
        public string ModeloVehiculo
        {
            get { return _modeloVehiculo; }
            set
            {
                _modeloVehiculo = value;
                if (Modelo != null)
                {
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(ModeloVehiculo)));
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Modelo)));
                    Modelo.Descripcion = _modeloVehiculo;
                }
            }
        }

        private string _marcaVehiculo;
        public string MarcaVehiculo
        {
            get { return _marcaVehiculo; }
            set
            {
                _marcaVehiculo = value;
                if (Marca != null)
                {
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(MarcaVehiculo)));
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Marca)));
                    Marca.Descripcion = _marcaVehiculo;
                }
            }
        }

        private string _tipoVehiculoTxt;
        public string TipoVehiculoTxt
        {
            get { return _tipoVehiculoTxt; }
            set
            {
                _tipoVehiculoTxt = value;
                if (TipoVehiculo != null)
                {
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(TipoVehiculoTxt)));
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(TipoVehiculo)));
                    TipoVehiculo.Descripcion = _tipoVehiculoTxt;
                }
            }
        }

        public string GetFormattedTitular => string.Concat(TitularCognoms, ", ", TitularNombre);
    }
}