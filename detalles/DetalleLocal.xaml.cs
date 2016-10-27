using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;
using SocialAppV2.ViewModels;
using SocialAppV2.VModel;

namespace SocialAppV2
{
    public partial class DetalleLocal : PhoneApplicationPage
    {
        private ViewModelExtended vextended;
        private ViewModelPlaceOffers vofertas;
        private ViewModelPartiesByPlace vparties;
        private ViewModelImagenes vimages;
        public DetalleLocal()
        {
            InitializeComponent();
            vextended = new ViewModelExtended();
            vofertas = new ViewModelPlaceOffers();
            vparties = new ViewModelPartiesByPlace();
            vimages = new ViewModelImagenes();
        }
        protected async override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            int placeID;
            string idstring;
            base.OnNavigatedTo(e);
            NavigationContext.QueryString.TryGetValue("id", out idstring);
            placeID = Int32.Parse(idstring);
            
            Place_extended local = await vextended.GetDefaultExtendeds(placeID);
            detalleLocal.DataContext = local;
            //Ofertas
            var ofertas = await vofertas.GetDefault(local.placeID, true);
            foreach (Offer item in ofertas)
            {
                item.place = local;
            }
            ofertaslocal.DataContext = GetItemGroups(ofertas.OrderBy(o => o.place.distraw).ToList(), c => c.place.name);
            //Fiestas
            var fiestas = await vparties.GetDefault(local.placeID);
            fiestaslocal.DataContext = GetItemGroups(fiestas.OrderBy(o => o.beginDate.Date).ToList(), c => c.fecha);
            string url = "http://produccion.rl2012alc.com/api/index.php/getPlacePicturesFromUsers";
            imagenes.DataContext = await vimages.GetDefault(local.placeID.ToString(), url);
            
        }
        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((Pivot)sender).SelectedIndex)
            {
                case 0:
                    ApplicationBar = ((ApplicationBar)Application.Current.Resources["localAppBar1"]);
                    break;
                case 1:
                    ApplicationBar = ((ApplicationBar)Application.Current.Resources["localAppBar2"]);
                    break;
                case 2:
                    ApplicationBar = ((ApplicationBar)Application.Current.Resources["localAppBar3"]);
                    break;
            }
        }
        private static List<Group<T>> GetItemGroups<T>(IEnumerable<T> itemList, Func<T, string> getKeyFunc)
        {
            IEnumerable<Group<T>> groupList = from item in itemList
                                              group item by getKeyFunc(item) into g
                                              orderby g.Key
                                              select new Group<T>(g.Key, g);

            return groupList.ToList();
        }
        public class Group<T> : List<T>
        {
            public Group(string name, IEnumerable<T> items)
                : base(items)
            {
                this.Title = name;
            }

            public string Title
            {
                get;
                set;
            }
        }
    }
}