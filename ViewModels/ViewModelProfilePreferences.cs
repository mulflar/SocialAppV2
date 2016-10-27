using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialAppV2.VModel;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.IO.IsolatedStorage;
using System.Collections.Specialized;

namespace SocialAppV2.ViewModels
{
    public class ViewModelProfilePreferences
    {
        public Profile Perfil { get; set; }
        private IsolatedStorageSettings Settings = IsolatedStorageSettings.ApplicationSettings;
        public bool prefchanged = false;
        
        public Profile GetSavedProfile()
        {
            objetoslistas.Profile Perfiltem = new objetoslistas.Profile();
            objetoslistas.Preferences PrefTemp = new objetoslistas.Preferences();
            Perfil = new Profile(Perfiltem, PrefTemp);
            //PERFIL
            if ((Settings.Contains("statusMessage")) && ((string)Settings["statusMessage"] != null))
                Perfil.statusMessage = (string)Settings["statusMessage"];
            if ((Settings.Contains("mapVisibility")) && ((bool)Settings["mapVisibility"] == true))
                Perfil.mapVisibility = true;
            if ((Settings.Contains("nickName")) && ((string)Settings["nickName"] != null))
                Perfil.nickName = (string)IsolatedStorageSettings.ApplicationSettings["nickName"];
            if ((Settings.Contains("isMen")) && ((bool)Settings["isMen"] == true))
                Perfil.isMen = true;
            if ((Settings.Contains("birthDate")) && ((DateTime)Settings["birthDate"] != null))
                Perfil.birthDate = (DateTime)IsolatedStorageSettings.ApplicationSettings["birthDate"];
            if ((Settings.Contains("mail")) && ((string)Settings["mail"] != null))
                Perfil.mail = (string)IsolatedStorageSettings.ApplicationSettings["mail"];
            if ((Settings.Contains("phone")) && ((string)Settings["phone"] != null))
                Perfil.phone = (string)IsolatedStorageSettings.ApplicationSettings["phone"];
            if (Settings.Contains("userID"))
                Perfil.userID = (int)Settings["userID"];
            if (Settings.Contains("isValidMail"))
                Perfil.isValidMail = (bool)Settings["isValidMail"];
            if (Settings.Contains("isValidPhone"))
                Perfil.isValidPhone = (bool)Settings["isValidPhone"];
            if ((Settings.Contains("phoneCountryCode")) && ((string)Settings["phoneCountryCode"] != null))
                Perfil.phoneCountryCode = (string)Settings["phoneCountryCode"];
            if ((Settings.Contains("imageURL")) && ((string)Settings["imageURL"] != null))
                Perfil.imageURL = (string)Settings["imageURL"];
            if ((Settings.Contains("otherImageURLs")) && ((string[])Settings["otherImageURLs"] != null))
                Perfil.otherImageURLs = (string[])Settings["otherImageURLs"];
            if ((Settings.Contains("favorites")) && ((int[])Settings["favorites"] != null))
                Perfil.favorites = (int[])Settings["favorites "];
            if ((Settings.Contains("parties")) && ((int[])Settings["parties"] != null))
                Perfil.parties = (int[])Settings["parties"];
            if ((Settings.Contains("events")) && ((string[])Settings["events"] != null))
                Perfil.events = (string[])Settings["events"];
            if ((Settings.Contains("vipPlaces")) && ((int[])Settings["vipPlaces"] != null))
                Perfil.vipPlaces = (int[])Settings["vipPlaces"];
            if ((Settings.Contains("vipEvents")) && ((string[])Settings["vipEvents"] != null))
                Perfil.vipEvents = (string[])Settings["vipEvents"];
            if ((Settings.Contains("likePlaces")) && ((int[])Settings["likePlaces"] != null))
                Perfil.likePlaces = (int[])Settings["likePlaces"];
            //PREFERENCIAS
            if (Settings.Contains("likesMen"))
                Perfil.likesMen = (bool)Settings["likesMen"];
            if (Settings.Contains("likesWomen"))
                Perfil.likesWomen = (bool)Settings["likesWomen"];
            if (Settings.Contains("minAge"))
                Perfil.minAge = (int)Settings["minAge"];
            if (Settings.Contains("maxAge"))
                Perfil.maxAge = (int)Settings["maxAge"];
            if ((Settings.Contains("placeTypes")) && ((ObservableCollection<bool>)Settings["placeTypes"] != null))
                Perfil.placeTypes = (ObservableCollection<bool>)Settings["placeTypes"];
            if ((Settings.Contains("eventTypes")) && ((string[])Settings["eventTypes"] != null))
                Perfil.eventTypes = (string[])Settings["eventTypes"];
            if (Settings.Contains("radio"))
                Perfil.radio = (int)Settings["radio"];
            return Perfil;
        }
        public async Task<Profile> GetDownloadedProfile(bool isValidMail)
        {
            objetoslistas.getprofileinput paquete = new objetoslistas.getprofileinput();
            paquete.isValidMail = isValidMail;
            Uri url = new Uri("http://produccion.rl2012alc.com/api/index.php/getProfile");
            string respuesta = await metodosJson.jsonPOST(url, paquete);
            var respuestajson = JsonConvert.DeserializeObject<objetoslistas.getprofileoutput>(respuesta.ToString());
            if (respuestajson.error == "")
            {
                if (respuestajson.perfil != null)
                {
                    Profile a = new Profile(respuestajson.perfil, respuestajson.perfil.preferences);
                    Perfil = a;
                }
            }
            return Perfil;
        }
        public async void SaveProfile()
        {
            objetoslistas.setProfileinput paquete = new objetoslistas.setProfileinput();
            if (Perfil.nickchange == true)
                paquete.nickName = Perfil.nickName;
            if (Perfil.birthchange == true)
                paquete.birthDate = Perfil.birthDate;
            if (Perfil.statuschange == true)
                paquete.statusMessage = Perfil.statusMessage;
            if (Perfil.sexchange == true)
                paquete.isMen = Perfil.isMen;
            if (Perfil.mapchange == true)
                paquete.mapVisibility = Perfil.mapVisibility;
        
            Uri url = new Uri("http://produccion.rl2012alc.com/api/index.php/setProfile");
            string respuesta = await metodosJson.jsonPOST(url, paquete);
            var respuestajson = JsonConvert.DeserializeObject<objetoslistas.setProfileoutput>(respuesta.ToString());
            if (respuestajson.error == "")
            {
                Settings["timestamp"] = respuestajson.timestamp;
            }
        }
        public async void SavePreferences()
        {
            bool launch = false;
            objetoslistas.setPreferencesinput paquete = new objetoslistas.setPreferencesinput();
            objetoslistas.Preferences preferencias = new objetoslistas.Preferences();

            if (Perfil.likemenchange == true)
            {
                preferencias.likesMen = Perfil.likesMen;
                launch = true;
            }
            if (Perfil.likewomenchange == true)
            {
                preferencias.likesWomen = Perfil.likesWomen;
                launch = true;
            }
            if (Perfil.minagechange == true)
            {
                preferencias.minAge = Perfil.minAge;
                launch = true;
            }
            if (Perfil.maxagechange == true)
            {
                preferencias.maxAge = Perfil.maxAge;
                launch = true;
            }
            if (Perfil.radiochange == true)
            {
                preferencias.radio = Perfil.radio;
                launch = true;
            }
            if (Perfil.placechange == true)
            {
                int[] places = new int[7];
                for (int i = 0; i < 6; i++)
                {
                    if (Perfil.placeTypes[i] == true)
                    {
                        places[i] = i + 1;
                    }
                }
                preferencias.placeTypes = places;
                launch = true;
            }
            paquete.preferencias = preferencias;
            if (launch == true)
            {
                Uri url = new Uri("http://produccion.rl2012alc.com/api/index.php/setPreferences");
                string respuesta = await metodosJson.jsonPOST(url, paquete);
                var paqueteprueba = JsonConvert.SerializeObject(paquete);
                var respuestajson = JsonConvert.DeserializeObject<objetoslistas.setPreferencesoutput>(respuesta.ToString());
                if (respuestajson.error == "")
                {
                    Settings["timestamp"] = respuestajson.timestamp;
                }
                prefchanged = true;
            }
        }

