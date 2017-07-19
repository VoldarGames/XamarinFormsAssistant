using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using XamarinFormsAssistant.Assistant.Attributes;

namespace XamarinFormsAssistant.Prototype.Model
{
    public class DenunciaTraficoDto : EntityBase
    {
        public string NombreAyuntamiento { get; set; }

        public string Emisora { get; set; }
        
        #region Simple Properties

        #region SystemProperties

        public int IdClient { get; set; }
        public Nullable<int> NumButlleta { get; set; }
        public string NumExpedient { get; set; }
        public DateTime Dia
        {
            get
            {
                return _dia;
            }
            set
            {
                _dia = value;
                DataHoraDenuncia = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Dia"));
            }
        }

        public DateTime Hora
        {
            get
            {
                return _hora;
            }
            set
            {
                _hora = value;
                DataHoraDenuncia = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Hora"));
            }
        }
        private DateTime _DataHoraDenuncia { get; set; }
        public DateTime DataHoraDenuncia
        {
            get { return _DataHoraDenuncia; }
            set
            {
                _DataHoraDenuncia = value;
                _hora = value;
                _dia = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DataHoraDenuncia"));
            }

        }



        public Nullable<int> IdAgentNotificador { get; set; }
        public int IdEstatExpedient { get; set; }
        public bool Anullada { get; set; }
        public Nullable<int> RefInformeECA { get; set; }
        public Nullable<System.DateTime> DataInformeECA { get; set; }
        public Nullable<bool> ECANoRequerit { get; set; }
        public Nullable<bool> ECAPositiu { get; set; }
        public int IdAgentDenunciant1 { get; set; }
        public Nullable<int> IdAgentDenunciant2 { get; set; }

        public System.DateTime DataEntradaExpedient { get; set; }
        public Nullable<System.DateTime> DataUltModificacio { get; set; }
        public Nullable<int> IdOrigenDenuncia { get; set; }
        public Nullable<int> NumRemesaTraspas { get; set; }
        public Nullable<System.DateTime> DataTraspas { get; set; }

        #endregion

        #region DenunciaTabProperties

        public string DescripcionNormativa { get; set; }
        private string _fetDenunciat { get; set; }

        [AssistantRequiredField]
        [AssistantRawStringPhraseField]
        public string FetDenunciat
        {
            get
            {
                return _fetDenunciat;
            }
            set
            {
                _fetDenunciat = value;
                OnPropertyChanged(new PropertyChangedEventArgs("FetDenunciat"));
            }
        }
        public Nullable<int> IdCausaNoNotificacio { get; set; }
        public Nullable<System.DateTime> DataAnullacio { get; set; }
        public Nullable<System.DateTime> DataTrasllatExecutiva { get; set; }
        public Nullable<System.DateTime> DataPagament { get; set; }
        public Nullable<decimal> ImportReduit { get; set; }
        public Nullable<decimal> ImportSenseReduir { get; set; }

        public string Observaciones { get; set; }

        #endregion

        #region LugarTabProperties
        public string NumKm { get; set; }
        public Nullable<int> IdCarrer { get; set; }
        public string Lloc { get; set; }
        public Nullable<int> IdTipusLloc { get; set; }
        public Nullable<int> IdSector { get; set; }
        public Nullable<int> IdDistricte { get; set; }

        #endregion

        #region InfractorTabProperties
        public Nullable<bool> Antecedents { get; set; }

        private string _enQualitatde;
        public string EnQualitatDe
        {
            get { return _enQualitatde; }
            set
            {
                _enQualitatde = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(EnQualitatDeResumen)));
            }
        }

        public string EnQualitatDeResumen
        {
            get
            {
                switch (EnQualitatDe)
                {
                    case "T":
                        return "Desconocido";
                    case "C":
                        return "Conductor";
                    case "O":
                        return "Ocupante";
                    default:
                        return "Desconodido";
                }


            }
            set
            {

            }
        }
        public Nullable<int> IdTitularVehicle { get; set; }
        public Nullable<int> IdInfractor { get; set; }
        public bool NotificacioResolutiva { get; set; }
        public Nullable<int> IdMenorInfractor { get; set; }
        public Nullable<int> IdDomiciliNotificacio { get; set; }
        public string Conducta { get; set; }
        public string Allegacions { get; set; }
        #endregion

        #region InfraccionTabProperties

        private PreceptoDto _precepto;

        [AssistantRequiredField]
        [AssistantSearchField]
        public PreceptoDto Precepto
        {
            get { return _precepto; }
            set
            {
                _precepto = value;
                if(value != null) IdPrecepte = value.Id;
            }
        }

        public int IdPrecepte { get; set; }
        public Nullable<bool> Penal { get; set; }

        public string Article { get; set; }
        public string Apartat { get; set; }
        public string Opcio { get; set; }

        public string Punts { get; set; }
        #endregion

        #region VehiculoTabProperties
        public int IdVehicle { get; set; }
        #endregion

        #region TabFotos
        private int _totalFotos;
        private DateTime _dia;
        private DateTime _hora;

        public int TotalFotos
        {
            get { return _totalFotos; }
            set
            {
                _totalFotos = ListaFotos.Count;
                OnPropertyChanged(new PropertyChangedEventArgs("TotalFotos"));
                OnPropertyChanged(new PropertyChangedEventArgs("TotalFotosTxt"));
            }
        }



        public string TotalFotosTxt => $"{TotalFotos} ";
        #endregion

        public ObservableCollection<int> ListaFotos { get; set; }


        public Nullable<bool> UsarGrua { get; set; }
        public Nullable<int> NumRemesaExecutiva { get; set; }
        public Nullable<int> NumRegistreExecutiva { get; set; }
        public Nullable<bool> TraspasatSCT { get; set; }
        public string NumIAM { get; set; }
        public bool EsSeleccionada { get; set; }
        public Nullable<int> IdMotiuAnulacio { get; set; }
        public Nullable<bool> UltimaRespostaPositiva { get; set; }
        public Nullable<int> IdCea { get; set; }
        public Nullable<int> IdGrupoDoc { get; set; }
        public Nullable<int> NumFitxers { get; set; }
        public Nullable<int> NumDocuments { get; set; }
        public Nullable<int> NumFotos { get; set; }
        public Nullable<int> RefIdProvaAlcoEstup { get; set; }
        public string RefNumExpedProvaAlcoEstup { get; set; }
        public string GDAny { get; set; }
        #endregion
        #region Navigation Properties
        //public Calle Carrer { get; set; }
        
        #endregion

        public bool IsProcessed
        {
            get
            {
                var ret = false;
                try
                {
                    ret = ListaFotos.Aggregate(ret, (current, foto) => current ) && Id > 0;
                }
                catch (Exception)
                {
                    ret = false;
                }
                return ret;
            }
        }
        public bool IsNotifying { get; set; }


    }
}