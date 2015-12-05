using System;
using System.Collections.Generic;
using FranceVacanceBookingSystem.Common;
using FranceVacanceBookingSystem.Model;
using FranceVacanceBookingSystem.Persistency;
using FranceVacanceBookingSystem.View;
using WpfApplication.ViewModel;


namespace FranceVacanceBookingSystem.ViewModel
{
    public class BookingSystem
    {
        #region Instance Fields
        private NavigationService _navigationService;
        private string[] _loginTypes = new string[] { "Kunde", "Admin" };

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
        public RelayCommand ShowCustomerCommand { get; set; }
        

        #endregion

        #region Constructors
        public BookingSystem()
        {
            ProfileRegister = new ProfileRegister();
            KundeRegister = new KundeRegister();
            AdminRegister = new AdminRegister();
            LoadProfiles();
            LoadKunder();

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
            ShowCustomerCommand = new RelayCommand(() =>
            {
                Dialog.Show(KundeRegister.KundeMedId.Count.ToString());
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

            KundePersistency.SaveKunderAsJsonAsync(KundeRegister.KundeMedId);
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
                    Profile LoginProfile = ProfileRegister.FindProfile(Username, Password);
                    ProfilId = GetId(LoginProfile.Username);

                    NavigateToMainSystem();
                }
                if (LoginTypes[SelectedIndexLoginType] == "Admin")
                {
                    AdminRegister.FindAdmin(Username, Password);
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

        public int GetId(string username)
        {
            foreach (KeyValuePair<int, Kunde> kunder in KundeRegister.KundeMedId)
            {
                if (kunder.Value.Username == username)
                    return kunder.Key;
            }
            return 100;
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
        private async void LoadKunder()
        {
            var loadedKunder = await KundePersistency.LoadKunderFromJsonAsync();

            if (loadedKunder != null)
            {
                KundeRegister.KundeMedId.Clear();
                foreach (var kunde in loadedKunder)
                {
                    KundeRegister.KundeMedId.Add(kunde.Key, kunde.Value);
                }

            }

        }


        #endregion


    }
}
