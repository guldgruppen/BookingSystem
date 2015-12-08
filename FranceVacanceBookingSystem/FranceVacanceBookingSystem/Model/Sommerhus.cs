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
        public int AntalBadeværelser { get; set; }
        public int AntalSoveværelser { get; set; }
        public string Beliggenhed { get; set; }
        public bool HusdyrTilladt { get; set; }
        public int Pris { get; set; }
        public int Størrelse { get; set; }
        public bool Swimmingpool { get; set; }

        public Sommerhus(int antalBadeværelser, int antalSoveværelser, string beliggenhed, bool husdyrTilladt, int pris, int størrelse,bool swimmingpool)
        {
            CheckAntalBadeværelser(antalBadeværelser);
            CheckAntalSoveværelser(antalSoveværelser);
            CheckBeliggenhed(beliggenhed);
            CheckPris(pris);
            CheckStørrelse(størrelse);
            AntalBadeværelser = antalBadeværelser;
            AntalSoveværelser = antalSoveværelser;
            Beliggenhed = beliggenhed;
            HusdyrTilladt = husdyrTilladt;
            Pris = pris;
            Størrelse = størrelse;
            Swimmingpool = swimmingpool;
        }

        public void CheckAntalBadeværelser(int antalbadeværelser)
        {
            if(antalbadeværelser.GetType() != typeof(int) || antalbadeværelser > 10 || antalbadeværelser <= 0)
                throw new ArgumentException("Venligst indtast korrekt antal badeværelser");
        }

        public void CheckAntalSoveværelser(int soveværelser)
        {
            if(soveværelser.GetType() != typeof(int) || soveværelser > 10 || soveværelser <= 0)          
                throw new ArgumentException("Venligst indtast korrekt antal soveværelser");
        }

        public void CheckBeliggenhed(string beg)
        {
            if(String.IsNullOrEmpty(beg))
                throw new ArgumentException("Venligst indtast en beliggenhed");
        }

        public void CheckPris(int pris)
        {
            if(pris.GetType() != typeof(int) || pris > 20000 || pris <= 0)
                throw new ArgumentException("Venligst indtast en korrekt pris");
        }

        public void CheckStørrelse(int størrelse)
        {
            if(størrelse.GetType() != typeof(int) || størrelse > 1000 || størrelse <= 0)
                throw new ArgumentException("Venligst indtast en ordentlig størrelse");
        }
        

    }
}
