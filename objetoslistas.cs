using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Globalization;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SocialAppV2
{
    public class objetoslistas
    {
        //metodos serializables de llamada y recepcion
        //checkdevice
        [DataContract]
        public class checkdeviceidinput
        {
            [DataMember(Name = "deviceID")]
            public string deviceID = "475cb5cd-6066-422e-a4cc-2461e5b35ec7";//(string)IsolatedStorageSettings.ApplicationSettings["Uuid"];
        }
        [DataContract]
        public class checkdeviceidoutput
        {
            [DataMember(Name = "error")]
            public string error { get; set; }
            [DataMember(Name = "profile")]
            public Profile perfil { get; set; }
        }
        //createprofile
        [DataContract]
        public class createprofileinput
        {
            [DataMember(Name = "deviceID")]
            public string textodeviceid = "475cb5cd-6066-422e-a4cc-2461e5b35ec7";//(string)IsolatedStorageSettings.ApplicationSettings["Uuid"];
            [DataMember(Name = "isMen")]
            public bool sexo { get; set; }
            [DataMember(Name = "os")]
            public int numos = 3;
            [DataMember(Name = "locale")]
            public string textolocale = CultureInfo.CurrentUICulture.Name.ToString();
        }
        [DataContract]
        public class createprofileoutput
        {
            [DataMember(Name = "error")]
            public string error { get; set; }
            [DataMember(Name = "userID")]
            public int userID { get; set; }
            [DataMember(Name = "timestamp")]
            public long timestamp { get; set; }
        }
        //getprofile
        [DataContract]
        public class getprofileinput
        {
            [DataMember(Name = "userID")]
            public int userID = (int)IsolatedStorageSettings.ApplicationSettings["userID"];
            [DataMember(Name = "isValidMail")]
            public bool isValidMail { get; set; }
            [DataMember(Name = "timestamp")]
            public long timestamp = (long)IsolatedStorageSettings.ApplicationSettings["timestamp"];
        }
        [DataContract]
        public class getprofileoutput
        {
            [DataMember(Name = "error")]
            public string error { get; set; }
            [DataMember(Name = "profile")]
            public Profile perfil { get; set; }
        }
        //register
        [DataContract]
        public class registerinput
        {
            [DataMember(Name = "userID")]
            public int userID = (int)IsolatedStorageSettings.ApplicationSettings["userID"];
            [DataMember(Name = "user")]
            public string user{ get; set; }
        }
        public class registeroutput
        {
            [DataMember(Name = "error")]
            public string error { get; set; }
            [DataMember(Name = "alreadyRegistered")]
            public bool alreadyRegistered { get; set; }
        }
        //registrar telefono
        [DataContract]
        public class sendSMSinput
        {
            [DataMember(Name = "userID")]
            public int userID = (int)IsolatedStorageSettings.ApplicationSettings["userID"];
            [DataMember(Name = "phoneNumber")]
            public string phoneNumber { get; set; }
            [DataMember(Name = "phoneCountryCode")]
            public string phoneCountryCode = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        }
        public class sendSMSoutput
        {
            [DataMember(Name = "error")]
            public string error { get; set; }
            [DataMember(Name = "alreadyRegistered")]
            public bool alreadyRegistered { get; set; }
        }
        //Metodo para pedir datos por Coordenadas y user id
        [DataContract]
        public class getByCoord
        {
            [DataMember(Name = "userID")]
            public int userID = (int)IsolatedStorageSettings.ApplicationSettings["userID"];
            [DataMember(Name = "latitude")]
            public double latitude { get; set; }
            [DataMember(Name = "longitude")]
            public double longitude { get; set; }
        }
        //Metodo para pedir datos sobre una Id de place
        [DataContract]
        public class getByPlaceID
        {
            [DataMember (Name="placeID")]
            public string placeID { get; set; }
        }
        //getPlacePicturesFromUsers
        public class getPicturesoutput
        {
            [DataMember(Name = "images")]
            public List<Image> images { get; set; }
            [DataMember(Name = "error")]
            public string error { get; set; }
        }
        //getgeograficplaces       
        [DataContract]
        public class getgeograficplacesoutput
        {
            [DataMember(Name = "places", IsRequired = false)]
            public List<Place> places { get; set; }
            [DataMember(Name = "hotspots", IsRequired = false)]
            public List<Hotspot> hotspots { get; set; }
            [DataMember(Name = "events", IsRequired = false)]
            public List<Event> events { get; set; }
            [DataMember(Name = "premiumPlaceID", IsRequired = false)]
            public int premiumPlaceID { get; set; }
            [DataMember(Name = "error")]
            public string error { get; set; }
        }
        //getPartiesAndEvents
        [DataContract]
        public class getPartiesAndEventsoutput
        {
            [DataMember(Name = "events", IsRequired=false)]
	        public List<Event> events { get; set; }
            [DataMember(Name = "parties", IsRequired=false)]
            public List<Party> parties { get; set; }
            [DataMember(Name = "error")]
            public string error { get; set; }
        }
        //Parties
        [DataContract]
        public class getPartiesForPlace
        {
            [DataMember(Name = "placeID")]
            public int placeID { get; set; }
            [DataMember(Name = "userID")]
            public int userID = (int)IsolatedStorageSettings.ApplicationSettings["userID"];
        }
        //Offers
        [DataContract]
        public class getOffersByPlaceinput
        {
            [DataMember(Name = "placeID")]
            public int placeID { get; set; }
            [DataMember(Name = "main")]
            public bool main { get; set; }
            [DataMember(Name = "userID")]
            public int userID = (int)IsolatedStorageSettings.ApplicationSettings["userID"];
        }
        [DataContract]
        public class getOffersoutput
        {
            [DataMember(Name = "offers")]
            public List<offer> offers { get; set; }
            [DataMember(Name = "error")]
            public string error { get; set; }
        }
        //nearUsers
        [DataContract]
        public class getnearUsersoutput
        {
            [DataMember(Name = "profile")]
            public List<user_reduced> profile { get; set; }
            [DataMember(Name = "error")]
            public string error { get; set; }
        }
        //myFriends
        [DataContract]
        public class myFriendsinput
        {
            [DataMember(Name = "userID")]
            public int userID = (int)IsolatedStorageSettings.ApplicationSettings["userID"];
        }
        [DataContract]
        public class myFriendsoutput
        {
            [DataMember(Name = "profile")]
            public List<user_reduced> profile { get; set; }
            [DataMember(Name = "error")]
            public string error { get; set; }
        }
        //checkInPlace
        [DataContract]
        public class checkInPlaceinput
        {
            [DataMember(Name = "placeID")]
            public int placeID { get; set; }
            [DataMember(Name = "userID", IsRequired = true)]
            public int userID = (int)IsolatedStorageSettings.ApplicationSettings["userID"];
        }
        [DataContract]
        public class checkInPlaceoutput
        {
            [DataMember(Name = "place", IsRequired = false)]
            public Place place { get; set; }
            [DataMember(Name = "error")]
            public string error { get; set; }
        }
        //getplaces
        [DataContract]
        public class getPlacesinput
        {
            [DataMember(Name = "placeIDs")]
            public int[] placeIDs { get; set; }

        }
        [DataContract]
        public class getPlacesoutput
        {
            [DataMember(Name = "places", IsRequired = false)]
            public List<Place> place { get; set; }
            [DataMember(Name = "error")]
            public string error { get; set; }

        }
        //setprofile
        [DataContract]
        public class setProfileinput
        {
            [DataMember(Name = "userID", IsRequired = true)]
            public int userID = (int)IsolatedStorageSettings.ApplicationSettings["userID"];
            [DataMember(Name = "nickName", IsRequired = false, EmitDefaultValue = false)]
            public string nickName { get; set; }
            [DataMember(Name = "statusMessage", IsRequired = false, EmitDefaultValue = false)]
            public string statusMessage { get; set; }
            [DataMember(Name = "mail", IsRequired = false, EmitDefaultValue = false)]
            public string mail { get; set; }
            [DataMember(Name = "isMen", IsRequired = false, EmitDefaultValue = false)]
            public bool isMen { get; set; }
            [DataMember(Name = "mapVisibility")]
            public bool mapVisibility { get; set; }
            [DataMember(Name = "locale", EmitDefaultValue = false)]
            public string locale = CultureInfo.CurrentUICulture.Name.ToString();
            [DataMember(Name = "phoneCountryCode", IsRequired = false, EmitDefaultValue = false)]
            public string countrycode = CultureInfo.CurrentUICulture.Name.ToString();
            [DataMember(Name = "phone", IsRequired = false, EmitDefaultValue = false)]
            public string phone { get; set; }
            [DataMember(Name = "birthDate", IsRequired = false, EmitDefaultValue = false)]
            public DateTime birthDate { get; set; }
        }
        [DataContract]
        public class setProfileoutput
        {
            [DataMember(Name = "error")]
            public string error { get; set; }
            [DataMember(Name = "timestamp")]
            public long timestamp { get; set; }
        }
        //setpreferences
        [DataContract]
        public class setPreferencesinput
        {
            [DataMember(Name = "userID")]
            public int userID = (int)IsolatedStorageSettings.ApplicationSettings["userID"];
            [DataMember(Name = "preferences")]
            public Preferences preferencias { get; set; }
        }
        [DataContract]
        public class setPreferencesoutput
        {
            [DataMember(Name = "error")]
            public string error { get; set; }
            [DataMember(Name = "timestamp")]
            public long timestamp { get; set; }
        }
       


        //SERVICIOS DUALINK
        public class logininput
        {
            [DataMember(Name = "user")]
            public string user { get; set; }
            [DataMember(Name = "password")]
            public string password { get; set; }
        }
        public class loginoutput
        {
            [DataMember(Name = "error")]
            public string error { get; set; }
            [DataMember(Name = "userID")]
            public int userID { get; set; }
        }

        //objetos serializables construidos en metodos anteriores
        //checkdeviceid - getprofile
        [DataContract]
        public class Profile
        {
            [DataMember(Name = "otherImageURLs")]
            public string[] otherImageURLs { get; set; }
            [DataMember(Name = "preferences")]
            public Preferences preferences { get; set; }
            [DataMember(Name = "favorites")]
            public int[] favorites { get; set; }
            [DataMember(Name = "parties")]
            public int[] parties { get; set; }
            [DataMember(Name = "events")]
            public string[] events { get; set; }
            [DataMember(Name = "vipPlaces")]
            public int[] vipPlaces { get; set; }
            [DataMember(Name = "vipEvents")]
            public string[] vipEvents { get; set; }
            [DataMember(Name ="likePlaces")]
            public int[] likePlaces { get; set; }
            [DataMember(Name = "userID")]
            public int userID { get; set; }
            [DataMember(Name = "mail")]
            public string mail { get; set; }
            [DataMember(Name = "phone")]
            public string phone { get; set; }
            [DataMember(Name = "birthDate")]
            public DateTime birthDate { get; set; }
            [DataMember(Name = "isMen")]
            public bool isMen { get; set; }
            [DataMember(Name = "imageURL")]
            public string imageURL { get; set; }
            [DataMember(Name = "nickName")]
            public string nickName { get; set; }
            [DataMember(Name = "statusMessage")]
            public string statusMessage { get; set; }
            [DataMember(Name = "mapVisibility")]
            public bool mapVisibility { get; set; }
            [DataMember(Name = "lastupdate")]
            public long lastUpdate { get; set; }
            [DataMember(Name = "isValidMail")]
            public bool isValidMail { get; set; }
            [DataMember(Name = "isValidPhone")]
            public bool isValidPhone { get; set; }
            [DataMember(Name = "phoneCountryCode")]
            public string phoneCountryCode { get; set; }
        }
        [DataContract]
        public class Preferences
        {
           [DataMember(Name = "likesMen")]
           public bool likesMen { get; set; }
           [DataMember(Name = "likesWomen")]
           public bool likesWomen { get; set; }
           [DataMember(Name = "minAge", IsRequired = false, EmitDefaultValue = false)]
           public int minAge { get; set; }
           [DataMember(Name = "maxAge", IsRequired = false, EmitDefaultValue = false)]
           public int maxAge { get; set; }
           [DataMember(Name = "radio", IsRequired = false, EmitDefaultValue = false)]
           public int radio { get; set; }
           [DataMember(Name = "placeTypes", IsRequired = false, EmitDefaultValue = false)]
           public int[] placeTypes { get; set; }
           [DataMember(Name = "eventTypes", IsRequired = false, EmitDefaultValue = false)]
           public string[] eventTypes { get; set; }
         }
        //myFriends
        [DataContract]
        public class friend
        {
            [DataMember(Name = "userID")]
            public string userID { get; set; }
            [DataMember(Name = "name")]
            public string name { get; set; }
            [DataMember(Name = "statusMessage")]
            public string statusMessage { get; set; }
            [DataMember(Name = "birthDate")]
            public DateTime birthDate { get; set; }
            [DataMember(Name = "isMen")]
            public bool isMen { get; set; }
            [DataMember(Name = "otherImageURLs")]
            public string[] otherImageURLs { get; set; }
            [DataMember(Name = "imageURL")]
            public string imageURL { get; set; }
            [DataMember(Name = "latitude")]
            public double latitude { get; set; }
            [DataMember(Name = "longitude")]
            public double longitude { get; set; }
            [DataMember(Name = "isFriend")]
            public bool isFriend { get; set; }
            [DataMember(Name = "parties")]
            public int[] parties { get; set; }
            [DataMember(Name="online")]
            public bool online{get;set;}
            [DataMember(Name="placeName")]
            public string placeName { get; set; }
            [DataMember(Name="mapVisibility")]
            public bool mapVisibility { get; set; }
            public string distancia
            {
                get
                {
                    var localcoord = new GeoCoordinate(latitude, longitude);
                    var centroccord = new GeoCoordinate(MainPage.usuariocordx, MainPage.usuariocordy);
                    distancia = (centroccord.GetDistanceTo(localcoord) / 1000).ToString() + " KM";
                    return distancia;
                }
                set { }
            }
        }
        [DataContract]
        public class offer
        {
            [DataMember(Name = "place", IsRequired = false)]
            public Place place { get; set; }
            [DataMember(Name = "main")]
            public bool main { get; set; }
            [DataMember(Name = "offerID")]
            public int offerID { get; set; }
            [DataMember(Name = "imageURL")]
            public string imageURL { get; set; }
            [DataMember(Name = "active")]
            public bool active { get; set; }
            [DataMember(Name = "name")]
            public string name { get; set; }
            [DataMember(Name = "info")]
            public string info { get; set; }
            [DataMember(Name = "dateFrom", IsRequired=false)]
            public DateTime dateFrom { get; set; }
            [DataMember(Name = "dateTo", IsRequired=false)]
            public DateTime dateTo { get; set; }
            [DataMember(Name = "promoText")]
            public string promoText { get; set; }
        }
        [DataContract]
        public class user_reduced
        {
            [DataMember(Name = "userID")]
            public string userID { get; set; }
            [DataMember(Name = "nickName")]
            public string nickName { get; set; }
            [DataMember(Name = "statusMessage")]
            public string statusMessage { get; set; }
            [DataMember(Name = "birthDate")]
            public DateTime birthDate { get; set; }
            [DataMember(Name = "isMen")]
            public bool isMen { get; set; }
            [DataMember(Name = "imageURL")]
            public string imageURL { get; set; }
            [DataMember(Name = "latitude")]
            public double latitude { get; set; }
            [DataMember(Name = "longitude")]
            public double longitude { get; set; }
            [DataMember(Name = "isFriend")]
            public bool isFriend { get; set; }
        }
        [DataContract]
        public class Hotspot
        {
            [DataMember(Name = "latitude")]
            public double latitude { get; set; }
            [DataMember(Name = "longitude")]
            public double longitude { get; set; }
            [DataMember(Name = "numberOfMen")]
            public int numberOfMen { get; set; }
            [DataMember(Name = "numberOfWomen")]
            public int numberOfWomen { get; set; }
        }
        [DataContract]
        public class Place
        {
                [DataMember (Name="placeID")]
                public int placeID {get;set;}
                [DataMember(Name = "name")]
                public string name { get; set; }
                [DataMember(Name = "latitude")]
                public double latitude { get; set; }
                [DataMember(Name = "longitude")]
                public double longitude { get; set; }
                [DataMember(Name = "numberOfMen")]
                public int numberOfMen { get; set; }
                [DataMember(Name = "numberOfWomen")]
                public int numberOfWomen { get; set; }
                [DataMember(Name = "type")]
                public int type { get; set; }
                [DataMember(Name = "subtype")]
                public string subtype { get; set; }
                [DataMember(Name = "subtypeID", IsRequired=false)]
                public int subtypeID { get; set; }
                [DataMember(Name = "logoURL")]
                public string logoURL { get; set; }
                [DataMember(Name = "numberOfLikePeople")]
                public int numberOfLikePeople { get; set; }
                [DataMember(Name = "subscriptionActive")]
                public bool subscriptionActive { get; set; }
                [DataMember(Name = "info",IsRequired=false)]
                public string info { get; set; }
                [DataMember(Name = "address", IsRequired = false)]
                public string address { get; set; }
                [DataMember(Name = "phone", IsRequired = false)]
                public string phone { get; set; }
                [DataMember(Name = "hoursOfOpeningText", IsRequired = false)]
                public string hoursOfOpeningText { get; set; }
                [DataMember(Name = "messageForMen", IsRequired = false)]
                public string messageForMen { get; set; }
                [DataMember(Name = "messageForWomen", IsRequired = false)]
                public string messageForWomen { get; set; }
                [DataMember(Name = "services", IsRequired = false)]
                public int[] services {get;set;}
                [DataMember(Name = "imageURLs", IsRequired = false)]
                public string[] imageURLs {get;set;}
        }
        [DataContract]
        public class PlaceImage
        {
                [DataMember (Name="imageDate")]
                public DateTime imageDate {get;set;}
                [DataMember (Name="imagePlaceID")]
                public int imagePlaceID {get;set;}
                [DataMember (Name="profileImageURL")]
                public string profileImageURL {get;set;}
                [DataMember (Name="urlString")]
                public string urlString {get;set;}
                [DataMember (Name="userName")]
                public string userNameL {get;set;}
                [DataMember (Name="userID")]
                public int userID {get;set;} 
        }
        [DataContract]
        public class Event
        {
            [DataMember(Name = "eventID")]
            public string eventID { get; set; }
            [DataMember(Name = "name")]
            public string name { get; set; }
            [DataMember(Name = "latitude")]
            public double latitude { get; set; }
            [DataMember(Name = "longitude")]
            public double longitude { get; set; }
            [DataMember(Name = "phone", IsRequired = false)]
            public string phone { get; set; }
            [DataMember(Name = "address", IsRequired = false)]
            public string address { get; set; }
            [DataMember(Name = "info", IsRequired = false)]
            public string info { get; set; }
            [DataMember(Name = "numberOfMen", IsRequired=false)]
            public int numberOfMen { get; set; }
            [DataMember(Name = "numberOfWomen", IsRequired = false)]
            public int numberOfWomen { get; set; }
            [DataMember(Name = "type", IsRequired = false)]
            public int type { get; set; }
            [DataMember(Name = "subtype", IsRequired = false)]
            public string subtype { get; set; }
            [DataMember(Name = "logoURL", IsRequired = false)]
            public string logoURL { get; set; }
            [DataMember(Name = "imagesURLs", IsRequired = false)]
            public string[] imagesURLs { get; set; }
            [DataMember(Name = "beginDate", IsRequired = false)]
            public DateTime beginDate {get;set;}
            [DataMember(Name = "endDate", IsRequired = false)]
            public DateTime endDate { get; set; }
            [DataMember(Name = "minPrice", IsRequired = false)]
            public double minPrice { get; set; }
            [DataMember(Name = "maxPrice", IsRequired = false)]
            public double maxPrice { get; set; }
            [DataMember(Name = "services", IsRequired = false)]
            public int[] services { get; set; }
        }
        [DataContract]
        public class Party
        {
            [DataMember(Name = "partyID")]
            public int partyID { get; set; }
            [DataMember(Name = "name")]
            public string name { get; set; }
            [DataMember(Name = "numberOfMen")]
            public int numberOfMen { get; set; }
            [DataMember(Name = "numberOfWomen")]
            public int numberOfWomen { get; set; }
            [DataMember(Name = "logoURL", IsRequired = false)]
            public string logoURL { get; set; }
            [DataMember(Name = "beginDate")]
            public DateTime beginDate { get; set; }
            [DataMember(Name = "price")]
            public double price { get; set; }
            [DataMember(Name = "place")]
            public Place place { get; set; }

        }
        [DataContract]
        public class order
        {
            [DataMember(Name = "Status")]
            public int status { get; set; }
            [DataMember(Name = "orderID")]
            public int orderID { get; set; }
            [DataMember(Name = "type")]
            public int type { get; set; }
            [DataMember(Name = "countryCode")]
            public string countryCode { get; set; }
            [DataMember(Name = "estimatedReadyDateTime")]
            public DateTime estimatedReadyDateTime { get; set; }
            [DataMember(Name = "readyDateTime")]
            public DateTime readyDateTime { get; set; }
        }
        [DataContract]
        public class PlaceLegend
        {
            [DataMember(Name = "placeID")]
            public int placeID { get; set; }
            [DataMember(Name = "name")]
            public string name { get; set; }
            [DataMember(Name = "latitude")]
            public double latitude { get; set; }
            [DataMember(Name = "longitude")]
            public double longitude { get; set; }
            [DataMember(Name = "type")]
            public int type { get; set; }
            [DataMember(Name = "logoURL")]
            public string logoURL { get; set; }
            [DataMember(Name = "address")]
            public string address { get; set; }
            [DataMember(Name = "phone")]
            public string phone { get; set; }

        }
        [DataContract]
        public class Image
        {
            [DataMember(Name = "imageURL")]
            public string imageURL { get; set; }
            [DataMember(Name = "userID")]
            public int userID { get; set; }
            [DataMember(Name = "username")]
            public string username { get; set; }
            [DataMember(Name = "uploadDate")]
            public DateTime uploadDate { get; set; }
        }



        //grabador de preferences en isolated
        public static void savePreferences(Preferences preferences)
        {
            
            IsolatedStorageSettings.ApplicationSettings.Save();
        }
    }
}
