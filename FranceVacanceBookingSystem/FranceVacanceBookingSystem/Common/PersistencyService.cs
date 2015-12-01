using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using FranceVacanceBookingSystem.Model;
using FranceVacanceBookingSystem.ViewModel;
using Newtonsoft.Json;

namespace FranceVacanceBookingSystem.Common
{
    //Json.Net er downloaded til projektet via NuGet: Højreklik på projektet -> Manage NuGet Package

    class PersistencyService
    {
        private static string JsonFileName = "ProfilesAsJson.dat";
        private static string SommerHuse = "CottagesAsJson.dat";

        public static async void SaveProfileAsJsonAsync(ObservableCollection<Profil> notes)
        {
            string notesJsonString = JsonConvert.SerializeObject(notes);
            SerializeNotesFileAsync(notesJsonString, JsonFileName);
        }

        public static async void SaveCottageAsJsonAsync(ObservableCollection<SommerhusBeskrivelse> sommerhus)
        {
            string notesJsonString = JsonConvert.SerializeObject(sommerhus);
            SerializeNotesFileAsync(notesJsonString, SommerHuse);
        }

        public static async Task<List<Profil>> LoadNotesFromJsonAsync()
        {
            string notesJsonString = await DeserializeNotesFileAsync(JsonFileName);
            if (notesJsonString != null)
                return (List<Profil>)JsonConvert.DeserializeObject(notesJsonString, typeof(List<Profil>));
            return null;
        }

        public static async Task<List<Sommerhus>> LoadSommerhusFromJsonAsync()
        {
            string sommerhusJsonString = await DeserializeNotesFileAsync(SommerHuse);
            if (sommerhusJsonString != null)
                return (List<Sommerhus>)JsonConvert.DeserializeObject(sommerhusJsonString, typeof(List<Profil>));
            return null;
        }



        private static async void SerializeNotesFileAsync(string notesJsonString, string fileName)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(localFile, notesJsonString);
        }

        private static async void SerializeSommerhusFileAsync(string sommerhusJsonString, string fileName)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(localFile, sommerhusJsonString);
        }


        private static async Task<string> DeserializeNotesFileAsync(string fileName)
        {
            try
            {
                StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return await FileIO.ReadTextAsync(localFile);
            }
            catch (FileNotFoundException ex)
            {
                MessageDialogHelper.Show("Loading for the first time? - Try Add and Save some Notes before trying to Save for the first time", "File not Found");
                return null;
            }
        }

        private static async Task<string> DeserializeSommerhusFileAsync(string fileName)
        {
            try
            {
                StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return await FileIO.ReadTextAsync(localFile);
            }
            catch (FileNotFoundException ex)
            {
                MessageDialogHelper.Show("Loading for the first time? - Try Add and Save some Notes before trying to Save for the first time", "File not Found");
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
