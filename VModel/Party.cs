using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppV2.VModel
{
    public class Party
    {
        public int partyID { get; set; }
        public string name { get; set; }
        public int numberOfMen { get; set; }
        public int numberOfWomen { get; set; }
        public string logoURL { get; set; }
        public DateTime beginDate { get; set; }
        public double price { get; set; }
        public string placename { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string fecha { get; set; }
        public int distraw { get; set; }
        public GeoCoordinate posicion { get; set; }
        public string distancia { get; set; }

        public Party(objetoslistas.Party party)
        {
            partyID = party.partyID;
            name = party.name;
            numberOfMen = party.numberOfMen;
            numberOfWomen = party.numberOfWomen;
            logoURL = party.logoURL;
            beginDate = party.beginDate;
            DateTime now = DateTime.Now;
            if (beginDate < now)
                fecha = "Fiestas Pasadas";
            else
                fecha = "Proximas";
            price = party.price;
            placename = party.place.name;
            latitude = party.place.longitude;
            longitude = party.place.latitude;
            posicion = new GeoCoordinate(party.place.latitude, party.place.longitude);
            GeoCoordinate localcoord = new GeoCoordinate(party.place.latitude, party.place.longitude);
            GeoCoordinate centroccord = new GeoCoordinate(MainPage.usuariocordx, MainPage.usuariocordy);
            distraw = Convert.ToInt32(centroccord.GetDistanceTo(localcoord));
            if (distraw > 1000)
                distancia = (distraw / 1000).ToString() + " KM";
            else
                distancia = distraw.ToString() + "  M";
        }
    }
    public class Party_Extended : Party
    {
        public List<simpleimage> imageURLs { get; set; }



        public Party_Extended(objetoslistas.Party party):base(party)
        {

            imageURLs = new List<simpleimage>();
            foreach (string item in party.imageURLs)
            {
                simpleimage imagen = new simpleimage();
                imagen.url = item;
                imageURLs.Add(imagen);
            }
        }
    }

}
