using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using SocialAppV2.Resources;
using Newtonsoft.Json;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using SocialAppV2.ViewModels;

namespace SocialAppV2
{
    public partial class intro : PhoneApplicationPage
    {
        static bool isMen;
        private ViewModelProfilePreferences vperfil;
        public intro()
        {
            InitializeComponent();
            checkconexion();
            vperfil = new ViewModelProfilePreferences();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //lanza al cargar la pagina, si tiene exito navega a Main sin que el usuario lo note      
            if (IsolatedStorageSettings.ApplicationSettings.Contains("userID"))
            {
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
            else
            {
                //int id = 2276;
                //PROVISIONAL, SOLO PARA DEBUG
                //IsolatedStorageSettings.ApplicationSettings["userID"] = id;
                //IsolatedStorageSettings.ApplicationSettings["timestamp"] = 1000000000000;
                //IsolatedStorageSettings.ApplicationSettings.Save();
                //NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
              //si no, combrueba la localizacion y comprueba el deviceid
                permisoloc();
                checkdevice();
            }
           
        }
        //comprueba si la id del dispositivo esta registrada en la bd
        public async void checkdevice()
        {
            objetoslistas.checkdeviceidinput paquete = new objetoslistas.checkdeviceidinput();
            Uri url = new Uri("http://produccion.rl2012alc.com/api/index.php/checkDeviceID");
            string paqueteprueba = JsonConvert.SerializeObject(paquete);
            string respuesta = await metodosJson.jsonPOST(url, paquete);
            var respuestajson = JsonConvert.DeserializeObject<objetoslistas.checkdeviceidoutput>(respuesta.ToString());
            if (respuestajson.error == "")
            {
                if (respuestajson.perfil == null)
                {
                    //la id no esta registrada, asi que se queda a la espera de que el usuario elija sexo y pulse al boton
                }
                else
                {
                    //la id esta registrada, si ha llegado a este paso es porque no tiene el userID almacenado pero la uuid si lo esta,
                    //asi que almacenara todos los datos que de el perfil estandar
                    VModel.Profile perfil = new VModel.Profile(respuestajson.perfil, respuestajson.perfil.preferences);
                    vperfil.StoreProfile(perfil);
                    (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                }
            }
            else
            {
                MessageBoxResult result =
                MessageBox.Show(respuestajson.error,
                "ERROR",
                MessageBoxButton.OK);
            }
            
        }
        //manda una peticion para recibir una id de usuario al pulsar el boton, si se ha seleccionado sexo
        private async void create_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                Uri url = new Uri("http://preproduccion.rl2012alc.com/api/index.php/createProfile");
                objetoslistas.createprofileinput paquete = new objetoslistas.createprofileinput();
                
                paquete.sexo = isMen;
               
                string respuesta = await metodosJson.jsonPOST(url, paquete);
                var respuestajson = JsonConvert.DeserializeObject<objetoslistas.createprofileoutput>(respuesta.ToString());
                if (respuestajson.error == "")
                {
                    objetoslistas.Profile perfilprovisional = new objetoslistas.Profile();
                    perfilprovisional.preferences = new objetoslistas.Preferences();
                    perfilprovisional.nickName = Microsoft.Phone.Info.DeviceStatus.DeviceName;
                    perfilprovisional.mail = "email";
                    perfilprovisional.phone = "telefono";
                    perfilprovisional.preferences.likesMen = true;
                    perfilprovisional.preferences.likesWomen = true;
                    perfilprovisional.preferences.minAge = 18;
                    perfilprovisional.preferences.maxAge = 99;
                    perfilprovisional.preferences.placeTypes = new int[9];
                    for (int i = 0; i < 9; i++)
                    {
                        perfilprovisional.preferences.placeTypes[i] = i+1;
                    }
                    perfilprovisional.userID = respuestajson.userID;
                    perfilprovisional.lastUpdate = respuestajson.timestamp;
                    perfilprovisional.isMen = isMen;
                    VModel.Profile perfil = new VModel.Profile(perfilprovisional, perfilprovisional.preferences);
                    vperfil.StoreProfile(perfil);
                    (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBoxResult result =
                    MessageBox.Show(respuestajson.error,
                    "ERROR",
                    MessageBoxButton.OK);
                }
            }
            catch (Exception)
            {
                MessageBox.Show(AppResources.nosexo,
                    AppResources.nosexocab,
                    MessageBoxButton.OK);
            }
        }
        //Comprueba que el usuario haya dado el consentimiento de la localizacion
        public void permisoloc()
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("LocationConsent"))
            {
                // El usuario ya decidio si queria usar la localizacion
                return;
            }
            else
            {
                //el usuario no dio el consentimiento, lo pide y guarda el resultado.
                MessageBoxResult result =
                    MessageBox.Show(AppResources.locconsent,
                    AppResources.loccab,
                    MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = true;
                }
                else
                {
                    IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = false;
                }
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }
        //Comprueba que el telefono tiene conexion a internet, sino avisa y se cierra
        public static void checkconexion()
        {
            bool conexion =  Microsoft.Phone.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            if (conexion == false)
            {
                MessageBox.Show(AppResources.conexion,
                    AppResources.conexioncab,
                    MessageBoxButton.OK);
                Application.Current.Terminate();
            }
        }
        //metodos de los botones de sexo
        private void men_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            isMen = true;
            IsolatedStorageSettings.ApplicationSettings["isMen"] = isMen;
            IsolatedStorageSettings.ApplicationSettings.Save();
            men.Background = new SolidColorBrush(Colors.Gray);
            men.Foreground = new SolidColorBrush(Colors.Black);
            women.Background = new SolidColorBrush(Colors.Transparent);
            women.Foreground = new SolidColorBrush(Colors.White);
        }
        private void women_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            isMen = false;
            IsolatedStorageSettings.ApplicationSettings["isMen"] = isMen;
            IsolatedStorageSettings.ApplicationSettings.Save();
            men.Background = new SolidColorBrush(Colors.Transparent);
            men.Foreground = new SolidColorBrush(Colors.White);
            women.Background = new SolidColorBrush(Colors.Gray);
            women.Foreground = new SolidColorBrush(Colors.Black);
        }
    }
}