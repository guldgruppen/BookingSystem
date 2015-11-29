using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FranceVacanceBookingSystem.View;
using FranceVacanceBookingSystem.ViewModel;
using FranceVacanceBookingSystem.Annotations;
using FranceVacanceBookingSystem.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FranceVacanceBookingSystem.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
       
        public Login()
        {
            this.InitializeComponent();
            
        }
        // DELETE WHEN YOU CAN FIX IT!
        private void button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (MainSystem));
        }

        //DELETE WHEN YOU CAN FIX IT!
        private void button1_Copy_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (OpretProfil));
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ProfilHandler p = new ProfilHandler();
            int c = p.Profiles.Count;
            MessageDialog dia = new MessageDialog(c.ToString());
            dia.ShowAsync();
        }
    }
}
