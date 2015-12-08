using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FranceVacanceBookingSystem.Annotations;

namespace FranceVacanceBookingSystem.Model
{
    public class KundeRegister : INotifyPropertyChanged
    {
        public Dictionary<int, Kunde> KundeMedId { get; set; }
       
        public KundeRegister()
        {
            KundeMedId = new Dictionary<int, Kunde>();
            KundeMedId.Add(Kunde.Id,new Kunde("bob","bob","bob","bobbobbo","bob","bob"));
            KundeMedId.Add(Kunde.Id, new Kunde("bob", "bob", "bob", "bobbobbo", "bob", "bob"));
           
        }

        public void AddKunde(string username, string password,string adresse,string email, string name, string tlf)
        {
            KundeMedId.Add(Kunde.Id, new Kunde(adresse, email, name, tlf, username, password));
            OnPropertyChanged();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
