using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Activation;
using Windows.Security.Cryptography.Core;
using FranceVacanceBookingSystem.Annotations;
using FranceVacanceBookingSystem.Common;
using FranceVacanceBookingSystem.Model;
using FranceVacanceBookingSystem.Persistency;
using FranceVacanceBookingSystem.View;
using WpfApplication.ViewModel;


namespace FranceVacanceBookingSystem.ViewModel
{
    public class BookingSystem : INotifyPropertyChanged
    {
        #region Instance Fields
        private readonly NavigationService _navigationService;
        private string[] _loginTypes = { "Kunde", "Admin" };
        private List<Kunde> KunderTilList; 
        private static int _id = 1;       

        #endregion
        #region Properties
        public ProfileRegister ProfileRegister { get; set; }
        public KundeRegister KundeRegister { get; set; }
        public AdminRegister AdminRegister { get; set; }
       
        public string[] LoginTypes
        {
            get { return _loginTypes; }
            set { _loginTypes = value; }
        }
        public static int ProfilId { get; set; }
        public int SelectedIndexLoginType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string Tlf { get; set; }

        
        public RelayCommand NavToMainSystemCommand { get; set; }
        public RelayCommand AddProfileWithCustomerCommand { get; set; }
        public RelayCommand NavToOretProfilCommand { get; set; }
        public RelayCommand SendEmailCommand { get; set; }     
        

        #endregion
        #region Constructors
        public BookingSystem()
        {

            LoadProfiles();
            LoadKunder();

            ProfileRegister = new ProfileRegister();
            KundeRegister = new KundeRegister();
            AdminRegister = new AdminRegister();

            _navigationService = new NavigationService();
            NavToMainSystemCommand = new RelayCommand(CheckLoginInformationAndNavigate);
            
            AddProfileWithCustomerCommand = new RelayCommand(AddCustomerWithProfile);
            NavToOretProfilCommand = new RelayCommand(() =>
            {
                _navigationService.Navigate(typeof(OpretProfil));
            });
            SendEmailCommand = new RelayCommand(() =>
            {
                Dialog.Show("Logininformationer er sendt til din email");
            });           
        }

        #endregion

        #region Methods
        public void AddCustomerWithProfile()
        {
            try
            {
                
                ProfileRegister.AddProfile(Username, Password);
                KundeRegister.AddKunde(Username, Password, Adress, Email, Name, Tlf);
                CheckRepeatPassword(Password, RepeatPassword);
                Dialog.Show("Profil er tilføjet");
            }
            catch (ArgumentException ex)
            {
                Dialog.Show(ex.Message);
            }
            ProfilePersistency.SaveProfilesAsJsonAsync(ProfileRegister.Profiles);

           SaveCustomers();
        }

        public void CheckRepeatPassword(string password, string repeatPassword)
        {
            if (!password.Equals(repeatPassword))
                throw new ArgumentException("Kodeordene stemmer ikke overens");
        }

        public void CheckLoginInformationAndNavigate()
        {
            try
            {
                CheckForNullOrWhiteSpace(Username, Password);
                if (LoginTypes[SelectedIndexLoginType] == "Kunde")
                {
                    Profile loginProfile = ProfileRegister.FindProfile(Username, Password);
                    NavigateToMainSystem();
                }
                if (LoginTypes[SelectedIndexLoginType] == "Admin")
                {
                    Admin TempAdmin = AdminRegister.FindAdmin(Username, Password);
                    AdminVM.KundeDic = KundeRegister.KundeMedId;
                    AdminVM.LoginAdmin = TempAdmin; 
                    NavigateToAdminPage();                  
                }
            }
            catch (ArgumentException ex)
            {
                Dialog.Show(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                Dialog.Show(ex.Message);
            }
        }

        public void CheckForNullOrWhiteSpace(string username, string password)
        {
            if (String.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Venligst indtast et brugernavn");
            if (String.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Venligst indtast et kodeord");
        }

        public void NavigateToMainSystem()
        {
            _navigationService.Navigate(typeof(MainSystem));
        }

        public void NavigateToAdminPage()
        {           
            _navigationService.Navigate(typeof(AdminPage));
        }

        private async void LoadProfiles()
        {
            var loadedProfiles = await ProfilePersistency.LoadProfilesFromJsonAsync();
            if (loadedProfiles != null)
            {
                ProfileRegister.Profiles.Clear();
                foreach (var profile in loadedProfiles)
                {
                    ProfileRegister.Profiles.Add(profile);
                }
            }

        }

        public void SaveCustomers()
        {
            KunderTilList = KundeRegister.KundeMedId.Values.ToList();           
            KundePersistency.SaveKunderAsJsonAsync(KunderTilList);
        }

        private async void LoadKunder()
        {
            _id = 1;
            var loadedKunder = await KundePersistency.LoadKunderFromJsonAsync();
            
            if (loadedKunder != null)
            {
                KundeRegister.KundeMedId.Clear();
                foreach (var kunde in loadedKunder)
                {                   
                    KundeRegister.KundeMedId.Add(_id++,kunde);
                   
                }                
            }                    
        }

        #endregion

        #region OnPropertyChanged Region

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}
