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
        private bool _swimmingpool;
        private bool _husdyrTilladt;
        public int AntalBadeværelser { get; set; }
        public int AntalSoveværelser { get; set; }
        public string Beliggenhed { get; set; }

        public bool HusdyrTilladt
        {
            get { return _husdyrTilladt; }
            set
            {
                _husdyrTilladt = value;
                if (value)
                    HusdyrIText = "Ja";
                else
                {
                    HusdyrIText = "Nej";
                }
            }
        }

        public int Pris { get; set; }
        public int Størrelse { get; set; }
        public bool Swimmingpool
        {
            get { return _swimmingpool; }
            set
            {
                _swimmingpool = value;
                if (value)
                    SwimmingPoolIText = "Ja";
                else
                {
                    SwimmingPoolIText = "Nej";
                }
            }
        }
        public int AntalPersoner { get; set; }
        public string HusdyrIText { get; set; }
        public string SwimmingPoolIText { get; set; }

        public Sommerhus(int antalPersoner, int antalBadeværelser, int antalSoveværelser, string beliggenhed, bool husdyrTilladt, int pris, int størrelse,bool swimmingpool)
        {
            CheckAntalBadeværelser(antalBadeværelser);
            CheckAntalSoveværelser(antalSoveværelser);
            CheckBeliggenhed(beliggenhed);
            CheckPris(pris);
            CheckStørrelse(størrelse);
            AntalPersoner = antalPersoner;
            AntalBadeværelser = antalBadeværelser;
            AntalSoveværelser = antalSoveværelser;
            Beliggenhed = beliggenhed;
            HusdyrTilladt = husdyrTilladt;
            Pris = pris;
            Størrelse = størrelse;
            Swimmingpool = swimmingpool;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Sommerhus))
                return false;
            return AntalBadeværelser == ((Sommerhus) obj).AntalBadeværelser &&
                   AntalSoveværelser == ((Sommerhus) obj).AntalSoveværelser && Beliggenhed ==
                   ((Sommerhus) obj).Beliggenhed && Pris == ((Sommerhus) obj).Pris &&
                   Størrelse == ((Sommerhus) obj).Størrelse;
            //SKAL OGSÅ INDEHOLDE HUSDYR OG SWIMMINGPOOL
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
