using System;
using System.Collections.Generic;
using System.ComponentModel;
using XamarinFormsAssistant.Assistant.Attributes;

namespace XamarinFormsAssistant.Prototype.Model
{
    public class Persona : PersonaBase
    {
        public const String PropNom = "Nom";
        public const String PropCognoms = "Cognoms";
        public const String PropDni = "DNI";

        private DomiciliDto _domiciliActual;

        public string GetPersonDataListView()
        {
            return $"{DNI} {Nom} {Cognoms} ";
        }

        public int? Edad => (DateTime.Now - DataNaixement).GetValueOrDefault().Days / 365;

        public string GetPersonDataFull
        {
            get
            {
                var edad = Edad.ToString();
                if (string.IsNullOrEmpty(edad) || edad == "0") edad = "¿?";
                if (string.IsNullOrEmpty(DNI)) return null;
                return $"{DNI} ,Edad: {edad}\n{Nom}, {Cognoms}";
            }
        }

        private string _nom;
        private DateTime? _dataNaixement;

        [AssistantRequiredField]
        [AssistantRawStringField]
        public string Nom
        {
            get { return _nom; }
            set
            {
                _nom = value;
                OnPropertyChanged(new PropertyChangedEventArgs(PropNom));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(GetPersonDataFull)));
            }
        }
        public string Sexe { get; set; }
        public int Codi { get; set; }
        private string _cognoms { get; set; }

        [AssistantRequiredField]
        [AssistantRawStringPhraseField]

        public string Cognoms
        {
            get
            {
                return _cognoms;
            }
            set
            {
                _cognoms = value;
                OnPropertyChanged(new PropertyChangedEventArgs(PropCognoms));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(GetPersonDataFull)));
            }
        }

        private string _DNI { get; set; }

        [AssistantRequiredField]
        [AssistantRawStringPhraseField]
        public string DNI
        {
            get
            {
                return _DNI;
            }
            set
            {
                _DNI = value;
                OnPropertyChanged(new PropertyChangedEventArgs(PropDni));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(GetPersonDataFull)));
            }
        }
        public int IdClient { get; set; }

        public DateTime? DataNaixement
        {
            get { return _dataNaixement; }
            set
            {
                _dataNaixement = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(DataNaixement)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Edad)));
            }
        }

        public string Nacionalitat { get; set; }
        public string NombreEmpresa { get; set; }
        public DomiciliDto DomiciliActual
        {
            get { return _domiciliActual; }
            set
            {
                _domiciliActual = value; OnPropertyChanged(new PropertyChangedEventArgs(nameof(DomiciliActual)));
            }
        }
        public int DomiciliDtoId { get; set; }

        public string PropDomicili { get; set; }

        public List<int> Requisitorias { get; set; }
    }
}