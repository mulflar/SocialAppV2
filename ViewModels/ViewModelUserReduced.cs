using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialAppV2.VModel;
using SocialAppV2;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace SocialAppV2.ViewModels
{
    public class ViewModelUserReduced
    {
        public ObservableCollection<userReduced> Usuarios { get; set; }
        public async Task<ObservableCollection<userReduced>> GetNearUsers(double latitude, double longitude)
        {
            if (Usuarios == null)
            {
                Usuarios = await GetDefaultNearUsers(latitude, longitude);
            }
            return Usuarios;
        }

        public async Task<ObservableCollection<userReduced>> GetDefaultNearUsers(double latitude, double longitude)
        {
            ObservableCollection<userReduced> a = new ObservableCollection<userReduced>();
            Uri url = new Uri("http://produccion.rl2012alc.com/api/index.php/nearUsers ");
            objetoslistas.getByCoord paquete = new objetoslistas.getByCoord();
            paquete.latitude = latitude;
            paquete.longitude = longitude;
            string respuesta = await metodosJson.jsonPOST(url, paquete);
            var respuestajson = JsonConvert.DeserializeObject<objetoslistas.getnearUsersoutput>(respuesta.ToString());
            if (respuestajson.error == "")
            {
                if (respuestajson.profile != null)
                {
                    foreach (objetoslistas.user_reduced user in respuestajson.profile)
                    {
                        userReduced usuario = new userReduced(user);
                        a.Add(usuario);
                    }
                }
                Usuarios = a;
            }
            return Usuarios;
        }
        /*public void GetSavedEventReduceds()
        {
            ObservableCollection<PartyandEvent> a = new ObservableCollection<PartyandEvent>();

            foreach (Object o in IsolatedStorageSettings.ApplicationSettings.Values)
            {
                a.Add((PartyandEvent)o);
            }
            PartyandEventsReduced = a;
        }*/

    }
}
