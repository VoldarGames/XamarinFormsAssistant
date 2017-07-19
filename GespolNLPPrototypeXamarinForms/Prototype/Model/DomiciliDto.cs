using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace XamarinFormsAssistant.Prototype.Model
{
    public class DomiciliDto : EntityBase
    {
        public const String PropPoblacioCodiPostal = "PoblacioCodiPostal";
        public const String PropNumeroCarrer = "NumeroCarrer";
        public const String PropBloc = "Bloc";
        public const String PropPis = "Pis";
        public const String PropEscala = "Escala";
        public const String PropPorta = "Porta";
        public const String PropBis = "Bis";
        public const String PropPortal = "Portal";
        public const String PropDescripcio = "Descripcio";
        private PoblacioDto _poblacion;

        private Calle _carrerActual;
        public Calle Carrer
        {
            get { return _carrerActual; }
            set
            {
                _carrerActual = value; OnPropertyChanged(new PropertyChangedEventArgs(nameof(Carrer)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(DescripcionCompleta)));
                //OnPropertyChanged(new PropertyChangedEventArgs(CalleDescipcion));
            }
        }

        
        public int? CalleId { get; set; }

        private string _poblacioCodiPostal;

        public string PoblacioCodiPostal
        {
            get
            {
                return _poblacioCodiPostal;
            }
            set
            {
                _poblacioCodiPostal = value;
                OnPropertyChanged(new PropertyChangedEventArgs(PropPoblacioCodiPostal));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(DescripcionCompleta)));
            }
        }

        private string _descripcio;

        public string Descripcio
        {
            get
            {
                return _descripcio;
            }
            set
            {
                _descripcio = value;
                OnPropertyChanged(new PropertyChangedEventArgs(PropDescripcio));
            }
        }
        //NEW
        private string _numeroCarrer;

        public string NumeroCarrer
        {
            get
            {
                return _numeroCarrer;
            }
            set
            {
                _numeroCarrer = value;
                OnPropertyChanged(new PropertyChangedEventArgs(PropNumeroCarrer));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(DescripcionCompleta)));
            }
        }

        private string _bloc;

        public string Bloc
        {
            get
            {
                return _bloc;
            }
            set
            {
                _bloc = value;
                OnPropertyChanged(new PropertyChangedEventArgs(PropBloc));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(DescripcionCompleta)));
            }
        }

        private string _pis;

        public string Pis
        {
            get
            {
                return _pis;
            }
            set
            {
                _pis = value;
                OnPropertyChanged(new PropertyChangedEventArgs(PropPis));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(DescripcionCompleta)));
            }
        }

        private string _escala;

        public string Escala
        {
            get
            {
                return _escala;
            }
            set
            {
                _escala = value;
                OnPropertyChanged(new PropertyChangedEventArgs(PropEscala));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(DescripcionCompleta)));
            }
        }

        private string _porta;

        public string Porta
        {
            get
            {
                return _porta;

            }
            set
            {
                _porta = value;
                OnPropertyChanged(new PropertyChangedEventArgs(PropPorta));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(DescripcionCompleta)));
            }
        }

        private string _bis;

        public string Bis
        {
            get
            {
                return _bis;
            }
            set
            {
                _bis = value;
                OnPropertyChanged(new PropertyChangedEventArgs(PropBis));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(DescripcionCompleta)));
            }
        }

        private string _portal;

        public string Portal
        {
            get
            {
                return _portal;
            }
            set
            {
                _portal = value;
                OnPropertyChanged(new PropertyChangedEventArgs(PropPortal));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(DescripcionCompleta)));
            }
        }


        public PoblacioDto Poblacio
        {
            get { return _poblacion; }
            set
            {
                _poblacion = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Poblacio)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(DescripcionCompleta)));
            }
        }
        public int? PoblacionId { get; set; }

        private ObservableCollection<Persona> _residentes;
        public ObservableCollection<Persona> Residentes
        {
            get
            {
                return _residentes;
            }
            set
            {
                _residentes = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Residentes)));
            }
        }

        public string DescripcionCompleta
        {
            get
            {
                if (string.IsNullOrEmpty(Descripcio))
                    return $"{Carrer?.Descripcion} {NumeroCarrer}{Bis} {Bloc} {Portal} {Escala} {Pis} {Porta} ({PoblacioCodiPostal} - {Poblacio?.Descripcio})";
                else
                {
                    //return $"{Descripcio} ({PoblacioCodiPostal} - {Poblacio?.Descripcio})";
                    return $"{Descripcio}";
                }
            }
            set
            {

            }
        }
    }
}