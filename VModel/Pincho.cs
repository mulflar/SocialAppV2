using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppV2.VModel
{
    public class Pincho
    {
        public string ID { get; set; }
        public string name { get; set; }
        public string tipo { get; set; }
        public int distraw { get; set; }
        public GeoCoordinate posicion { get; set; }
        public string type_usable { get; set; }
        public double numberOfMen{ get; set; }
        public double numberOfWomen { get; set; }

        public Pincho(objetoslistas.Place place)
        {
            ID = place.placeID.ToString();
            name = place.name;
            tipo = "place";
            int totalparty = place.numberOfMen + place.numberOfWomen;
            if (totalparty == 0)
            {
                numberOfMen = 0.5;
                numberOfWomen = 0.5;
            }
            else
            {
                numberOfMen = (float)((float)(place.numberOfMen * 100) / totalparty) / 100;
                numberOfWomen = (float)((float)(place.numberOfWomen * 100) / totalparty) / 100;
            }
            switch (place.type)
            {
                case 1:
                    type_usable = "/images/iconos/cup.png";
                    break;
                case 2:
                    type_usable = "/images/iconos/appbar.star.png";
                    break;
                case 3:
                    type_usable = "/images/iconos/food.png";
                    break;
                case 4:
                    type_usable = "/images/iconos/appbar.star.png";
                    break;
                case 5:
                    type_usable = "/images/iconos/appbar.star.png";
                    break;
                case 6:
                    type_usable = "/images/iconos/cup.png";
                    break;
                case 7:
                    type_usable = "/images/iconos/food.cross.png";
                    break;
                case 8:
                    type_usable = "/images/iconos/appbar.star.png";
                    break;
                case 9:
                    type_usable = "/images/iconos/beer.png";
                    break;
                default:
                    type_usable = "/images/iconos/appbar.star.png";
                    break;
            }
            posicion = new GeoCoordinate(place.latitude, place.longitude);
            GeoCoordinate localcoord = new GeoCoordinate(place.latitude, place.longitude);
            GeoCoordinate centroccord = new GeoCoordinate(MainPage.usuariocordx, MainPage.usuariocordy);
            distraw = Convert.ToInt32(centroccord.GetDistanceTo(localcoord));
        }
        public Pincho(objetoslistas.Event evento)
        {
            ID = evento.eventID;
            name = evento.name;
            int totalparty = evento.numberOfMen + evento.numberOfWomen;
            if (totalparty == 0)
            {
                numberOfMen = 0.5;
                numberOfWomen = 0.5;
            }
            else
            {
                numberOfMen = (float)((float)(evento.numberOfMen * 100) / totalparty) / 100;
                numberOfWomen = (float)((float)(evento.numberOfWomen * 100) / totalparty) / 100;
            }
            tipo = "event";
            switch (evento.type)
            {
                case 1:
                    type_usable = "/images/iconos/cup.png";
                    break;
                case 2:
                    type_usable = "/images/iconos/appbar.star.png";
                    break;
                case 3:
                    type_usable = "/images/iconos/food.png";
                    break;
                case 4:
                    type_usable = "/images/iconos/appbar.star.png";
                    break;
                case 5:
                    type_usable = "/images/iconos/appbar.star.png";
                    break;
                case 6:
                    type_usable = "/images/iconos/cup.png";
                    break;
                case 7:
                    type_usable = "/images/iconos/food.cross.png";
                    break;
                case 8:
                    type_usable = "/images/iconos/appbar.star.png";
                    break;
                default:
                    type_usable = "/images/iconos/appbar.star.png";
                    break;
            }
            posicion = new GeoCoordinate(evento.latitude, evento.longitude);
            GeoCoordinate localcoord = new GeoCoordinate(evento.latitude, evento.longitude);
            GeoCoordinate centroccord = new GeoCoordinate(MainPage.usuariocordx, MainPage.usuariocordy);
            distraw = Convert.ToInt32(centroccord.GetDistanceTo(localcoord));
        }
        public Pincho(objetoslistas.Hotspot hotspot)
        {
            ID = "HotSpot";
            name = "HotSpot";
            tipo = "HotSpot";
            type_usable = "/images/iconos/appbar.star.png";
            int totalparty = hotspot.numberOfMen + hotspot.numberOfWomen;
            if (totalparty == 0)
            {
                numberOfMen = 0.5;
                numberOfWomen = 0.5;
            }
            else
            {
                numberOfMen = (float)((float)(hotspot.numberOfMen * 100) / totalparty) / 100;
                numberOfWomen = (float)((float)(hotspot.numberOfWomen * 100) / totalparty) / 100;
            }
            posicion = new GeoCoordinate(hotspot.latitude, hotspot.longitude);
            GeoCoordinate localcoord = new GeoCoordinate(hotspot.latitude, hotspot.longitude);
            GeoCoordinate centroccord = new GeoCoordinate(MainPage.usuariocordx, MainPage.usuariocordy);
            distraw = Convert.ToInt32(centroccord.GetDistanceTo(localcoord));


        }
    }
}
