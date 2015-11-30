using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FranceVacanceBookingSystem.Model
{
    public class Profil
    {
        #region Instance Fields
        private string _adresse;
        private string _by;
        private int _postnummer;
        private int _alder;
        private string _email;
        private string _navn;
        private string _password;
        private string _username;
        private int _telefonNummer;

        #endregion
        #region Properties

        public string Adresse
        {
            get { return _adresse; }
            set { _adresse = value; }
        }

        public int Postnummer
        {
            get { return _postnummer; }
            set { _postnummer = value; }
        }

        public int Alder
        {
            get { return _alder; }
            set { _alder = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Navn
        {
            get { return _navn; }
            set { _navn = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string RepeatPassword { get; set; }
        public string Username
        {
            get { return _username; }
            set
            {
                
                _username = value;
            }
        }

        public int TelefonNummer
        {
            get { return _telefonNummer; }
            set { _telefonNummer = value; }
        }

        #endregion


        #region Constructors
        public Profil()
        {

        }

        public Profil(string username, string password,string repeatPassword)
        {
            CheckUser(username);
            CheckPassword(password);
            CheckRepeatPassword(password,repeatPassword);
            Username = username;
            Password = password;
            RepeatPassword = repeatPassword;
        }

        public Profil(string adresse, string email, string navn, string password, string username, int telefonNummer)
        {
            Adresse = adresse;           
            Email = email;
            Navn = navn;
            Password = password;
            Username = username;
            TelefonNummer = telefonNummer;
        }


        public void CheckUser(string name)
        {
            if(String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Venlig indtast et korrekt brugernavn");
        }

        public void CheckPassword(string password)
        {
            if(String.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Venlig indtast et kodeord");         
        }

        public void CheckRepeatPassword(string pass, string repeatpassword)
        {
            if(!pass.Equals(repeatpassword))
                throw new ArgumentException("kodeordene er ikke de samme");
        }

        public void CheckTlf(int tlf)
        {
      
        }

        #endregion

    }
}
