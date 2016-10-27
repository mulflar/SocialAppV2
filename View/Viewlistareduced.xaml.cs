using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace SocialAppV2.View
{
    public partial class Viewlistareduced : UserControl
    {
        public Viewlistareduced()
        {
            InitializeComponent();
        }
        private void longListLocales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             //si la seleccion es null (no hay nada elegido) no hace nada
            if (longListLocales.SelectedItem == null)
                return;

             //Navega a una pagina con id = placeID
            VModel.Place data = (sender as LongListSelector).SelectedItem as VModel.Place;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/detalles/DetalleLocal.xaml?id=" + data.placeID, UriKind.Relative));

             //Vuelve la seleccion a -1 (no selection)
            longListLocales.SelectedItem = null;
        }
    }
}
