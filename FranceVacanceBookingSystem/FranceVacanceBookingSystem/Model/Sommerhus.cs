using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Core.AnimationMetrics;

namespace FranceVacanceBookingSystem.Model
{
    public class Sommerhus
    {
        public int AfstandFraVandIKm { get; set; }
        public int AntalBadeværelser { get; set; }
        public int AntalParkeringspladser { get; set; }
        public int AntalSoveværelser { get; set; }
        public string Beliggenhed { get; set; }
        public bool HusdyrTilladt { get; set; }
        public int Pris { get; set; }
        public int Størrelse { get; set; }
        public bool Swimmingpool { get; set; }

        public Sommerhus(int afstandFraVand, int antalBadeværelser, int antalParkeringspladser, int antalSoveværelser, string beliggenhed, bool husdyrTilladt, int pris, int størrelse,bool swimmingpool)
        {
            AfstandFraVandIKm = afstandFraVand;
            AntalBadeværelser = antalBadeværelser;
            AntalParkeringspladser = antalParkeringspladser;
            AntalSoveværelser = antalSoveværelser;
            Beliggenhed = beliggenhed;
            HusdyrTilladt = husdyrTilladt;
            Pris = pris;
            Størrelse = størrelse;
            Swimmingpool = swimmingpool;
        }
    }
}
