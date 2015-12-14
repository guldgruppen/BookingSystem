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
    public class ProfilePersistency
    {
        private static string JsonFileProfiles = "ALFAProfilAsJson.dat";

        public static async void SaveProfilesAsJsonAsync(Dictionary<string,string> sh)
        {
            string notesJsonString = JsonConvert.SerializeObject(sh);
            SerializeProfilesFileAsync(notesJsonString, JsonFileProfiles);
        }

        public static async Task<Dictionary<string,string>> LoadProfilesFromJsonAsync()
        {
            string SommerhusJsonString = await DeserializeProfilesFileAsync(JsonFileProfiles);
            if (SommerhusJsonString != null)
                return (Dictionary<string,string>)JsonConvert.DeserializeObject(SommerhusJsonString, typeof(Dictionary<string,string>));
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
