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
using Newtonsoft.Json;

namespace FranceVacanceBookingSystem.Persistency
{
    public class SommerhusPersistency
    {
        private static string JsonFileName = "SommerhusAsJson.dat";

        public static async void SaveSommerhusAsJsonAsync(ObservableCollection<Sommerhus> sh)
        {
            string notesJsonString = JsonConvert.SerializeObject(sh);
            SerializeSommerhusFileAsync(notesJsonString, JsonFileName);
        }

        public static async Task<List<Sommerhus>> LoadSommerhuseFromJsonAsync()
        {
            string SommerhusJsonString = await DeserializeSommerhusFileAsync(JsonFileName);
            if (SommerhusJsonString != null)
                return (List<Sommerhus>)JsonConvert.DeserializeObject(SommerhusJsonString, typeof(List<Sommerhus>));
            return null;
        }



        private static async void SerializeSommerhusFileAsync(string SommerhusJsonString, string fileName)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(localFile, SommerhusJsonString);
        }


        private static async Task<string> DeserializeSommerhusFileAsync(string fileName)
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
