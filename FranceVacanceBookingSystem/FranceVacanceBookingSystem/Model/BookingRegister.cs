using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Automation.Peers;
using FranceVacanceBookingSystem.Common;
using FranceVacanceBookingSystem.Persistency;

namespace FranceVacanceBookingSystem.Model
{
    public class BookingRegister
    {
        public ObservableCollection<Booking> Bookings { get; set; }

        public BookingRegister()
        {
            Bookings = new ObservableCollection<Booking>();
        }


        public void AddBooking(Profile profil, Sommerhus somhus, DateTimeOffset fra, DateTimeOffset til)
        {           
            Bookings.Add(new Booking(profil,somhus,fra,til));
        }


    }
}
