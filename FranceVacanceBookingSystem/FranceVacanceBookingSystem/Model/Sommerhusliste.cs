using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FranceVacanceBookingSystem.Common;
using FranceVacanceBookingSystem.ViewModel;
using Newtonsoft.Json;

namespace FranceVacanceBookingSystem.Model
{
    class Sommerhusliste
    {
        private ObservableCollection<Sommerhus> _sommerhuslist;

        public ObservableCollection<Sommerhus> Sommerhuslist
        {
            get
            {
                return _sommerhuslist;
            }

            set
            {
                _sommerhuslist = value;
            }
        }

        public Sommerhusliste()
        {
            Sommerhuslist = new ObservableCollection<Sommerhus>();

            LoadSommerhus();
        }

        private async void LoadSommerhus()
        {
            var loadedSommerhus = await PersistencyService.LoadSommerhusFromJsonAsync();
            if (loadedSommerhus != null)
            {
                Sommerhuslist.Clear();
                foreach (var sh in loadedSommerhus)
                {
                    Sommerhuslist.Add(sh);
                }

            }
        }
    }
}
