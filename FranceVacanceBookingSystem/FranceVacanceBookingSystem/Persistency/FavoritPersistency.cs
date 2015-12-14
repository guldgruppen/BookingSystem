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
    public class FavoritPersistency
    {
        private static string JsonFileFavorit = "FavoritAsJsonFile.dat";


        public static async void SaveFavoritAsJsonAsync(ObservableCollection<FavoritSommerhuse> sh)
        {
            string kundeJsonString = JsonConvert.SerializeObject(sh);
            SerializeFavoritFileAsync(kundeJsonString, JsonFileFavorit);
        }

        public static async Task<List<FavoritSommerhuse>> LoadFavoritFromJsonAsync()
        {
            string kundeJsonString = await DeserializekunderFileAsync(JsonFileFavorit);
            if (kundeJsonString != null)
                return (List<FavoritSommerhuse>)JsonConvert.DeserializeObject(kundeJsonString, typeof(List<FavoritSommerhuse>));
            return null;
        }



        private static async void SerializeFavoritFileAsync(string kunderJsonString, string fileName)
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

