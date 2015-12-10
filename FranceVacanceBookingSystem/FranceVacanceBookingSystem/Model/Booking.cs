using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FranceVacanceBookingSystem.Model
{
    public class Booking
    {
        public Profile BookingProfile { get; set; }
        public Sommerhus BookingSommerhus { get; set; }
        public DateTimeOffset BookingFra { get; set; }
        public DateTimeOffset BookingTil { get; set; }

        public Booking(Profile bookingProfile, Sommerhus bookingSommerhus, DateTimeOffset bookingFra, DateTimeOffset bookingTil)
        {
            BookingProfile = bookingProfile;
            BookingSommerhus = bookingSommerhus;
            BookingFra = bookingFra;
            BookingTil = bookingTil;
        }

        public override string ToString()
        {
            return "Profile: " + BookingProfile.Username + "Sommerhus beliggenhed: " + BookingSommerhus.Beliggenhed +
                   "Booking fra: " + BookingFra.ToString("d") + "Booking til: " + BookingTil.ToString("d");
        }
    }
}
