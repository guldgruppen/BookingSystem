using System;
using FranceVacanceBookingSystem.Common;
using FranceVacanceBookingSystem.Model;
using FranceVacanceBookingSystem.Persistency;
using FranceVacanceBookingSystem.View;
using WpfApplication.ViewModel;


namespace FranceVacanceBookingSystem.ViewModel
{
    public class BookingSystem
    {
        private NavigationService _navigationService;
        public ProfileRegister ProfileRegister { get; set; }
        public KundeRegister KundeRegister { get; set; }

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
        
       

        public BookingSystem()
        {
            LoadProfiles();
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
            _navigationService = new NavigationService();            
            ProfileRegister = new ProfileRegister();
            KundeRegister = new KundeRegister();
                        
        }

        public void AddCustomerWithProfile()
        {
            try
            {
                ProfileRegister.AddProfile(Username, Password, RepeatPassword,Adress, Email, Name, Tlf);
                ProfilePersistency.SaveProfilesAsJsonAsync(ProfileRegister.Profiles);
                Dialog.Show("Profil er tilføjet");
            }
            catch (ArgumentException ex)
            {
                Dialog.Show(ex.Message);
            }
        }

        public void CheckLoginInformationAndNavigate()
        {
            try
            {
                CheckForNullOrWhiteSpace(Username, Password);
                Profile LoginProfile = ProfileRegister.FindProfile(Username, Password);
                //Tilføj linjen nedenunder til den rette kode.
                SommerhusKatalog.LoginProfil = LoginProfile;
                NavigateToMainSystem();
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
            if(String.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Venligst indtast et brugernavn");
            if(String.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Venligst indtast et kodeord");
        }

        public void NavigateToMainSystem()
        {
            _navigationService.Navigate(typeof(MainSystem));
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




    }
}
