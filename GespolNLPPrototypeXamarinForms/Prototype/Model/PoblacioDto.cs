using System.ComponentModel;

namespace XamarinFormsAssistant.Prototype.Model
{
    public class PoblacioDto : EntityBase
    {
        private string _descripcio;

        public string Descripcio
        {
            get { return _descripcio; }
            set
            {
                _descripcio = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Descripcio"));

            }
        }
    }
}