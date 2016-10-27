using Microsoft.Phone.Controls;
using Microsoft.Phone.Reactive;
using Newtonsoft.Json;
using SocialAppV2.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SocialAppV2
{
    public class metodosJson
    {
        public static async Task<string> jsonPOST(Uri theUri, object paquete)
        {
           
            //Create an Http client and set the headers we want
            HttpClient aClient = new HttpClient();
          
            //Post the data
            HttpResponseMessage aResponse = await aClient.PostAsync(theUri, new StringContent(JsonConvert.SerializeObject(paquete), Encoding.UTF8, "application/json"));
            if (aResponse.IsSuccessStatusCode)
            {
                string res = await aResponse.Content.ReadAsStringAsync();
                return res;
            }
            else
            {
                // show the response status code
                MessageBoxResult result =
                MessageBox.Show("HTTP Status: " + aResponse.StatusCode.ToString() + " - Reason: " + aResponse.ReasonPhrase,
                "ERROR",
                MessageBoxButton.OK);
                String failureMsg = "HTTP Status: " + aResponse.StatusCode.ToString() + " - Reason: " + aResponse.ReasonPhrase;
                return failureMsg;
            } 
        }
        
    }
}
