using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FranceVacanceBookingSystem.Annotations;
using FranceVacanceBookingSystem.Model;

namespace FranceVacanceBookingSystem.ViewModel
{
    public class OpretProfilHandler : INotifyPropertyChanged
    {
        private ProfilHandler _profilHandler;
        private string _username;
        private string _kodeord;
        private string _navn;
        private string _adresse;
        private int _telefonNummer;
        private string _email;

        public ProfilHandler ProfilHandler
        {
            get { return _profilHandler; }
            set { _profilHandler = value; }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Kodeord
        {
            get { return _kodeord; }
            set { _kodeord = value; }
        }

        public string Navn
        {
            get { return _navn; }
            set { _navn = value; }
        }

        public string Adresse
        {
            get { return _adresse; }
            set { _adresse = value; }
        }

        public int TelefonNummer
        {
            get { return _telefonNummer; }
            set
            {
                CheckTelefonNummer(value);
                _telefonNummer = value;
            }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }


        public void CheckTelefonNummer(int nr)
        {
            if(TelefonNummer < 10000000 || TelefonNummer > 99999999)
                throw new ArgumentException("Venligst indtast et korret telefonnummer");
        }

        

        public void AddProfile()
        {
            ProfilHandler.Profiles.Add(new Profil(Adresse,Email,Navn,Kodeord,Username,TelefonNummer));
        }



        #region PropertyChanged region
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}
