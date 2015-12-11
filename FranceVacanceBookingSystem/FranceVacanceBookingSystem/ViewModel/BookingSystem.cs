using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.Security.Cryptography.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
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
        private int _selectedAntalVærelser;
        private bool _selectedHusdyr;
        private bool _selectedSwimmingpool;
        private Uri _currentImage1;

        #endregion
        #region Properties

        public DispatcherTimer Timer { get; set; } = new DispatcherTimer();
        public static int CountBackground { get; set; } = 0;
        public  Uri[] BackgroundImages { get; set; }
        public Uri CurrentImage
        {
            get { return _currentImage1; }
            set
            {
                _currentImage1 = value;
                OnPropertyChanged();
            }
        }
        public int SelectedAntalVærelser
        {
            get { return _selectedAntalVærelser; }

            set
            {
                _selectedAntalVærelser = value;
                OnPropertyChanged();
            }
        }
        public int SelectedAntalPersoner { get; set; }
        public bool SelectedHusdyr
        {
            get { return _selectedHusdyr; }

            set
            {
                _selectedHusdyr = value;
                OnPropertyChanged();
            }
        }
        public bool SelectedSwimmingpool
        {
            get { return _selectedSwimmingpool; }

            set
            {
                _selectedSwimmingpool = value;
                OnPropertyChanged();
            }
        }       
        public ProfileRegister ProfileRegister { get; set; }
        public KundeRegister KundeRegister { get; set; }
        public AdminRegister AdminRegister { get; set; }
        public BookingRegister BookingRegister { get; set; }
        public static Profile Pro { get; set; }

        public Profile LoginProfile { get; set; }
        public static string LoginUsername { get; set; }
        public int VærelseComboIndex { get; set; }
        public int PersonerComboIndex { get; set; }
        public ObservableCollection<Sommerhus> Sommerhuse { get; set; }
        public static ObservableCollection<Sommerhus> SommerhusMatch { get; set; } = new ObservableCollection<Sommerhus>();
        public ObservableCollection<int> PersoneriCombobox { get; set; }
        public ObservableCollection<int> VærelserICombobox { get; set; }
        public int AntalPersoner { get; set; }
        public int AntalSoveværelser { get; set; }
        public int AntalBadeværelser { get; set; }
        public int Størrelse { get; set; }
        public string Beliggenhed { get; set; }
        public int Pris { get; set; }
        public static DateTimeOffset FraDato { get; set; }
        public static DateTimeOffset TilDato { get; set; }

        public DateTimeOffset MinValueFra { get; set; } = DateTimeOffset.Parse("01-01-2016");
        public DateTimeOffset MaxValueFra { get; set; }  = DateTimeOffset.Parse("01-01-2016");
        public DateTimeOffset MinValueTil { get; set; } = DateTimeOffset.Parse("01-01-2020");
        public DateTimeOffset MaxValueTil { get; set; } = DateTimeOffset.Parse("01-01-2020");


        public bool HusdyrTilladt { get; set; }
        public bool Swimmingpool { get; set; }               
        public string[] LoginTypes
        {
            get { return _loginTypes; }
            set { _loginTypes = value; }
        }
        public int SelectedIndexLoginType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string Tlf { get; set; }
        public MessageDialog ConDialog { get; set; }
        public MessageDialog BonDialog { get; set; }
        public int SelectedIndexListeKunde { get; set; }
        public int SommerhusIndex { get; set; } = -1;

        public RelayCommand NavToMainSystemCommand { get; set; }
        public RelayCommand AddProfileWithCustomerCommand { get; set; }
        public RelayCommand NavToOretProfilCommand { get; set; }
        public RelayCommand SendEmailCommand { get; set; }
        public RelayCommand DeleteCustomersCommand { get; set; }
        public RelayCommand NavToOpretSommerhus { get; set; }
        public RelayCommand NavToListSommerhus { get; set; }
        public RelayCommand LogudCommand { get; set; }
        public RelayCommand AddSommerhusCommand { get; set; }
        public RelayCommand DeleteKundeCommand { get; set; }
        public RelayCommand ShowPageOmOsCommand { get; set; }
        public RelayCommand ShowPageKontaktOsCommand { get; set; }
        public RelayCommand BookingCommand { get; set; }

        #endregion
        #region Constructors
        public BookingSystem()
        {
            Sommerhuse = new ObservableCollection<Sommerhus>();
            PersoneriCombobox = new ObservableCollection<int>();
            VærelserICombobox = new ObservableCollection<int>();
                       
            //InitSommerhus();

            ProfileRegister = new ProfileRegister();
            KundeRegister = new KundeRegister();
            AdminRegister = new AdminRegister();
            BookingRegister = new BookingRegister();

            LoadKunder();
            LoadProfiles();
            LoadSommerhuse();
            LoadBookings();
           
            BackgroundImages = new Uri[] { new Uri("ms-appx:///assets/FranceImage.jpg"), new Uri("ms-appx:///assets/FranceImage2.jpg"), new Uri("ms-appx:///assets/louvre-museum.jpg") };
            CurrentImage = BackgroundImages[CountBackground];            
            StartTimer();
           

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
            NavToOpretSommerhus = new RelayCommand(() =>
            {
                _navigationService.Navigate(typeof(OpretSommerhus));
            });
            NavToListSommerhus = new RelayCommand(() =>
            {
                try
                {
                    CheckComboboxValue(SelectedAntalPersoner,SelectedAntalVærelser);
                    CheckTime(FraDato, TilDato);
                    MatchAfSommerhus();
                    _navigationService.Navigate(typeof(SommerhusListe));                    
                }
                catch (ArgumentException ex)
                {
                    Dialog.Show(ex.Message);
                }                                             
            });
            LogudCommand = new RelayCommand(() =>
            {
                _navigationService.Navigate(typeof(Login));        
            });
            DeleteKundeCommand = new RelayCommand(DeleteKundeFromListe);
            
            AddSommerhusCommand = new RelayCommand(AddSommerhus);
            

            ShowPageOmOsCommand = new RelayCommand(ShowOmOs);
            ShowPageKontaktOsCommand = new RelayCommand(ShowKontaktOs);            
            BookingCommand = new RelayCommand(TryToBook);

           

        }

        #endregion
        #region Methods
        private void MatchAfSommerhus()
        {                               
                SommerhusMatch.Clear(); 
                          
                foreach (var matchingSommerhus in Sommerhuse.Where(x => x.AntalPersoner >= SelectedAntalPersoner && x.AntalSoveværelser >= SelectedAntalVærelser && x.HusdyrTilladt == SelectedHusdyr && x.Swimmingpool == SelectedSwimmingpool))
                {                                     
                    SommerhusMatch.Add(matchingSommerhus);
                foreach (var bookings in BookingRegister.Bookings)
                {
                    if (matchingSommerhus.Equals(bookings.BookingSommerhus))
                    {
                        if ((FraDato > bookings.BookingFra && FraDato < bookings.BookingTil) ||
                            (TilDato > bookings.BookingFra && TilDato < bookings.BookingTil))
                        {
                            SommerhusMatch.Remove(matchingSommerhus);
                        }
                        
                    }
                }
            } 
                    
        }
        public void StartTimer()
        {
            Timer.Interval = TimeSpan.FromSeconds(10);
            Timer.Tick += (sender, e) =>
            {
                if (CountBackground == 2)
                    CountBackground = 0;
                else
                    CountBackground++;

                if (CountBackground == 0)
                {
                    CurrentImage = BackgroundImages[0];
                }
                if (CountBackground == 1)
                    CurrentImage = BackgroundImages[1];
                if (CountBackground == 2)
                {
                    CurrentImage = BackgroundImages[2];
                }

            };
            Timer.Start();
        }
        public void CheckTime(DateTimeOffset fra, DateTimeOffset til)
        {           
            if(fra > til)
                throw new ArgumentException("Din fradato skal være tidligere end din tildato");
            var difTimeSpan = til - fra;
            int DifInNumber = difTimeSpan.Days;
            if(DifInNumber > 365)
                throw new ArgumentException("Du kan ikke rejse mere end 2 måneder");
            if(DifInNumber == 0)
                throw new ArgumentException("Du skal minimum være afsted i 1 dag");
        }
        public void TryToBook()
        {
            try
            {
                NoSelectedIndex(SommerhusIndex);
                Sommerhus temp = SommerhusMatch[SommerhusIndex];
                BonDialog = new MessageDialog("Er du sikker på at du vil booke sommerhuset i "+temp.Beliggenhed+ " fra: "+FraDato.ToString("d") + " til: "+TilDato.ToString("d"));
                BonDialog.Commands.Add(new UICommand("JA", command =>
                {                    
                    BookingRegister.AddBooking(Pro, temp, FraDato, TilDato);
                    Dialog.Show("Booking gennemført");
                    BookingPersistency.SaveBookingAsJsonAsync(BookingRegister.Bookings);
                }));
                BonDialog.Commands.Add(new UICommand("NEJ", command => { }));
                BonDialog.ShowAsync();

            }
            catch (ArgumentOutOfRangeException)
            {
                Dialog.Show("Du har ikke valgt noget sommerhus at booke");
            }
            catch (NullReferenceException ex)
            {
                Dialog.Show(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Dialog.Show(ex.Message);
            }
        }
        public void CheckComboboxValue(int per, int vær)
        {
            if(per == 0)
                throw new ArgumentException("Du mangler at indtaste personer");
            if(vær == 0)
                throw new ArgumentException("Du mangler at indtaste værelser");              
        }
        public void DeleteKundeFromListe()
        {
            try
            {
                NoSelectedIndex(SelectedIndexListeKunde);
                ConDialog = new MessageDialog("Er du sikker på at ville slette en kunde fra listen?");
                ConDialog.Commands.Add(new UICommand("JA", command =>
                {
                    KundeRegister.Kunder.Remove(KundeRegister.Kunder[SelectedIndexListeKunde]);
                    KundePersistency.SaveKunderAsJsonAsync(KundeRegister.Kunder);
                }));
                ConDialog.Commands.Add(new UICommand("NEJ", command => { }));
            }
            catch (ArgumentOutOfRangeException)
            {
                Dialog.Show("Ingen kunde er valgt");
                return;
            }
            ConDialog.ShowAsync();


        }
        public void NoSelectedIndex(int val)
        {
            if (val < 0 )
            throw new ArgumentOutOfRangeException();
        }
        public void ShowOmOs()
        {
            _navigationService.Navigate(typeof(OmOs));

        }
        public void ShowKontaktOs()
        {
            _navigationService.Navigate(typeof(KontaktOs));
        }
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
            KundePersistency.SaveKunderAsJsonAsync(KundeRegister.Kunder);
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
                    Pro = ProfileRegister.FindProfile(Username, Password);
                    LoginUsername = Pro.Username;                                                                       
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
        private async void LoadKunder()
        {           
            var loadedKunder = await KundePersistency.LoadKunderFromJsonAsync();

            if (loadedKunder != null)
            {
                KundeRegister.Kunder.Clear();             
                foreach (var kunde in loadedKunder)
                {
                    KundeRegister.Kunder.Add(kunde);               
                }
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
        public void AddSommerhus()
        {
            try
            {
                Sommerhuse.Add(new Sommerhus(AntalPersoner,AntalBadeværelser, AntalSoveværelser, Beliggenhed,HusdyrTilladt, Pris, Størrelse,Swimmingpool));
                Dialog.Show("Sommerhus er tilføjet");
                SommerhusPersistency.SaveSommerhusAsJsonAsync(Sommerhuse);
            }
            catch (ArgumentException ex)
            {
                Dialog.Show(ex.Message);
            }
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
        public void InitSommerhus()
        {
            Sommerhuse.Add(new Sommerhus(5,2, 4, "Val Torens", true, 5000, 250, true));
            Sommerhuse.Add(new Sommerhus(5,3, 6, "Val Torens", true, 5000, 250, true));
            Sommerhuse.Add(new Sommerhus(5,1, 3, "DET VIRKER", false, 3500, 150, false));
            Sommerhuse.Add(new Sommerhus(5,2, 4, "Val Torens", true, 5000, 250, true));
            Sommerhuse.Add(new Sommerhus(5,7, 9, "Val Torens", true, 5000, 250, true));
            Sommerhuse.Add(new Sommerhus(5,2, 3, "Val Torens", false, 3500, 150, false));
            Sommerhuse.Add(new Sommerhus(5,2, 4, "Val Torens", true, 5000, 250, true));
            Sommerhuse.Add(new Sommerhus(5,6, 8, "Val Torens", true, 5000, 250, true));
            Sommerhuse.Add(new Sommerhus(5,3, 3, "Val Torens", false, 3500, 150, false));
        }
        public void DeleteCustomer()
        {
            ProfileRegister.Profiles = null;
            OnPropertyChanged();
            Dialog.Show("done");
        }    
        private async void LoadSommerhuse()
        {
            var loadedSommerhuse = await SommerhusPersistency.LoadSommerhuseFromJsonAsync();
            if (loadedSommerhuse != null)
            {
                Sommerhuse.Clear();
                foreach (var s in loadedSommerhuse)
                {
                    Sommerhuse.Add(s);
                }
                PersoneriCombobox.Clear();
                foreach (var item in loadedSommerhuse.OrderBy(x => x.AntalPersoner).Select(x => x.AntalPersoner).Distinct())
                {
                    PersoneriCombobox.Add(item);
                }
                VærelserICombobox.Clear();
                foreach (var item in loadedSommerhuse.OrderBy(x => x.AntalSoveværelser).Select(x => x.AntalSoveværelser).Distinct())
                {
                    VærelserICombobox.Add(item);
                }
            }
        }
        private async void LoadBookings()
        {
            var loadedBookings = await BookingPersistency.LoadBookingFromJsonAsync();
               
            if (loadedBookings != null)
            {
                BookingRegister.Bookings.Clear();
                foreach (var s in loadedBookings)
                {
                    BookingRegister.Bookings.Add(s);
                }
                OnPropertyChanged();

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
