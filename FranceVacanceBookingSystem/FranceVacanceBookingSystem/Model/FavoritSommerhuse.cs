using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FranceVacanceBookingSystem.Model
{
    public class FavoritSommerhuse
    {
        public Sommerhus FavoritSommerhus { get; set; }
        public Profile FavoritProfile { get; set; }

        public FavoritSommerhuse(Sommerhus favoritSommerhus, Profile favoritProfile)
        {
            FavoritSommerhus = favoritSommerhus;
            FavoritProfile = favoritProfile;
        }


    }
}
