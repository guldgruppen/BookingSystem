using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FranceVacanceBookingSystem.Model
{
    public class AdminRegister
    {
        public List<Admin> Admins { get; set; }

        public AdminRegister()
        {
            Admins = new List<Admin>();
            Admins.Add(new Admin("kasper","kasper"));
            Admins.Add(new Admin("ricki", "ricki"));

        }

        public Admin FindAdmin(string username, string password)
        {
            foreach (Admin admin in Admins)
            {
                if (admin.Username == username && admin.Password == password)
                {
                    return admin;
                }                
            }
            throw new NullReferenceException("Administrator findes ikke");
        }
    }
}
