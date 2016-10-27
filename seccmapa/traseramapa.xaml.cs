using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SocialAppV2.ViewModels;
using System.Collections.ObjectModel;

namespace SocialAppV2
{
    public partial class traseramapa : PhoneApplicationPage
    {
        private ViewModelPlaceReduced vplace;
        private ViewModelPartyEvent vpe;
        private ViewModelUserReduced vpuser;
        private ViewModelMainOffer voffer;
        ProgressIndicator prog;
        ObservableCollection<VModel.Place> listado = new ObservableCollection<VModel.Place>();
        ObservableCollection<VModel.PartyandEvent> listadopande = new ObservableCollection<VModel.PartyandEvent>();
        ObservableCollection<VModel.Offer> listadooffer = new ObservableCollection<VModel.Offer>();
        ObservableCollection<VModel.userReduced> listadousers = new ObservableCollection<VModel.userReduced>();
        public traseramapa()
        {
            InitializeComponent();
            vplace = new ViewModelPlaceReduced();
            vpe = new ViewModelPartyEvent();
            vpuser = new ViewModelUserReduced();
            voffer = new ViewModelMainOffer();
            prog = new ProgressIndicator();
            prog.IsIndeterminate = true;
            prog.IsVisible = false;
            SystemTray.SetProgressIndicator(this, prog);
            if (MainPage.placereduced != null && MainPage.placereduced.Count > 0)
                listareduced.DataContext = GetItemGroups(MainPage.placereduced, c => c.grupo);
            datos();
        }
        private async void datos()
        {
            prog.IsVisible = true;
            prog.Text = "Obteniendo datos";
            listado = await vplace.GetPlaceReduced(MainPage.usuariocordx, MainPage.usuariocordy);
            listadopande = await vpe.GetPartyandEvents(MainPage.usuariocordx, MainPage.usuariocordy, vplace.premium);
            listadousers = await vpuser.GetDefaultNearUsers(MainPage.usuariocordx, MainPage.usuariocordy);
            listadooffer = await voffer.GetDefault(MainPage.usuariocordx, MainPage.usuariocordy);
            if (listado != null && listado.Count > 0)
            {
                listareduced.DataContext = GetItemGroups(listado.OrderBy(o => o.distraw).ToList(), c => c.grupo);
            }
            if (vpe.PartyandEventsReduced.Count > 0)
            {
                listaeventparty.DataContext = GetItemGroups(vpe.PartyandEventsReduced.OrderBy(o => o.distraw).ToList(), c => c.grupo);
            }
            if (vpuser.Usuarios.Count>0)
            {
                listagente.DataContext = GetItemGroups(vpuser.Usuarios.OrderBy(o => o.distraw).ToList(), c => c.isFriend);
            }
            if (voffer.lista!=null && voffer.lista.Count>0)
            {
                listaofertas.DataContext = GetItemGroups(voffer.lista.OrderBy(o => o.place.distraw).ToList(), c => c.place.grupo);
            }
            
            prog.Text = "Datos Obtenidos";
            prog.IsVisible = false;
        }

        //Vale para todos los longlist
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

        private void TraseraPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((Pivot)sender).SelectedIndex)
            {
                case 0:
                    ApplicationBar = ((ApplicationBar)Application.Current.Resources["traseraAppBar1"]);
                    break;
                case 1:
                    ApplicationBar = ((ApplicationBar)Application.Current.Resources["traseraAppBar2"]);
                    break;
                case 2:
                    ApplicationBar = ((ApplicationBar)Application.Current.Resources["traseraAppBar3"]);
                    break;
                case 3:
                    ApplicationBar = ((ApplicationBar)Application.Current.Resources["traseraAppBar4"]);
                    break;
            }
        }

        //cambio de barra al cambio de pivot
       
    }
}