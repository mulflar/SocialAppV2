using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SocialAppV2.VModel
{
    public class Profile : INotifyPropertyChanged
    {
        public int userID { get; set; }
        public bool nickchange = false;
        public bool statuschange = false;
        public bool sexchange = false;
        public bool mapchange = false;
        public bool birthchange = false;

        public bool minagechange = false;
        public bool maxagechange = false;
        public bool radiochange = false;
        public bool likemenchange = false;
        public bool likewomenchange = false;

        public bool placechange = false;


        private string _nickName { get; set; }
        public string nickName
        {
            get
            {
                return _nickName;
            }
            set
            {
                _nickName = value;
                nickchange = true;
                RaisePropertyChanged("nickName");
            }
        }
        private string _mail { get; set; }
        public string mail
        {
            get
            {
                return _mail;
            }
            set
            {
                _mail = value;
                RaisePropertyChanged("mail");
            }
        }
        private string _phone { get; set; }
        public string phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
                RaisePropertyChanged("phone");
            }
        }
        private DateTime _birthDate { get; set; }
        public DateTime birthDate
        {
            get
            {
                return _birthDate;
            }
            set
            {
                _birthDate = value;
                birthchange = true;
                RaisePropertyChanged("birthDate");
            }
        }
        private bool _isMen { get; set; }
        public bool isMen
        {
            get
            {
                return _isMen;
            }
            set
            {
                _isMen = value;
                sexchange = true;
                RaisePropertyChanged("isMen");
            }
        }
        private string _statusMessage { get; set; }
        public string statusMessage
        {
            get
            {
                return _statusMessage;
            }
            set
            {
                _statusMessage = value;
                statuschange = true;
                RaisePropertyChanged("statusMessage");
            }
        }
        private bool _mapVisibility { get; set; }
        public bool mapVisibility
        {
            get
            {
                return _mapVisibility;
            }
            set
            {
                _mapVisibility = value;
                mapchange = true;
                RaisePropertyChanged("mapVisibility");
            }
        }


        private bool _likesMen { get; set; }
        public bool likesMen
        {
            get
            {
                return _likesMen;
            }
            set
            {
                _likesMen = value;
                likemenchange = true;
                RaisePropertyChanged("likesMen");

            }
        }
        private bool _likesWomen { get; set; }
        public bool likesWomen
        {
            get
            {
                return _likesWomen;
            }
            set
            {
                _likesWomen = value;
                likewomenchange = true;
                RaisePropertyChanged("likesWomen");
            }
        }
        private int _minAge { get; set; }
        public int minAge
        {
            get
            {
                return _minAge;
            }
            set
            {
                _minAge = value;
                minagechange = true;
                RaisePropertyChanged("minAge");
            }
        }
        private int _maxAge { get; set; }
        public int maxAge
        {
            get
            {
                return _maxAge;
            }
            set
            {
                _maxAge = value;
                maxagechange = true;
                RaisePropertyChanged("maxAge");
            }
        }
        private int _radio { get; set; }
        public int radio
        {
            get
            {
                return _radio;
            }
            set
            {
                _radio = value;
                radiochange = true;
                RaisePropertyChanged("radio");
            }
        }
        private ObservableCollection<bool> _placeTypes = new ObservableCollection<bool>();
        public ObservableCollection<bool> placeTypes
        {
            get
            {
                return _placeTypes;
            }
            set
            {
                _placeTypes = value;
                RaisePropertyChanged("placeTypes");
            }
        }
        private string[] _eventTypes { get; set; }
        public string[] eventTypes
        {
            get
            {
                return _eventTypes;
            }
            set
            {
                _eventTypes = value;
                RaisePropertyChanged("eventTypes");
            }
        }
   
        public string[] otherImageURLs { get; set; }       
        public int[] favorites { get; set; }
        public int[] parties { get; set; }
        public string[] events { get; set; }
        public int[] vipPlaces { get; set; }
        public string[] vipEvents { get; set; }
        public int[] likePlaces { get; set; }

        public string imageURL { get; set; }
 
        public long lastUpdate { get; set; }
        public bool isValidMail { get; set; }
        public bool isValidPhone { get; set; }
        public string phoneCountryCode { get; set; }

        public Profile(objetoslistas.Profile perfil, objetoslistas.Preferences preferencias)
        {
            userID = perfil.userID;
            _nickName = perfil.nickName;
            _mail = perfil.mail;
            _phone = perfil.phone;
            _birthDate = perfil.birthDate;
            _isMen = perfil.isMen;
            _statusMessage = perfil.statusMessage;
            _mapVisibility = perfil.mapVisibility;
            _likesMen = preferencias.likesMen;
            _likesWomen = preferencias.likesWomen;
            _minAge = preferencias.minAge;
            _maxAge = preferencias.maxAge;
            _radio = preferencias.radio;
            _placeTypes = new ObservableCollection<bool>();

            _placeTypes.CollectionChanged += _placeTypes_CollectionChanged;
            if (preferencias.placeTypes!=null)
            {
                for (int i = 0; i < preferencias.placeTypes.Count<int>(); i++)
                {
                    if (preferencias.placeTypes[i] == i + 1)
                    {
                        _placeTypes.Add(true);
                    }
                    else
                    {
                        _placeTypes.Add(false);
                    }
                }
            }
            
                
            _eventTypes = preferencias.eventTypes;
            otherImageURLs = perfil.otherImageURLs;
            favorites = perfil.favorites;
            parties = perfil.parties;
            events = perfil.events;
            vipPlaces = perfil.vipPlaces;
            vipEvents = perfil.vipEvents;
            likePlaces = perfil.likePlaces;
            imageURL = perfil.imageURL;
            lastUpdate = perfil.lastUpdate;
            isValidMail = perfil.isValidMail;
            isValidPhone = perfil.isValidPhone;
            phoneCountryCode = perfil.phoneCountryCode;
        }

        void _placeTypes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.OldItems!=null)
            {
                placechange = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
     }
}
