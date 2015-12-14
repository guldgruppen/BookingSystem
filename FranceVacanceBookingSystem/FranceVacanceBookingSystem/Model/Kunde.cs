using System;
using System.Collections.ObjectModel;
using Windows.UI.Core.AnimationMetrics;

namespace FranceVacanceBookingSystem.Model
{
    public class Kunde
    {

        #region Instance Fields
        private string _name;
        private string _adress;
        private string _email;
        private string _tlf;
        #endregion

        #region Properties
        public string Adress
        {
            get { return _adress; }
            set { CheckAdress(value); _adress = value; }
        }
        public string Email
        {
            get { return _email; }
            set { CheckEmail(value); _email = value; }
        }
        public string Name
        {
            get { return _name; }
            set { CheckName(value); _name = value; }
        }
        public string Tlf
        {
            get { return _tlf; }
            set { CheckTlf(value); _tlf = value; }
        }
        public string Username { get; set; }
        public string Password { get; set; }

        public ObservableCollection<Sommerhus> Favoritter { get; set; }


        #endregion

        #region Constructors
        public Kunde(string adress, string email, string name, string tlf,string username,string password)
        {
            CheckName(name);
            CheckAdress(adress);
            CheckEmail(email);
            CheckTlf(tlf);            
            Adress = adress;
            Email = email;
            Name = name;
            Tlf = tlf;
            Username = username;
            Password = password;
            Favoritter = new ObservableCollection<Sommerhus>();
           
        } 
        #endregion

        public void AddFavoritSommerhus(Sommerhus s)
        {
            Favoritter.Add(s);
        }

        public override string ToString()
        {
            return $"Adress: {Adress}, Email: {Email}, Name: {Name}, Tlf: {Tlf}, Username: {Username}, Password: {Password}";
        }

        #region CheckInformationRegion

        public void CheckTlf(string tlf)
        {
            if (String.IsNullOrWhiteSpace(tlf) || tlf.Length != 8)
                throw new ArgumentException("Venlig indtast et korret telefon nummer");
        }

        public void CheckName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Venlist indtast et ordentlig navn");
            }
        }

        public void CheckAdress(string adresse)
        {
            if (String.IsNullOrWhiteSpace(adresse))
                throw new ArgumentException("Venligst indtast en korrekt adresse");
        }
        public void CheckEmail(string email)
        {
            if (String.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Venligst indtast en korrekt email");
        }

        #endregion
    }
}
