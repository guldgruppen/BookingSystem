using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using FranceVacanceBookingSystem.Annotations;
using FranceVacanceBookingSystem.Common;
using FranceVacanceBookingSystem.Model;
using FranceVacanceBookingSystem.View;
using WpfApplication.ViewModel;

namespace FranceVacanceBookingSystem.ViewModel
{
    public class SommerhusKatalog : INotifyPropertyChanged
    {
       
        #region Properties       
        public int AntalPersoner { get; set; }
        public int AntalVærelser { get; set; }
        public int FraDato { get; set; }
        public int TilDato { get; set; }
        public bool HusdyrTilladt { get; set; }
        public bool Swimmingpool { get; set; }
        public Dictionary<int, int> av { get; set; }
        public ObservableCollection<Sommerhus> Sommerhuse { get; set; }

        public RelayCommand NavToOpretCommand { get; set; }
        public RelayCommand NavToListCommand { get; set; }
        #endregion
        #region constructors
        public SommerhusKatalog()
        {
            Sommerhuse = new ObservableCollection<Sommerhus>();
            initSommerhus();
            LoadSommerhuse();


        } 
        #endregion

        public void initSommerhus()
        {
            Sommerhuse.Add(new Sommerhus(100, 2, 2, 4, "Val Torens", true, 5000, 250, true));
            Sommerhuse.Add(new Sommerhus(1000, 20, 20, 40, "Val Torens", true, 5000, 250, true));
            Sommerhuse.Add(new Sommerhus(3000, 1, 0, 3, "Val Torens", false, 3500, 150, false));
            Sommerhuse.Add(new Sommerhus(100, 2, 2, 4, "Val Torens", true, 5000, 250, true));
            Sommerhuse.Add(new Sommerhus(1000, 20, 20, 40, "Val Torens", true, 5000, 250, true));
            Sommerhuse.Add(new Sommerhus(3000, 1, 0, 3, "Val Torens", false, 3500, 150, false));
            Sommerhuse.Add(new Sommerhus(100, 2, 2, 4, "Val Torens", true, 5000, 250, true));
            Sommerhuse.Add(new Sommerhus(1000, 20, 20, 40, "Val Torens", true, 5000, 250, true));
            Sommerhuse.Add(new Sommerhus(3000, 1, 0, 3, "Val Torens", false, 3500, 150, false));
        }
        private async void LoadSommerhuse()
        {
            var loadedSommerhuse = await Persistency.SommerhusPersistency.LoadSommerhuseFromJsonAsync();
            if (loadedSommerhuse != null)
            {
                Sommerhuse.Clear();
                foreach (var s in loadedSommerhuse)
                {
                    Sommerhuse.Add(s);
                }

            }
        }

        private void AddToComboBox()
        {
            av = new Dictionary<int, int>();

            Sommerhuse.Select(x => x.AntalSoveværelser).Distinct();

            foreach (int item in Sommerhuse.Select(x => x.AntalSoveværelser).Distinct())
            {
                av.Add(item, item);
            }
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
