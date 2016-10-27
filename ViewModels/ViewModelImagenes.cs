using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppV2.ViewModels
{
    public class ViewModelImagenes
    {
        public ObservableCollection<objetoslistas.Image> lista { get; set; }
        public async Task<ObservableCollection<objetoslistas.Image>> GetDefault(string id, string link)
        {
            ObservableCollection<objetoslistas.Image> a = new ObservableCollection<objetoslistas.Image>();
            Uri url = new Uri(link);
            objetoslistas.getByPlaceID paquete = new objetoslistas.getByPlaceID();
            paquete.placeID = id;
            string respuesta = await metodosJson.jsonPOST(url, paquete);
            var respuestajson = JsonConvert.DeserializeObject<objetoslistas.getPicturesoutput>(respuesta.ToString());
            if (respuestajson.error == "")
            {
                if (respuestajson.images != null)
                {
                    foreach (objetoslistas.Image item in respuestajson.images)
                    {
                        a.Add(item);
                    }
                    // Items to collect
                    lista = a;
                }
            }
            return lista;
        }
    }
}
