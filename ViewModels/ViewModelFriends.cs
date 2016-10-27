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
    public class ViewModelFriends
    {
        public ObservableCollection<userReduced> Amigos { get; set; }
        public async Task<ObservableCollection<userReduced>> GetNearFriends(double latitude, double longitude)
        {
            if (Amigos == null)
            {
                Amigos = await GetDefaultNearFriends(latitude, longitude);
            }
            return Amigos;
        }

        public async Task<ObservableCollection<userReduced>> GetDefaultNearFriends(double latitude, double longitude)
        {
            ObservableCollection<userReduced> a = new ObservableCollection<userReduced>();
            Uri url = new Uri("http://produccion.rl2012alc.com/api/index.php/myFriends");
            objetoslistas.myFriendsinput paquete = new objetoslistas.myFriendsinput();
            string respuesta = await metodosJson.jsonPOST(url, paquete);
            var respuestajson = JsonConvert.DeserializeObject<objetoslistas.myFriendsoutput>(respuesta.ToString());
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
                Amigos = a;
            }
            return Amigos;
        }
    }
}
