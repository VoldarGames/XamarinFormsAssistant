using System.ComponentModel;

namespace XamarinFormsAssistant.Prototype.Model
{
    public abstract class EntityBase : INotifyPropertyChanged
    {
        
        public int Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null) PropertyChanged(this, e);

        }
    }
}