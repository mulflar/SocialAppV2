using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppV2.VModel
{

    //Place Reduced
    public class Place
    {
        public int placeID { get; set; }
        public string name { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public int numberOfMen { get; set; }
        public int numberOfWomen { get; set; }
        public int type { get; set; }
        public string subtype { get; set; }
        public string logoURL { get; set; }
        public int numberOfLikePeople { get; set; }
        public bool isSubscriptionActive { get; set; }
        //Atributos Custom
        public string back { get; set; }
        public string user_vip_in_place { get; set; }
        public string user_favourite_place { get; set; }
        public string grupo { get; set; }
        public int distraw { get; set; }
        public GeoCoordinate posicion { get; set; }
        public string distancia { get; set; }
        public string type_usable { get; set; }
        public string type_name { get; set; }

        public Place(objetoslistas.Place pincho)
        {
            placeID = pincho.placeID;
            name = pincho.name;
            latitude = pincho.latitude;
            longitude = pincho.longitude;
            numberOfMen = pincho.numberOfMen;
            numberOfWomen = pincho.numberOfWomen;
            type = pincho.type;
            subtype = pincho.subtype;
            logoURL = pincho.logoURL;
            numberOfLikePeople = pincho.numberOfLikePeople;
            isSubscriptionActive = pincho.subscriptionActive;
            //ADAPTACIONES
            back = "#FF938E8E";

            if (pincho.subscriptionActive == true)
                grupo = "Locales SocialAppV2";
            else
                grupo = "Otros Locales";

            if (IsolatedStorageSettings.ApplicationSettings.Contains("favorites"))
            {
                if ((int[])IsolatedStorageSettings.ApplicationSettings["favorites"] != null)
                {
                    int[] favorites = (int[])IsolatedStorageSettings.ApplicationSettings["favorites"];
                    foreach (int item in favorites)
                        if (item == pincho.placeID)
                            user_favourite_place = "/Images/iconos/heart.png";
                        else
                            user_favourite_place = "/Images/iconos/heart.outline.png";
                }
                else
                    user_favourite_place = "/Images/iconos/heart.outline.png";
            }
            else
                user_favourite_place = "/Images/iconos/heart.outline.png";

            if (IsolatedStorageSettings.ApplicationSettings.Contains("vipPlaces"))
            {
                if ((int[])IsolatedStorageSettings.ApplicationSettings["vipPlaces"] != null)
                {
                    int[] vip = (int[])IsolatedStorageSettings.ApplicationSettings["vipPlaces"];
                    foreach (int item in vip)
                        if (item == pincho.placeID)
                            user_vip_in_place = "/Images/iconos/vip.png";
                        else
                            user_vip_in_place = "/Images/iconos/vip.wh.png";
                }
                else
                    user_vip_in_place = "/Images/iconos/vip.wh.png";
            }
            else
                user_vip_in_place = "/Images/iconos/vip.wh.png";

            switch (pincho.type)
            {
                case 1:
                    type_name = "Cafetería";
                    type_usable = "/images/iconos/cup.png";
                    break;
                case 2:
                    type_name = "Pub";
                    type_usable = "/images/iconos/appbar.star.png";
                    break;
                case 3:
                     type_name = "Chiringuito";
                    type_usable = "/images/iconos/food.png";
                    break;
                case 4:
                    type_name = "Beach Club";
                    type_usable = "/images/iconos/appbar.star.png";
                    break;
                case 5:
                    type_name = "Discoteca";
                    type_usable = "/images/iconos/appbar.star.png";
                    break;
                case 6:
                    type_name = "Tetería";
                    type_usable = "/images/iconos/cup.png";
                    break;
                case 7:
                    type_name = "Restaurante";
                    type_usable = "/images/iconos/food.cross.png";
                    break;
                case 8:
                    type_name = "Ambiente";
                    type_usable = "/images/iconos/appbar.star.png";
                    break;
                case 9:
                    type_name = "Cerveceria";
                    type_usable = "/images/iconos/beer.png";
                    break;
                default:
                    type_name = "Desconocido";
                    type_usable = "/images/iconos/appbar.star.png";
                    break;
            }
            posicion = new GeoCoordinate(pincho.latitude, pincho.longitude);
            GeoCoordinate localcoord = new GeoCoordinate(pincho.latitude, pincho.longitude);
            GeoCoordinate centroccord = new GeoCoordinate(MainPage.usuariocordx, MainPage.usuariocordy);
            distraw = Convert.ToInt32(centroccord.GetDistanceTo(localcoord));
            if (distraw > 1000)
                distancia = (distraw / 1000).ToString() + " KM";
            else
                distancia = distraw.ToString() + "  M";
        }
    }
    public class PlaceLegend : Place
    {
        public string address { get; set; }
        public string phone { get; set; }
        public PlaceLegend(objetoslistas.Place place)
            : base(place)
        {
            address = place.address;
            phone = place.phone;
        }
    }
    public class Place_extended : Place
    {
        public string info { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string hoursOfOpeningText { get; set; }
        public string messageForMen { get; set; }
        public string messageForWomen { get; set; }
        public int[] services { get; set; }
        public List<simpleimage> imageURLs { get; set; }

        public Place_extended(objetoslistas.Place place)
            : base(place)
        {
            info = place.info;
            address = place.address;
            phone = place.phone;
            hoursOfOpeningText = place.hoursOfOpeningText;
            messageForMen = place.messageForMen;
            messageForWomen = place.messageForWomen;
            services = place.services;
            imageURLs = new List<simpleimage>();
            foreach (string item in place.imageURLs)
            {
                simpleimage imagen = new simpleimage();
                imagen.url = item;
                imageURLs.Add(imagen);
            }
            
        }
    }

}
