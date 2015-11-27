using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FranceVacanceBookingSystem.Annotations;
using FranceVacanceBookingSystem.Model;
namespace FranceVacanceBookingSystem.ViewModel
{
    public class ProfilHandler : INotifyPropertyChanged
    {
        private ObservableCollection<Profil> _profiles;

        public ObservableCollection<Profil> Profiles
        {
            get { return _profiles; }
            set { _profiles = value; }
        }

        public ProfilHandler()
        {
            
        }
        
        
        
        
        #region OnPropertyChanged region
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}
