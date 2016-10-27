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
using System.IO.IsolatedStorage;
using SocialAppV2.ViewModels;

namespace SocialAppV2.View
{
    public partial class ViewPerfil : UserControl
    {
        public ViewPerfil()
        {
            InitializeComponent();
        }

        private async void validaremail_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Uri url = new Uri("http://produccion.rl2012alc.com/api/index.php/register");
            objetoslistas.registerinput paquete = new objetoslistas.registerinput();
            paquete.user = email.Text;
            string respuesta = await metodosJson.jsonPOST(url, paquete);
            var respuestajson = JsonConvert.DeserializeObject<objetoslistas.registeroutput>(respuesta.ToString());
            if (respuestajson.error == "")
            {
                if (respuestajson.alreadyRegistered == true)
                {
                    MessageBoxResult result = MessageBox.Show("El email ya esta registrado","ERROR",MessageBoxButton.OK);
                }
                else
                {
                    validaremail.Content = "email valido";
                    validaremail.IsEnabled = false;
                }

            }
            else
            {
                MessageBoxResult result =
                MessageBox.Show(respuestajson.error,
                "ERROR",
                MessageBoxButton.OK);
            }
        }

        private void validartlf_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void recuperar_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            passwordin.Begin();
        }

        private void passwordok_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            passwordout.Begin();
            //((MainPage)(((System.Windows.Controls.ContentControl)(App.RootFrame)).Content)).recuperaPerfil();
        }
    }
}
