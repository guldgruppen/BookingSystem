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
        private INavigationService _navigationService;
        private string _navn;
        private string _adresse;
        private string _telefonNummer;
        private string _email;
        private string _repeatPassword;

        #endregion

        #region Properties

        public string SendEmail { get; set; }
        public ObservableCollection<Profil> Profiles
        {
            get { return _profiles; }
            set { _profiles = value; }
        }
        public string Username
        {
            get { return _username; }
            set
            {                    
                _username = value;
            }
        }

        public string Navn
        {
            get { return _navn; }
            set
            {             
                _navn = value;
            }
        }

        public string Adresse
        {
            get { return _adresse; }
            set { _adresse = value; }
        }

        public string TelefonNummer
        {
            get { return _telefonNummer; }
            set { _telefonNummer = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
            }
        }

        public string RepeatPassword
        {
            get { return _repeatPassword; }
            set { _repeatPassword = value; }
        }

        public MessageDialog log { get; set; }

        public RelayCommand SendEmailCommand { get; set; }
        public RelayCommand LoginCommand { get; set; }
        public RelayCommand AddProfileCommand { get; set; }
        public RelayCommand NavToOpretProfilCommand { get; set; }
        public string AddUsername { get; private set; }
        public string AddPassword { get; private set; }

        #endregion

        #region Constructors
        public ProfilHandler()
        {           
            Profiles = new ObservableCollection<Profil>();
            _navigationService = new NavigationService();
            Profiles.Add(new Profil("bob","bob"));
            LoginCommand = new RelayCommand(CheckLoginInfo);
            AddProfileCommand = new RelayCommand(AddProfile);
            NavToOpretProfilCommand = new RelayCommand(() =>
            {
                _navigationService.Navigate(typeof (OpretProfil));
            });
            SendEmailCommand = new RelayCommand(() =>
            {
                log = new MessageDialog("E-mail med logininformation er sendt til din email");
                log.ShowAsync();
            });

            LoadProfiles();
        }
        #endregion

        #region Methods

        public void CheckUserName(string name)
        {
            if(String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("prøve");
        }

        public void AddProfile()
        {          
            try
            {
                CheckEmailAndUsername();
                Profiles.Add(new Profil(Adresse,Email,Navn,Password,RepeatPassword,Username,TelefonNummer));          
                log = new MessageDialog("Profil er tilføjet");
                log.ShowAsync();
            }
            catch (ArgumentException e)
            {
                ShowDialog(e.Message);
            }           
           
            PersistencyService.SaveProfileAsJsonAsync(Profiles);
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

        public void NavigateToBookingSystem()
        {
            _navigationService.Navigate(typeof(MainSystem));
        }

        public void CheckEmailAndUsername()
        {
            foreach (Profil profil in Profiles)
            {
                if(profil.Username == Username)
                    throw new ArgumentException("brugernavn eksisterer i forvejen");
                if(profil.Email == Email)
                    throw new ArgumentException("Email eksisterer i forvejen");
            }
        }

        public void CheckLoginInfo()
        {
            
            if (CheckUsername(Username) == true)
            {
                log = new MessageDialog("Du mangler brugernavn");
                
            }
            else if (CheckPassword(Password) == true)
            {
                log = new MessageDialog("Du mangler kodeord");
            }
            else
            {
               
                foreach (Profil profil in Profiles)
                {
                    if (profil.Username == Username && profil.Password == Password)
                    {
                        log = new MessageDialog("Velkommen tilbage "+profil.Username + " :)"); 
                         NavigateToBookingSystem();                                                                      
                        break;
                    }
                    else
                    {
                        log = new MessageDialog("Login fejl - prøv igen");
                                             
                    }
                }
            }
            log.ShowAsync();

        }

        public void ShowDialog(string text)
        {
            MessageDialog dialog = new MessageDialog(text);
            dialog.ShowAsync();
        }

        private async void LoadProfiles()
        {
            var loadedProfiles = await PersistencyService.LoadNotesFromJsonAsync();
            if (loadedProfiles != null)
            {
                Profiles.Clear();
                foreach (var note in loadedProfiles)
                {
                    Profiles.Add(note);
                }

            }

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
