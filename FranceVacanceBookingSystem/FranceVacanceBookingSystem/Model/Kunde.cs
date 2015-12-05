using System;

namespace FranceVacanceBookingSystem.Model
{
    public class Kunde
    {
        private static int _id = 1;

        #region Instance Fields

        #endregion

        #region Properties
        public string Adress { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Tlf { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public static int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        #endregion

        #region Constructors
        public Kunde(string adress, string email, string name, string tlf,string username,string password)
        {
            CheckName(name);
            CheckAdress(adress);
            CheckEmail(email);
            CheckTlf(tlf);
            _id++;
            Adress = adress;
            Email = email;
            Name = name;
            Tlf = tlf;
            Username = username;
            Password = password;
           
        } 
        #endregion

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
