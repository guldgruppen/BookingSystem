using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FranceVacanceBookingSystem.Model
{
    public class FavoritRegister
    {
        public ObservableCollection<FavoritSommerhuse> FavoritListe { get; set; }

        public FavoritRegister()
        {
            FavoritListe = new ObservableCollection<FavoritSommerhuse>();
        }


        public void Addfavorit(Profile pro,Sommerhus som)
        {
            FavoritListe.Add(new FavoritSommerhuse(som,pro));
        }

    }
}
