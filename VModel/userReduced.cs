using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppV2.VModel
{
    public class userReduced
    {
        public string userID { get; set; }
        public string nickName { get; set; }
        public string statusMessage { get; set; }
        public int años { get; set; }
        public string isMen { get; set; }
        public string imageURL { get; set; }
        public string distancia { get; set; }
        public double distraw { get; set; }
        public string isFriend { get; set; }

        public userReduced (objetoslistas.user_reduced user)
        {
            userID = user.userID;
            nickName = user.nickName;
            statusMessage = user.statusMessage;
            años = DateTime.Now.Year - user.birthDate.Year;
            if (user.isMen == true)
            {
                isMen = "/Images/iconos/icon.gender.male.png";
            }
            else
            {
                isMen = "/Images/iconos/icon.gender.female.png";
            }
            GeoCoordinate localcoord = new GeoCoordinate(user.latitude, user.longitude);
            GeoCoordinate centroccord = new GeoCoordinate(MainPage.usuariocordx, MainPage.usuariocordy);
            distraw = Convert.ToInt32(centroccord.GetDistanceTo(localcoord));
            if (distraw > 1000)
            {
                distancia = (distraw / 1000).ToString() + " KM";
            }
            else
            {
                distancia = distraw.ToString() + "  M";
            }
            if (user.isFriend == true)
            {
                isFriend = "Amigos";
            }
            else
            {
                isFriend = "Gente Cercana";
            }
        }
    }
}