        public void StoreProfile(Profile perfil)
        {
            IsolatedStorageSettings.ApplicationSettings["userID"] = perfil.userID;
            IsolatedStorageSettings.ApplicationSettings["nickName"] = perfil.nickName;
            IsolatedStorageSettings.ApplicationSettings["statusMessage"] = perfil.statusMessage;
            IsolatedStorageSettings.ApplicationSettings["birthDate"] = perfil.birthDate;
            IsolatedStorageSettings.ApplicationSettings["isMen"] = perfil.isMen;
            IsolatedStorageSettings.ApplicationSettings["mapVisibility"] = perfil.mapVisibility;
            IsolatedStorageSettings.ApplicationSettings["timestamp"] = perfil.lastUpdate;
            IsolatedStorageSettings.ApplicationSettings["mail"] = perfil.mail;
            IsolatedStorageSettings.ApplicationSettings["isValidMail"] = perfil.isValidMail;
            IsolatedStorageSettings.ApplicationSettings["phone"] = perfil.phone;
            IsolatedStorageSettings.ApplicationSettings["isValidPhone"] = perfil.isValidPhone;
            IsolatedStorageSettings.ApplicationSettings["phoneCountryCode"] = perfil.phoneCountryCode;
            IsolatedStorageSettings.ApplicationSettings["imageUrl"] = perfil.imageURL;
            IsolatedStorageSettings.ApplicationSettings["otherImageURLs"] = perfil.otherImageURLs;
            IsolatedStorageSettings.ApplicationSettings["favorites"] = perfil.favorites;
            IsolatedStorageSettings.ApplicationSettings["parties"] = perfil.parties;
            IsolatedStorageSettings.ApplicationSettings["events"] = perfil.events;
            IsolatedStorageSettings.ApplicationSettings["vipPlaces"] = perfil.vipPlaces;
            IsolatedStorageSettings.ApplicationSettings["likesMen"] = perfil.likesMen;
            IsolatedStorageSettings.ApplicationSettings["likesWomen"] = perfil.likesWomen;
            IsolatedStorageSettings.ApplicationSettings["minAge"] = perfil.minAge;
            IsolatedStorageSettings.ApplicationSettings["maxAge"] = perfil.maxAge;
            IsolatedStorageSettings.ApplicationSettings["placeTypes"] = perfil.placeTypes;
            IsolatedStorageSettings.ApplicationSettings["evenTypes"] = perfil.eventTypes;
            IsolatedStorageSettings.ApplicationSettings["radio"] = perfil.radio;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }
    }
}
