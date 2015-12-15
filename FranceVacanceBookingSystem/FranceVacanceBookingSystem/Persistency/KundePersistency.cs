using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using FranceVacanceBookingSystem.Model;

namespace FranceVacanceBookingSystem.Persistency
{
    public class KundePersistency
    {
        private static string JsonFileKunder = "xxxKunderneAsJson.dat";
        

        public static async void SaveKunderAsJsonAsync(ObservableCollection<Kunde> sh)
        {           
            string kundeJsonString = JsonConvert.SerializeObject(sh);
            SerializeKunderFileAsync(kundeJsonString, JsonFileKunder);
        }

        public static async Task<List<Kunde>> LoadKunderFromJsonAsync()
        {
            string kundeJsonString = await DeserializekunderFileAsync(JsonFileKunder);
            if (kundeJsonString != null)
                return (List<Kunde>)JsonConvert.DeserializeObject(kundeJsonString, typeof(List<Kunde>));
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


        private class MessageDialogHelper
        {

            public static async void Show(string content, string title)
            {
                MessageDialog messageDialog = new MessageDialog(content, title);
                await messageDialog.ShowAsync();
            }
        }
    }
}
