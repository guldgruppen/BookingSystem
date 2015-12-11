using System;
using System.Collections.Generic;

namespace FranceVacanceBookingSystem.Model
{
    public class ProfileRegister
    {
        public List<Profile> Profiles { get; set; }     
        public ProfileRegister()
        {           
            Profiles = new List<Profile>()
            {
                new Profile("bob","bob"),
                new Profile("john","john")
            };           
        }
              
        public void AddProfile(string username, string password)
        {
            foreach (Profile profil in Profiles)
            {
                if(profil.Username == username)
                    throw new ArgumentException("Brugernavn eksisterer i forvejen,  indtast et andet brugernavn");
                
            }
            Profiles.Add(new Profile(username, password));


        }        
        public Profile FindProfile(string username, string password)
        {
            foreach (var profile in Profiles)
            {
                if (profile.Username == username && profile.Password == password)
                {
                    return profile;
                }
            }
            throw new NullReferenceException("Profil eksisterer ikke");
        }


    }
}
