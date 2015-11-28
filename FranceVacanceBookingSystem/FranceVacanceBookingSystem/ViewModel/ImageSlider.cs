using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace FranceVacanceBookingSystem.ViewModel
{
    public class ImageSlider
    {
        public Uri uri {get; set;}

        public int Index { get; set; }

        public DispatcherTimer Timer { get; set; }
        public List<string> images { get; set; }
        public ImageSlider()
        {
            images = new List<string>();
            images.Add("fvimg1.jpg");
            images.Add("fvimg2.jpg");
            images.Add("fvimg3.jpg");
            uri = new Uri("ms-appx:///Assets/"+images[0]);         
        }

        public void StartTimer()
        {
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(1000);
            
            
            Timer.Start();
        }

        private void TimerTick(object send, EventArgs e)
        {
            Index++;
        }



    }
}
