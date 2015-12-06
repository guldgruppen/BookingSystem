using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FranceVacanceBookingSystem.Model;
using FranceVacanceBookingSystem.Persistency;

namespace FranceVacanceBookingSystem.ViewModel
{
    public class AdminVM
    {
        
        private string _antalKunder = KundeDic.Count.ToString();
        public string AntalKunder
        {
            get { return _antalKunder; }
            set { _antalKunder = value; }
        }
        public static Admin LoginAdmin { get; set; }
        public static Dictionary<int,Kunde> KundeDic { get; set; }

        public AdminVM()
        {
                      
        }

        
    }
}
