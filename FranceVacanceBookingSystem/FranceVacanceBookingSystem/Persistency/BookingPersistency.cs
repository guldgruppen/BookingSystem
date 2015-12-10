using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using FranceVacanceBookingSystem.Model;
using Newtonsoft.Json;

namespace FranceVacanceBookingSystem.Persistency
{
    public class BookingPersistency
    {
        private static string JsonFileBooking = "BBookAsJsonFile.dat";


        public static async void SaveBookingAsJsonAsync(ObservableCollection<Booking> sh)
        {
            string kundeJsonString = JsonConvert.SerializeObject(sh);
            SerializeKunderFileAsync(kundeJsonString, JsonFileBooking);
        }

        public static async Task<List<Booking>> LoadBookingFromJsonAsync()
        {
            string kundeJsonString = await DeserializekunderFileAsync(JsonFileBooking);
            if (kundeJsonString != null)
                return (List<Booking>)JsonConvert.DeserializeObject(kundeJsonString, typeof(List<Booking>));
            return null;
        }



        private static async void SerializeKunderFileAsync(string kunderJsonString, string fileName)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(localFile, kunderJsonString);
        }


        private static async Task<string> DeserializekunderFileAsync(string fileName)
        {
            try
            {
                StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return await FileIO.ReadTextAsync(localFile);
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

    }
}
