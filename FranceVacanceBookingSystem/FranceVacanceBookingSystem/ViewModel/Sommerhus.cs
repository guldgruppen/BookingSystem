using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FranceVacanceBookingSystem.Model;
using FranceVacanceBookingSystem.Common;
using WpfApplication.ViewModel;

namespace FranceVacanceBookingSystem.ViewModel
{
    class Sommerhus
    {
        private ObservableCollection<SommerhusBeskrivelse> _sommerhuse;

        public ObservableCollection<SommerhusBeskrivelse> Sommerhuse
        {
            get
            {
                return _sommerhuse;
            }

            set
            {
                _sommerhuse = value;
            }
        }

        public double Distancefromwater { get; set; }
        public int Bathrooms { get; set; }
        public int Parkinglots { get; set; }
        public int Bedrooms { get; set; }
        public string Location { get; set; }
        public bool Petallowed { get; set; }
        public double Price { get; set; }
        public double Size { get; set; }

        public RelayCommand AddSommerhuCommand { get; set; }

        public Sommerhus()
        {
            Sommerhuse = new ObservableCollection<SommerhusBeskrivelse>();
            Sommerhuse.Add(new SommerhusBeskrivelse(5,2,2,1,"Paris",false,5000,180));
            AddSommerhuCommand = new RelayCommand(AddSommerhus);
            PersistencyService.SaveNotesAsJsonAsync(Sommerhuse);
        }

        public void AddSommerhus()
        {
            Sommerhuse.Add(new SommerhusBeskrivelse(Distancefromwater,Bathrooms,Parkinglots,Bedrooms, Location, Petallowed, Price, Size));
        }
    }
}
