using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using SocialAppV2.VModel;
using SocialAppV2;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace SocialAppV2.ViewModels
{
    public class ViewModelPinchos
    {
        //Reduced
        public int premium = 0;
        public ObservableCollection<Pincho> Reduceds { get; set; }
        public ObservableCollection<Place> PlaceReduced { get; set; }
        public async Task<ObservableCollection<Pincho>> GetPinchos(double latitude, double longitude)
        {
            ObservableCollection<Pincho> a = new ObservableCollection<Pincho>();
            ObservableCollection<Place> b = new ObservableCollection<Place>();
            Uri url = new Uri("http://produccion.rl2012alc.com/api/index.php/getGeographicPlaces");
            objetoslistas.getByCoord paquete = new objetoslistas.getByCoord();
            paquete.latitude = latitude;
            paquete.longitude = longitude;
            string respuesta = await metodosJson.jsonPOST(url, paquete);
            var respuestajson = JsonConvert.DeserializeObject<objetoslistas.getgeograficplacesoutput>(respuesta.ToString());
            premium = respuestajson.premiumPlaceID;
            if (respuestajson.error == "")
            {
                if (respuestajson.places!= null)
                {
                    foreach (objetoslistas.Place item in respuestajson.places)
                    {
                        Place reduced = new Place(item);
                        if (reduced.placeID == premium)
                        {
                            reduced.grupo = "Destacado";
                            reduced.back = "#FFA89416";
                        }
                        b.Add(reduced);
                        Pincho place = new Pincho(item);
                        a.Add(place);
                    }
                }
                if (respuestajson.events!=null)
                {
		             foreach (objetoslistas.Event evento in respuestajson.events)
                    {
                        Pincho eventored = new Pincho(evento);
                        a.Add(eventored);
                    }
                }
                if(respuestajson.hotspots!=null)
                {
                    foreach (objetoslistas.Hotspot hotspot in respuestajson.hotspots)
                    {
                        Pincho hot = new Pincho(hotspot);
                        a.Add(hot);
                    }
                }
                Reduceds = a;
                PlaceReduced = b;
            }
            return Reduceds;
        }
        public void GetSavedPinchos()
        {
            ObservableCollection<Pincho> a = new ObservableCollection<Pincho>();

            foreach (Object o in IsolatedStorageSettings.ApplicationSettings.Values)
            {
                a.Add((Pincho)o);
            }
            Reduceds = a;
        }
    }
}
