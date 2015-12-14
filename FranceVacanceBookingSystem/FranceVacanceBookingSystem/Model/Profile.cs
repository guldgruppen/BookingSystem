using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core.AnimationMetrics;

namespace FranceVacanceBookingSystem.Model
{
    public class Profile
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Profile(string username, string password)
        {
            
            CheckUsername(username);
            CheckPassword(password);           
            Username = username;
            Password = password;
        }
        public void CheckUsername(string username)
        {
            if (String.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Venlig indtast brugernavn");
            }
        }

        public void CheckPassword(string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Venligst indtast kodeord");
            }
        }
    }
}
