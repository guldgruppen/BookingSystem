using System;
using System.Collections.Generic;
using FranceVacanceBookingSystem.Persistency;

namespace FranceVacanceBookingSystem.Model
{
    public class ProfileRegister
    {    
        public Dictionary<string,string> DicProfile { get; set; }     
        public ProfileRegister()
        {                     
            DicProfile = new Dictionary<string, string>();          

        }
              


        public void AddDicProfile(string username, string password)
        {
            DicProfile.Add(username,password);
        }

        public Profile FindDicProfile(string username, string password)
        {
            foreach (var profile in DicProfile)
            {
                if (profile.Key == username && profile.Value == password)
                {
                    return new Profile(username,password);
                }
            }
            throw new NullReferenceException("Profil eksisterer ikke");
        }


    }
}
