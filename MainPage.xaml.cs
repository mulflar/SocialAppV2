using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SocialAppV2.Resources;
using Windows.Devices.Geolocation;
using System.Device.Location;
using System.Threading.Tasks;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Services;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using Microsoft.Phone.Maps.Toolkit;
using Newtonsoft.Json;
using System.Windows.Media;
using Windows.Foundation;
using System.IO.IsolatedStorage;
using SocialAppV2.ViewModels;
using System.Collections;
using System.Windows.Shapes;



    


namespace SocialAppV2
{
    public partial class MainPage : PhoneApplicationPage
    {
        Geolocator geolocator = new Geolocator();
        private GeoCoordinateWatcher _watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
        public static double usuariocordx;
        public static double usuariocordy;
        public static string direccion;
        private GeoCoordinate centro;
        public static ObservableCollection<VModel.Place> placereduced = new ObservableCollection<VModel.Place>();
        ObservableCollection<VModel.Pincho> listapinchos = new ObservableCollection<VModel.Pincho>();
        ObservableCollection<VModel.Pincho> listado = new ObservableCollection<VModel.Pincho>();
        const int kMaxGPSAutoLocation= 20;
        const int DesiredAccuracyInMeters = 50;
        const int checkdist = 70;
        ProgressIndicator prog;
        private IsolatedStorageSettings Settings;
        private ViewModelPinchos vm;
        private ViewModelProfilePreferences vperfil;
        bool rastreador = false;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            Settings = IsolatedStorageSettings.ApplicationSettings;
            intro.checkconexion();
            prog = new ProgressIndicator();
            prog.IsIndeterminate = true;
            prog.IsVisible = false;
            SystemTray.SetProgressIndicator(this, prog);
            vm = new ViewModelPinchos();
            vperfil = new ViewModelProfilePreferences();
            _watcher.PositionChanged += Watcher_PositionChanged;
            iniciamapa();
            peticionperfil();
        }

