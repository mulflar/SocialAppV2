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
    public class ViewModelMainOffer
    {
        public ObservableCollection<Offer> lista { get; set; }
        public async Task<ObservableCollection<Offer>> GetDefault(double latitude, double longitude)
        {
            ObservableCollection<Offer> a = new ObservableCollection<Offer>();
            Uri url = new Uri("http://produccion.rl2012alc.com/api/index.php/getMainOffers");
            objetoslistas.getByCoord paquete = new objetoslistas.getByCoord();
            paquete.latitude = latitude;
            paquete.longitude = longitude;
            string respuesta = await metodosJson.jsonPOST(url, paquete);
            var respuestajson = JsonConvert.DeserializeObject<objetoslistas.getOffersoutput>(respuesta.ToString());
            if (respuestajson.error == "")
            {
                if (respuestajson.offers != null)
                {
                    foreach (objetoslistas.offer item in respuestajson.offers)
                    {
                        Offer oferta = new Offer(item);
                        a.Add(oferta);
                    }
                    // Items to collect
                    lista = a;
                }
            }
            return lista;
        }
    }
}
