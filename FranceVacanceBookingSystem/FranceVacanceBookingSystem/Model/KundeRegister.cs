using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FranceVacanceBookingSystem.Annotations;
using FranceVacanceBookingSystem.Persistency;

namespace FranceVacanceBookingSystem.Model
{
    public class KundeRegister : INotifyPropertyChanged
    {
        public ObservableCollection<Kunde> Kunder { get; set; }
        
       
        public KundeRegister()
        {
           Kunder = new ObservableCollection<Kunde>();
            Kunder.Add(new Kunde("bob", "bob", "bob", "bobbobbo", "bob", "bob"));
            Kunder.Add(new Kunde("bob", "bob", "bob", "bobbobbo", "bob", "bob"));
            Kunder.Add(new Kunde("bob", "bob", "bob", "bobbobbo", "bob", "bob"));

        }

        public void AddKunde(string username, string password,string adresse,string email, string name, string tlf)
        {
            Kunder.Add(new Kunde(adresse, email, name, tlf, username, password));    
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       
    }
}
