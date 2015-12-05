using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        private static string JsonFileKunder = "KunderAsJson.dat";

        public static async void SaveKunderAsJsonAsync(Dictionary<int,Kunde> sh)
        {
            string kundeJsonString = JsonConvert.SerializeObject(sh);
            SerializeKunderFileAsync(kundeJsonString, JsonFileKunder);
        }

        public static async Task<Dictionary<int,Kunde>> LoadKunderFromJsonAsync()
        {
            string kundeJsonString = await DeserializekunderFileAsync(JsonFileKunder);
            if (kundeJsonString != null)
                return (Dictionary<int,Kunde>)JsonConvert.DeserializeObject(kundeJsonString, typeof(Dictionary<int,Kunde>));
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