        //METODOS DE INICIO
        //al navegar a esta pagina "limpia" la ruta de acceso, asi que al darle atras saldra de la aplicacion
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NavigationService.RemoveBackEntry();
            
        }
        //se lanza al presionar el boton atras, si las prefencias estan desplegadas las pliega, si no se sale de la APP
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (vistaPreferencias.Height > 0)
            {
                preferencesout.Begin();
                vperfil.SavePreferences();
                if (vperfil.prefchanged == true)
                    localespincho(usuariocordx, usuariocordy);
                e.Cancel = true;
            }
        }
        //lanzado de las animaciones de las preferencias
        public void launchpreferencias()
        {
            if (vistaPreferencias.Height == 0)
               preferencesin.Begin();
            else if (vistaPreferencias.Height == mapacentral.Height)
            {
                preferencesout.Begin();
                vperfil.SavePreferences();
                if (vperfil.prefchanged == true)
                    localespincho(usuariocordx,usuariocordy);
            }
        }

        //metodo que muestra una appbar diferente en funcion del elemento pivot visible y lanza los metodos asincronos de cada seccion
        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((Pivot)sender).SelectedIndex)
            {
                case 0:
                    ApplicationBar = ((ApplicationBar)Application.Current.Resources["AppBar1"]);
                    pivot1.Opacity = 1;
                    pivot2.Opacity = 0.5;
                    pivot3.Opacity = 0.5;
                    pivot4.Opacity = 0.5;
                    pivot5.Opacity = 0.5;
                    break;
                case 1:
                    ApplicationBar = ((ApplicationBar)Application.Current.Resources["AppBar2"]);
                    pivot1.Opacity = 0.5;
                    pivot2.Opacity = 1;
                    pivot3.Opacity = 0.5;
                    pivot4.Opacity = 0.5;
                    pivot5.Opacity = 0.5;
                    break;
                case 2:
                    ApplicationBar = ((ApplicationBar)Application.Current.Resources["AppBar3"]);
                    pivot1.Opacity = 0.5;
                    pivot2.Opacity = 0.5;
                    pivot3.Opacity = 1;
                    pivot4.Opacity = 0.5;
                    pivot5.Opacity = 0.5;
                    break;
                case 3:
                    ApplicationBar = ((ApplicationBar)Application.Current.Resources["AppBar4"]);
                    pivot1.Opacity = 0.5;
                    pivot2.Opacity = 0.5;
                    pivot3.Opacity = 0.5;
                    pivot4.Opacity = 1;
                    pivot5.Opacity = 0.5;
                    break;
                case 4:
                    ApplicationBar = ((ApplicationBar)Application.Current.Resources["AppBar5"]);
                    pivot1.Opacity = 0.5;
                    pivot2.Opacity = 0.5;
                    pivot3.Opacity = 0.5;
                    pivot4.Opacity = 0.5;
                    pivot5.Opacity = 1;
                    break;
                //case 5:
                //    ApplicationBar = ((ApplicationBar)Application.Current.Resources["AppBar6"]);
                //    break;
            }
        }
        
        //METODOS DE LOCALIZACION Y MAPA
        //busca la ubicacion, situa al usuario y al mapa, e inicia el seguimiento
        private async void iniciamapa()
        {
            geolocator.DesiredAccuracyInMeters = DesiredAccuracyInMeters;
            geolocator.DesiredAccuracy = PositionAccuracy.High;
            try
            {
                prog.IsVisible = true;
                prog.Text = "Obteniendo localización";
                IAsyncOperation<Geoposition> locationTask = null;
                try
                {
                    locationTask = geolocator.GetGeopositionAsync(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(15));
                    Geoposition position = await locationTask;
                    Geocoordinate myGeocoordinate = position.Coordinate;
                    GeoCoordinate myGeoCoordinate = ConvertGeocoordinate(myGeocoordinate);
                    centro = myGeoCoordinate;
                    prog.Text = "Localización obtenida";
                    this.mapacentral.Center = myGeoCoordinate;
                    usuariocordx = position.Coordinate.Latitude;
                    usuariocordy = position.Coordinate.Longitude;
                    setPosition(position.Coordinate.Latitude, position.Coordinate.Longitude);

                    ReverseGeocodeQuery query = new ReverseGeocodeQuery();
                    query.GeoCoordinate = new GeoCoordinate(position.Coordinate.Latitude, position.Coordinate.Longitude);
                    query.QueryCompleted += ReverseGeocodeQuery_QueryCompleted;
                    query.QueryAsync();

                    localespincho(position.Coordinate.Latitude, position.Coordinate.Longitude);
                    Uri url = new Uri("http://produccion.rl2012alc.com/api/index.php/setPosition ");
                    objetoslistas.getByCoord paquete = new objetoslistas.getByCoord();
                    paquete.latitude = position.Coordinate.Latitude;
                    paquete.longitude = position.Coordinate.Longitude;
                    string respuesta = await metodosJson.jsonPOST(url, paquete);
                    if (listapinchos.Count>0)
                    {
                        checklocal();
                    }

                }
                finally
                {
                    if (locationTask != null)
                    {
                        if (locationTask.Status == AsyncStatus.Started)
                            locationTask.Cancel();
                        locationTask.Close();
                    }
                }
            }
            catch (Exception)
            {
                // the app does not have the right capability or the location master switch is off 
                MessageBoxResult result = MessageBox.Show("Location  is disabled in phone settings", "ERROR", MessageBoxButton.OK);
            }
        }
        //adapta la variable posicion a una que el mapa entienda
        public static GeoCoordinate ConvertGeocoordinate(Geocoordinate geocoordinate)
        {
            return new GeoCoordinate
                (
                geocoordinate.Latitude,
                geocoordinate.Longitude,
                geocoordinate.Altitude ?? Double.NaN,
                geocoordinate.Accuracy,
                geocoordinate.AltitudeAccuracy ?? Double.NaN,
                geocoordinate.Speed ?? Double.NaN,
                geocoordinate.Heading ?? Double.NaN
                );
        }
        //metodo que se lanza cada vez que la posicion del gps cambia, almacena la posicion del usuario
        //y lanza una actualizacion del centro del mapa y de la direccion postal
        private void Watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            usuariocordx = e.Position.Location.Latitude;
            usuariocordy = e.Position.Location.Longitude;
            centro = new GeoCoordinate(e.Position.Location.Latitude, e.Position.Location.Longitude);
            mapacentral.SetView(centro, mapacentral.ZoomLevel);
            mapacentral.Layers.Clear();
            localespincho(e.Position.Location.Latitude, e.Position.Location.Longitude);
            MapLayer mapLayer = new MapLayer();
            DrawMapMarker(centro, Colors.Purple, mapLayer);
            mapacentral.Layers.Add(mapLayer);
            
           
            ReverseGeocodeQuery query = new ReverseGeocodeQuery();
            query.GeoCoordinate = new GeoCoordinate(e.Position.Location.Latitude, e.Position.Location.Longitude);
            query.QueryCompleted += ReverseGeocodeQuery_QueryCompleted;
            query.QueryAsync();

        }
        private void DrawMapMarker(GeoCoordinate coordinate, Color color, MapLayer mapLayer)
        {
            // Create a map marker
            Ellipse ellipse = new Ellipse();
            ellipse.Width = 20;
            ellipse.Height = 20;
            ellipse.Fill = new SolidColorBrush(color);

            // Create a MapOverlay and add marker.
            MapOverlay overlay = new MapOverlay();
            overlay.Content = ellipse;
            overlay.GeoCoordinate = new GeoCoordinate(coordinate.Latitude, coordinate.Longitude);
            overlay.PositionOrigin = new System.Windows.Point(0.0, 1.0);
            mapLayer.Add(overlay);
        }
        //averigua la direccion postal
        private void ReverseGeocodeQuery_QueryCompleted(object sender, QueryCompletedEventArgs<IList<MapLocation>> e)
        {
            if (e.Error == null)
            {
                if (e.Result.Count > 0)
                {
                    MapAddress address = e.Result[0].Information.Address;
                    Textodireccion.Text = "Estas en " + address.Street + " " + address.HouseNumber + " " + address.City;
                }
            }
        }
        //metodo que activa o desactiva el seguimiento de posicion
        public void track()
        {
            if (rastreador==false)
            {
                _watcher.Start();
                //mapacentral.Layers.Clear();
                MapLayer mapLayer = new MapLayer();
                DrawMapMarker(centro, Colors.Purple, mapLayer);
                mapacentral.Layers.Add(mapLayer);
                //mapacentral.SetView(centro, mapacentral.ZoomLevel);
                rastreador = true;
            }
            else
            {
                _watcher.Stop();
                mapacentral.Layers.Clear();
                //mapacentral.SetView(centro, mapacentral.ZoomLevel);
                rastreador = false;
            }
        }
        //Se lanza cada vez que el usuario levanta el dedo de mapa y pide pinchos nuevos
        //TO DO: implementar metodo de Dechecking y mover la desactivacion de botones
        private void mapacentral_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            localespincho(mapacentral.Center.Latitude, mapacentral.Center.Longitude);
        }
        private void mapacentral_CenterChanged(object sender, MapCenterChangedEventArgs e)
        {
            //Reservas.IsEnabled = false;
            //pedidos.IsEnabled = false;
            //carta.IsEnabled = false;
            //VIP.IsEnabled = false;
            //listacercanos.Header = "";

        }
        //crea una capa en mapa y bindea la lista a pushpins.
        //Pide localizaciones de pinchos, las adapta al objeto pincho y las almacena en una lista ordenados por distancia
        //tambien crea una lista mas reducida para mostrar sobre el mapa
        public async void localespincho(double latitude, double longitude)
        {
            ObservableCollection<DependencyObject> children = MapExtensions.GetChildren(mapacentral);
            var obj = children.FirstOrDefault(x => x.GetType() == typeof(MapItemsControl)) as MapItemsControl;
            listado = await vm.GetPinchos(latitude, longitude);
            prog.IsVisible = true;
            prog.Text = "Pintando el mundo";
            if (listado != null)
            {
               listapinchos.Clear();
                foreach (VModel.Pincho item in listado)
                {
                    listapinchos.Add(item);

                }
            }
            placereduced = vm.PlaceReduced;
            obj.ItemsSource = listapinchos;
            GeoCoordinate centro = new GeoCoordinate(mapacentral.Center.Latitude, mapacentral.Center.Longitude);
            mapacentral.SetView(centro, mapacentral.ZoomLevel);
            prog.IsVisible = false;
        }
        //comprueba si estas a menos de checkdist metros (70) del primer local de la lista y si es asi te "situa" en el nombre del pincho
        //y hace checking, sino, muestra la direccionm
        public async void checklocal()
        {
            int i = 0;
            while (listado[i].tipo!="place")
            {
                i++;
            }    
            if ((listado[i].distraw) <= (checkdist))
            {
                Textodireccion.Text = "Estas en " + listado[i].name;
                //pide el extended de ese pincho 
                Uri url = new Uri("http://produccion.rl2012alc.com/api/index.php/checkInPlace");
                objetoslistas.checkInPlaceinput paquete = new objetoslistas.checkInPlaceinput();
                paquete.placeID = Convert.ToInt32(listado[i].ID);
                string respuesta = await metodosJson.jsonPOST(url, paquete);
                var respuestajson = JsonConvert.DeserializeObject<objetoslistas.checkInPlaceoutput>(respuesta.ToString());
                if (respuestajson.place != null)
                {
                    VModel.Place_extended local = new VModel.Place_extended(respuestajson.place);
                    //chequea los servicios de ese sitio
                    for (int j = 0; j < local.services.Length; j++)
                    {
                        //|4;eOrder|5;invite|6;acceptPayit|
                        switch (local.services[j])
                        {
                            case 1://1;takeaway
                                goto case 3;
                            case 2://2;bookings
                                Reservas.IsEnabled = true;
                                break;
                            case 3://3;home delivery
                                pedidos.IsEnabled = true;
                                break;
                            case 4:
                                break;
                            case 5:
                                break;
                            case 6:
                                break;
                            case 7://7;menu
                                carta.IsEnabled = true;
                                break;
                        }
                    }
                }
            }
        }
        public async void setPosition(double latitude, double longitude)
        {
            Uri url = new Uri("http://produccion.rl2012alc.com/api/index.php/setPosition");
            objetoslistas.getByCoord paquete = new objetoslistas.getByCoord();
            paquete.latitude = latitude;
            paquete.longitude = longitude;
            string respuesta = await metodosJson.jsonPOST(url, paquete);

        }
        //METODOS DEL PERFIL
        //pide los datos del perfil y los almacena, despues pide que se rellene el pivot perfil
        public async void peticionperfil()
        {
            VModel.Profile perfil = vperfil.GetSavedProfile();
            vistaPerfil.DataContext = perfil; ;
            vistaPreferencias.DataContext = perfil;
            prog.IsVisible = true;
            prog.Text = "Obteniendo perfil";
            perfil = await vperfil.GetDownloadedProfile(false);
            vistaPerfil.DataContext = perfil;
            vistaPreferencias.DataContext = perfil;
            if (perfil.isValidMail == true)
            {
                vistaPerfil.validaremail.Content = "email valido";
                vistaPerfil.validaremail.IsEnabled = false;
            }
            
            vperfil.StoreProfile(perfil);
            prog.IsVisible = false;
        }
        //rellena el pivot del perfil con los datos almacenados
      
        public void cambioperfil()
        {
            vperfil.SaveProfile();
        }
        //recupera un perfil con un email
        public async void recuperaPerfil()
        {
            if(vistaPerfil.password.Text!="")
            {
                Uri url = new Uri("http://www.dual-link.es/webservice/dlink/login ");
                objetoslistas.logininput paquete = new objetoslistas.logininput();
                paquete.user = vistaPerfil.emailrecov.Text;
                paquete.password = vistaPerfil.password.Text;
                string respuesta = await metodosJson.jsonPOST(url, paquete);
                var respuestajson = JsonConvert.DeserializeObject<objetoslistas.loginoutput>(respuesta.ToString());
                if (respuestajson.error == "")
                {
                    IsolatedStorageSettings.ApplicationSettings["userID"] = respuestajson.userID;
                    IsolatedStorageSettings.ApplicationSettings["timestamp"] = 1000000000000;
                    IsolatedStorageSettings.ApplicationSettings.Save();
                }
                VModel.Profile perfil = vperfil.GetSavedProfile();
                perfil = await vperfil.GetDownloadedProfile(false);
                vistaPerfil.DataContext = perfil;
                vistaPreferencias.DataContext = perfil;
                if (perfil.isValidMail == true)
                {
                    vistaPerfil.validaremail.Content = "email valido";
                    vistaPerfil.validaremail.IsEnabled = false;
                }
                vperfil.StoreProfile(perfil);
            }
            
        }


        
        //metodos para los botones de la cabecera de los pivots
        private void pivot1_Tap(object sender, RoutedEventArgs e)
        {
            mainPivot.SelectedIndex = 0;
        }
        private void pivot2_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            mainPivot.SelectedIndex = 1;
        }
        private void pivot3_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            mainPivot.SelectedIndex = 2;
        }
        private void pivot4_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            mainPivot.SelectedIndex = 3;
        }
        private void pivot5_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            mainPivot.SelectedIndex = 4;
        }
       
         
        //private void Pushpin_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        //{
        //    var _ppmodel = sender as Pushpin;
        //    ContextMenu contextMenu =
        //        ContextMenuService.GetContextMenu(_ppmodel);
        //    contextMenu.DataContext = listapinchos.Where
        //        (c => (c.posicion
        //            == _ppmodel.GeoCoordinate)).FirstOrDefault();
        //    if (contextMenu.Parent == null)
        //    {
        //        contextMenu.IsOpen = true;
        //    }
        //}


       
    }
}