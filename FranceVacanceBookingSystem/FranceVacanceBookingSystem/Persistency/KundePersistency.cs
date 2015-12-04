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

        public static async void SaveProfilesAsJsonAsync(List<Profile> sh)
        {
            string notesJsonString = JsonConvert.SerializeObject(sh);
            SerializeProfilesFileAsync(notesJsonString, JsonFileKunder);
        }

        public static async Task<List<Profile>> LoadProfilesFromJsonAsync()
        {
            string SommerhusJsonString = await DeserializeProfilesFileAsync(JsonFileKunder);
            if (SommerhusJsonString != null)
                return (List<Profile>)JsonConvert.DeserializeObject(SommerhusJsonString, typeof(List<Profile>));
            return null;
        }



        private static async void SerializeProfilesFileAsync(string profilJsonString, string fileName)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(localFile, profilJsonString);
        }


        private static async Task<string> DeserializeProfilesFileAsync(string fileName)
        {
            try
            {
                StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return await FileIO.ReadTextAsync(localFile);
            }
            catch (FileNotFoundException ex)
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
