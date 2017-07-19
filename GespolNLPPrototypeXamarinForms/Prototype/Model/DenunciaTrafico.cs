using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using XamarinFormsAssistant.Assistant.Attributes;

namespace XamarinFormsAssistant.Prototype.Model
{
    public class DenunciaTrafico : DenunciaTraficoDto
    {
        [AssistantRequiredNavigationField]
        public VehiculoDto Vehiculo { get; set; }
        public int VehiculoDtoId { get; set; }
        public StateDenunciaTrafico Estado { get; set; }
        public int StateDenunciaTraficoId { get; set; }

        public bool IsNew { get; set; }


        private string _imagenEstado;


        #region TabInfractor


        private string _causaNoNotificacionTxt;
        public string CausaNoNotificacionTxt
        {
            get
            {
                return _causaNoNotificacionTxt;
            }
            set
            {
                _causaNoNotificacionTxt = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CausaNoNotificacionTxt"));
            }
        }

        public string InfractorTab;

        [AssistantRequiredNavigationField]
        public Persona Infractor { get; set; }


        public int InfractorPersonaId { get; set; }

        #endregion

        #region TabVehiculo
        public bool Notified { get; set; }
        public DateTime FechaAviso { get; set; }


        #endregion

        #region TabLugar

        [AssistantRequiredNavigationField]
        public Ubicacion Ubicacion { get; set; }

        public int UbicacionId { get; set; }



        #endregion


        #region Infraccion

        public string Infraccion;

        public string PreceptoTxt { get; set; }
        #endregion

        #region Fotos
        public string Fotos;


        #endregion

        #region Resumen
        public string Resumen;
        #endregion

        public string GetListText()
        {



            string vehiculoMatricula;
            string infractorDni;

            if (string.IsNullOrEmpty(Vehiculo?.Matricula))
            {
                vehiculoMatricula = "No introducida.";
            }
            else vehiculoMatricula = Vehiculo.Matricula;

            if (string.IsNullOrEmpty(Infractor?.DNI))
            {
                infractorDni = "No introducido.";
            }
            else
            {
                infractorDni = Infractor.DNI;
            }

            return $"Fecha: {DataHoraDenuncia.ToString().PadRight(12)}{System.Environment.NewLine}" +
                   $"Matrícula: {vehiculoMatricula.PadRight(12)}{System.Environment.NewLine}" +
                   $"DNI: {infractorDni.PadRight(12)}{System.Environment.NewLine}";



        }


        public string GetText
            => GetListText();

        public DenunciaTrafico()
        {
            Estado = new StateDenunciaTrafico();
            Vehiculo = new VehiculoDto();
            Infractor = new Persona();

            Ubicacion = new Ubicacion()
            {
                Calle = new Calle()
            };
            ListaFotos = new ObservableCollection<int>();
            DataHoraDenuncia = DateTime.Now;
        }

        public DenunciaTrafico(StatesDenunciaTrafico estadoDenuncia)
        {
            Estado = new StateDenunciaTrafico(estadoDenuncia);
            Vehiculo = new VehiculoDto();
            Infractor = new Persona();
            Ubicacion = new Ubicacion()
            {
                Calle = new Calle()
            };
            ListaFotos = new ObservableCollection<int>();
            DataHoraDenuncia = DateTime.Now;
        }




        public string ImagenEstado
        {
            get
            {
                return _imagenEstado;
            }
            set
            {
                _imagenEstado = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ImagenEstado"));
            }
        }


    }
}