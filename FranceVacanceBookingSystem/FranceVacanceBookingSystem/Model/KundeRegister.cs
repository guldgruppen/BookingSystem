using System.Collections.Generic;

namespace FranceVacanceBookingSystem.Model
{
    public class KundeRegister
    {
        public Dictionary<int, Kunde> KundeMedId { get; set; }

        public KundeRegister()
        {
            KundeMedId = new Dictionary<int, Kunde>();
        }

        public void AddKunde(string username, string password, string repeatPassword ,string adresse,string email, string name, string tlf)
        {
            KundeMedId.Add(Kunde.Id,new Kunde(adresse, email, name, tlf,username,password,repeatPassword));
        }
    }
}
