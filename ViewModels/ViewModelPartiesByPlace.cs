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
    public class ViewModelPartiesByPlace
    {
        public ObservableCollection<Party> lista { get; set; }
        public async Task<ObservableCollection<Party>> GetDefault(int id)
        {
            ObservableCollection<Party> a = new ObservableCollection<Party>();
            Uri url = new Uri("http://produccion.rl2012alc.com/api/index.php/getPartiesForPlace");
            objetoslistas.getPartiesForPlace paquete = new objetoslistas.getPartiesForPlace();
            paquete.placeID = id;
            string respuesta = await metodosJson.jsonPOST(url, paquete);
            var respuestajson = JsonConvert.DeserializeObject<objetoslistas.getPartiesAndEventsoutput>(respuesta.ToString());
            if (respuestajson.error == "")
            {
                if (respuestajson.parties != null)
                {
                    foreach (objetoslistas.Party item in respuestajson.parties)
                    {
                        Party fiesta = new Party(item);
                        a.Add(fiesta);
                    }
                    // Items to collect
                    lista = a;
                }
            }
            return lista;
        }
    }
}
