using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Navigation;
using FranceVacanceBookingSystem.Annotations;
using FranceVacanceBookingSystem.Common;
using FranceVacanceBookingSystem.Model;
using FranceVacanceBookingSystem.View;
using WpfApplication.ViewModel;

namespace FranceVacanceBookingSystem.ViewModel
{
    public class ProfilHandler : INotifyPropertyChanged
    {
        #region Instance Fields
        private ObservableCollection<Profil> _profiles;
        private string _username;
        private string _password;

        
        #endregion

        #region Properties
        public ObservableCollection<Profil> Profiles
        {
            get { return _profiles; }
            set { _profiles = value; }
        }

        
        public MessageDialog dialog { get; set; }
        public string Username
        {
            get { return _username; }
            set
            {

                _username = value;
            }
        }

        public string AddUsername { get; set; }
        public string AddPassword { get; set; }

        public string Password
        {
            get { return _password; }
            set
            {

                _password = value;
            }
        }

        public RelayCommand LoginCommand { get; set; }
        public RelayCommand AddProfileCommand { get; set; }

        #endregion

        #region Constructors
        public ProfilHandler()
        {
            Profiles = new ObservableCollection<Profil>();
            
            Profiles.Add(new Profil("Thomas","Thomas"));
            Profiles.Add(new Profil("Preben", "Preben"));
            Profiles.Add(new Profil("Bob", "Bob"));

            LoginCommand = new RelayCommand(CheckLoginInfo);
            AddProfileCommand = new RelayCommand(AddProfile);

        }  
        #endregion

        #region Methods
    

        public void AddProfile()
        {
            Profiles.Add(new Profil(AddUsername,AddPassword));
            OnPropertyChanged();
            MessageDialog dialog = new MessageDialog("Profil er tilføjet");
            dialog.ShowAsync();
            PersistencyService.SaveNotesAsJsonAsync(Profiles);
        }

        public bool CheckUsername(string name)
        {

            if (String.IsNullOrWhiteSpace(name))
            {
                return true;
            }
            return false;
        }

        public bool CheckPassword(string password)
        {
            if (String.IsNullOrWhiteSpace(password))
            {
                return true;
            }
            return false;
        }

        public void CheckLoginInfo()
        {
            
            if (CheckUsername(Username) == true)
            {
                dialog = new MessageDialog("Du mangler brugernavn");
                
            }
            else if (CheckPassword(Password) == true)
            {
                dialog = new MessageDialog("Du mangler kodeord");
            }
            else
            {
                foreach (Profil profil in Profiles)
                {
                    if (profil.Username == Username && profil.Password == Password)
                    {
                        dialog = new MessageDialog("Login Successful"); 
                                                                                               
                        break;
                    }
                    else
                    {
                        dialog = new MessageDialog("Login fejl - prøv igen");
                        
                        
                    }
                }
            }
            dialog.ShowAsync();

        } 
        #endregion

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
