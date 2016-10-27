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
    public class ViewModelExtended
    {
        //Extended
        public Place_extended Extendeds { get; set; }
        public async Task<Place_extended> GetDefaultExtendeds(int id)
        {
            Uri url = new Uri("http://produccion.rl2012alc.com/api/index.php/getPlaces");
            objetoslistas.getPlacesinput paquete = new objetoslistas.getPlacesinput();
            paquete.placeIDs = new int[1];
            paquete.placeIDs[0] = id;

            string respuesta = await metodosJson.jsonPOST(url, paquete);
            var respuestajson = JsonConvert.DeserializeObject<objetoslistas.getPlacesoutput>(respuesta.ToString());
            Place_extended local = new Place_extended(respuestajson.place[0]);
            Extendeds = local;
            return local;
        }
    }
}