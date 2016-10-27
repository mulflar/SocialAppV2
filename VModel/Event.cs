using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppV2.VModel
{
    public class EventLegend
    {
        public string eventID { get; set; }
        public string name { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string logoURL { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        //ATRIBUTOS CUSTOM
        public string distancia { get; set; }
        public int distraw { get; set; }
        public GeoCoordinate posicion { get; set; }

        public EventLegend(objetoslistas.Event evento)
        {
            eventID = evento.eventID;
            name = evento.name;
            latitude = evento.latitude;
            longitude = evento.longitude;
            logoURL = evento.logoURL;
            address = evento.address;
            phone = evento.phone;


            //CUSTOM
            posicion = new GeoCoordinate(evento.latitude, evento.longitude);
            GeoCoordinate centroccord = new GeoCoordinate(MainPage.usuariocordx, MainPage.usuariocordy);
            distraw = Convert.ToInt32(centroccord.GetDistanceTo(posicion));
            if (distraw > 1000)
                distancia = (distraw / 1000).ToString() + " KM";
            else
                distancia = distraw.ToString() + "  M";
        }
    }
    public class Event : EventLegend
    {
        public int number_of_men { get; set; }
        public int number_of_women { get; set; }
        public string type { get; set; }
        public string subtype { get; set; }
        public DateTime beginDate { get; set; }
        public double minPrice { get; set; }
        public double maxPrice { get; set; }
        //ATRIBUTOS CUSTOM
        public string type_usable { get; set; }
        public string back { get; set; }
        public string grupo { get; set; }

        public Event(objetoslistas.Event evento):base(evento)
        {
            grupo = "Eventos";
           
            number_of_men = evento.numberOfMen;
            number_of_women = evento.numberOfWomen;
            subtype = evento.subtype;
            back = "#FF938E8E";

            if (evento.type == 1)
            {
                type = "Cafetería";
                type_usable = "/images/iconos/cup.png";
            }
            else if (evento.type == 2)
            {
                type = "Pub";
                type_usable = "/images/iconos/appbar.star.png";
            }
            else if (evento.type == 3)//
            {
                type = "Chiringuito";
                type_usable = "/images/iconos/food.png";
            }
            else if (evento.type == 4)
            {
                type = "Beach Club";
                type_usable = "/images/iconos/appbar.star.png";
            }
            else if (evento.type == 5)
            {
                type = "Discoteca";
                type_usable = "/images/iconos/appbar.star.png";
            }
            else if (evento.type == 6)
            {
                type = "Tetería";
                type_usable = "/images/iconos/cup.png";
            }
            else if (evento.type == 7)
            {
                type = "Restaurante";
                type_usable = "/images/iconos/food.cross.png";
            }
            else if (evento.type == 8)
            {
                type = "Ambiente";
                type_usable = "/images/iconos/appbar.star.png";
            }
            else if (evento.type == 9)
            {
                type = "Cerveceria";
                type_usable = "/images/iconos/beer.png";
            }
            else
            {
                type = "Desconocido";
                type_usable = "/images/iconos/appbar.star.png";
            }

           
        }      
    }
}
