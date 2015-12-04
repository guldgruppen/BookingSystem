using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using FranceVacanceBookingSystem.Annotations;
using FranceVacanceBookingSystem.Common;
using FranceVacanceBookingSystem.Model;
using FranceVacanceBookingSystem.View;
using WpfApplication.ViewModel;

namespace FranceVacanceBookingSystem.ViewModel
{
    public class SommerhusKatalog : INotifyPropertyChanged
    {
        public ObservableCollection<Sommerhus> Sommerhuse { get; set; }


        public static Profile LoginProfil { get; set; }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public int AntalPersoner { get; set; }
        public int AntalVærelser { get; set; }
        public int FraDato { get; set; }
        public int tilDato { get; set; }
        public bool HusdyrTilladt{ get; set; }
        public bool Swimmingpool { get; set; }
        public int SelectedIndex { get; set; }
        private NavigationService _navigationService;

        private string _username = LoginProfil.Username;

        public RelayCommand ShowUsername { get; set; }

        public RelayCommand NavToOpretCommand { get; set; }

        public RelayCommand NavToListCommand { get; set; }     
       
        public SommerhusKatalog()
        {
            _navigationService = new NavigationService();

            NavToListCommand = new RelayCommand(() =>
            {
                _navigationService.Navigate(typeof(SommerhusListe));
            });
            NavToOpretCommand = new RelayCommand(() =>
            {
                _navigationService.Navigate(typeof(OpretSommerhus));
            });
            ShowUsername = new RelayCommand(() =>
            {
                MessageDialog dialog = new MessageDialog(Username);
                dialog.ShowAsync();
            });

            Sommerhuse = new ObservableCollection<Sommerhus>();
            Sommerhuse.Add(new Sommerhus(100,2,2,4,"Val Torens",true,5000,250,true));
            Sommerhuse.Add(new Sommerhus(1000, 20, 20, 40, "Val Torens", true, 5000, 250, true));
            Sommerhuse.Add(new Sommerhus(3000, 1, 0, 3, "Val Torens", false, 3500, 150, false));
            Sommerhuse.Add(new Sommerhus(100, 2, 2, 4, "Val Torens", true, 5000, 250, true));
            Sommerhuse.Add(new Sommerhus(1000, 20, 20, 40, "Val Torens", true, 5000, 250, true));
            Sommerhuse.Add(new Sommerhus(3000, 1, 0, 3, "Val Torens", false, 3500, 150, false));
            Sommerhuse.Add(new Sommerhus(100, 2, 2, 4, "Val Torens", true, 5000, 250, true));
            Sommerhuse.Add(new Sommerhus(1000, 20, 20, 40, "Val Torens", true, 5000, 250, true));
            Sommerhuse.Add(new Sommerhus(3000, 1, 0, 3, "Val Torens", false, 3500, 150, false));

        }









        #region PropertyChanged Region
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}
