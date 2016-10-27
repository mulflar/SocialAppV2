using Newtonsoft.Json;
using SocialAppV2.VModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppV2.ViewModels
{
    public class ViewModelPlaceReduced
    {
        public int premium = 0;
        public ObservableCollection<Place> PlaceReduced { get; set; }
        public async Task<ObservableCollection<Place>> GetPlaceReduced(double latitude, double longitude)
        {
            ObservableCollection<Place> a = new ObservableCollection<Place>();
            Uri url = new Uri("http://produccion.rl2012alc.com/api/index.php/getGeographicPlaces");
            objetoslistas.getByCoord paquete = new objetoslistas.getByCoord();
            paquete.latitude = latitude;
            paquete.longitude = longitude;
            string respuesta = await metodosJson.jsonPOST(url, paquete);
            var respuestajson = JsonConvert.DeserializeObject<objetoslistas.getgeograficplacesoutput>(respuesta.ToString());
            premium = respuestajson.premiumPlaceID;
            if (respuestajson.error == "")
            {
                if (respuestajson.places != null)
                {
                    foreach (objetoslistas.Place item in respuestajson.places)
                    {
                        Place reduced = new Place(item);
                        if (reduced.placeID == premium)
                        {
                            reduced.grupo = "Destacado";
                            reduced.back = "#FFA89416";
                        }
                        a.Add(reduced);
                    }
                }
                PlaceReduced = a;
            }
            return PlaceReduced;
        }
    }
}
