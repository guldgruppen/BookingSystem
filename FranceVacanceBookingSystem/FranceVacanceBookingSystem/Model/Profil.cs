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

       

        #endregion

        #region Properties

        public string Adresse { get; set; }

        public int Postnummer { get; set; }

        public int Alder { get; set; }

        public string Email { get; set; }

        public string Navn { get; set; }

        public string Password { get; set; }

        public string RepeatPassword { get; set; }

        public string Username { get; set; }

        public string TelefonNummer { get; set; }

        #endregion

        #region Constructors

        public Profil()
        {

        }

        public Profil(string adresse, string email, string navn, string password, string repeatPassword, string username, string telefonNummer)
        {
            CheckNavn(navn);
            CheckEmail(email);
            CheckAdresse(adresse);
            CheckTlf(telefonNummer);
            CheckUsername(username);
            CheckPassword(password);
            CheckRepeatPassword(password,repeatPassword);
            
            Adresse = adresse;
            Email = email;
            Navn = navn;
            Password = password;
            Username = username;
            TelefonNummer = telefonNummer;
        }

        public Profil(string username, string password)
        {
            Username = username;
            Password = password;
        }
       #endregion
       
        #region CheckInformationRegion
        public void CheckUsername(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Venlig indtast et korrekt brugernavn");
        }

        public void CheckPassword(string password)
        {
            if (String.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Venlig indtast et kodeord");
        }

        public void CheckRepeatPassword(string pass, string repeatpassword)
        {
            if (!pass.Equals(repeatpassword))
                throw new ArgumentException("kodeordene er ikke de samme");
        }

        public void CheckTlf(string tlf)
        {
            if (String.IsNullOrWhiteSpace(tlf) || tlf.Length != 8)
                throw new ArgumentException("Venlig indtast et korret telefon nummer");
        }

        public void CheckNavn(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Venlist indtast et ordentlig navn");
            }
        }

        public void CheckAdresse(string adresse)
        {
            if (String.IsNullOrWhiteSpace(adresse))
                throw new ArgumentException("Venligst indtast en korrekt adresse");
        }
        public void CheckEmail(string email)
        {
            if(String.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Venligst indtast en korrekt email");
        }


        #endregion



    }
}
