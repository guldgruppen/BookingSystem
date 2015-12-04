using System;
using System.Collections.Generic;

namespace FranceVacanceBookingSystem.Model
{
    public class ProfileRegister
    {
        public List<Profile> Profiles { get; set; }
        public KundeRegister KundeRegister { get; set; }
        public ProfileRegister()
        {
            KundeRegister = new KundeRegister();
            Profiles = new List<Profile>()
            {
                new Profile("bob","bob"),
                new Profile("john","john")
            };           
        }
              
        public void AddProfile(string username, string password, string repeatPassword,string adresse, string email, string navn, string tlf)
        {
            Profiles.Add(new Profile(username,password));
            KundeRegister.AddKunde(username,password,repeatPassword,adresse,email,navn,tlf);
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
