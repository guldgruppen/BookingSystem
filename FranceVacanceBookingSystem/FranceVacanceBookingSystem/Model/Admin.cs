using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FranceVacanceBookingSystem.Model
{
    public class Admin
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public Admin(string username, string password, string name, string email)
        {
            Username = username;
            Password = password;
            Name = name;
            Email = email;
        }

        public Admin(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
