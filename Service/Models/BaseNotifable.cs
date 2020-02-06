
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QRGenerator.Services.Models
{
    public class BaseNotifable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        protected virtual void SetProperty<T>(ref T member, T val, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(member, val))
            {                
                return;
               
            }


           
            member = val;
            OnPropertyChange(propertyName);
        }
        protected virtual void AlwaysSetProperty<T>(ref T member, T val, [CallerMemberName] string propertyName = null)
        {
            member = val;
            OnPropertyChange(propertyName);
        }
        protected virtual void OnPropertyChange(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        
    }
}
