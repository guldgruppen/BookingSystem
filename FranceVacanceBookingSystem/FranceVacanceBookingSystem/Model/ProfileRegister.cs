using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FranceVacanceBookingSystem.Model
{
    public class ProfileRegister
    {     
        public Dictionary<string,string> DicProfile { get; set; } 
        public ProfileRegister()
        {           
            DicProfile = new Dictionary<string, string>();
            DicProfile.Add("bo", "bo");
            DicProfile.Add("jan","jan");         
        }
              

        public void AddDicProfile(string username, string password)
        {
            DicProfile.Add(username,password);
        }

        public Profile FindDicProfile(string username, string password)
        {
            foreach (var dics in DicProfile)
            {
                if (dics.Key == username && dics.Value == password)
                {
                    return new Profile(dics.Key,dics.Value);
                }                              
            }
            throw new NullReferenceException("Profil eksisterer ikke");
        }

    }
}
