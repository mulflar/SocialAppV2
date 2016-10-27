using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialAppV2.VModel;
using SocialAppV2;
using Newtonsoft.Json;
using System.IO.IsolatedStorage;
using System.Device.Location;

namespace SocialAppV2.ViewModels
{
    public class ViewModelPartyEvent
    {
        public ObservableCollection<PartyandEvent> PartyandEventsReduced{ get; set; }
        public async Task<ObservableCollection<PartyandEvent>> GetPartyandEvents(double latitude, double longitude, int premium)
        {
            if (PartyandEventsReduced == null)
            {
                PartyandEventsReduced = await GetDefaultPartyandEvents(latitude, longitude, premium);
            }
            return PartyandEventsReduced;
        }

        public async Task<ObservableCollection<PartyandEvent>> GetDefaultPartyandEvents(double latitude, double longitude, int premium)
        {
            ObservableCollection<PartyandEvent> a = new ObservableCollection<PartyandEvent>();
            ObservableCollection<PartyandEvent> posiblepremium = new ObservableCollection<PartyandEvent>();
            Uri url = new Uri("http://produccion.rl2012alc.com/api/index.php/getPartiesAndEvents");
            objetoslistas.getByCoord paquete = new objetoslistas.getByCoord();
            paquete.latitude = latitude;
            paquete.longitude = longitude;
            string respuesta = await metodosJson.jsonPOST(url, paquete);
            var respuestajson = JsonConvert.DeserializeObject<objetoslistas.getPartiesAndEventsoutput>(respuesta.ToString());
            if (respuestajson.error == "")
            {
                if (respuestajson.events != null)
                {
                    foreach (objetoslistas.Event item in respuestajson.events)
                    {
                        GeoCoordinate localcoord = new GeoCoordinate(item.latitude, item.longitude);
                        GeoCoordinate centroccord = new GeoCoordinate(MainPage.usuariocordx, MainPage.usuariocordy);
                        int distraw = Convert.ToInt32(centroccord.GetDistanceTo(localcoord));
                        string distancia;
                        if (distraw > 1000)
                        {
                            distancia = (distraw / 1000).ToString() + " KM";
                        }
                        else
                        {
                            distancia = distraw.ToString() + "  M";
                        }
                        string grupo = "Eventos";
                        PartyandEvent evento = new PartyandEvent(item.name,item.numberOfMen,item.numberOfWomen,
                                                                    item.logoURL,
                                                                    item.beginDate, item.maxPrice,item.subtype,distancia,distraw, grupo);
                        a.Add(evento);
                    }
                 }
                    if (respuestajson.parties != null)
                    {
                        foreach (objetoslistas.Party itemparty in respuestajson.parties)
                        {
                            GeoCoordinate localcoordpart = new GeoCoordinate(itemparty.place.latitude, itemparty.place.longitude);
                            GeoCoordinate centroccord2 = new GeoCoordinate(MainPage.usuariocordx, MainPage.usuariocordy);
                            int distrawpar = Convert.ToInt32(centroccord2.GetDistanceTo(localcoordpart));
                            string distanciapar;
                            if (distrawpar > 1000)
                            {
                                distanciapar = (distrawpar / 1000).ToString() + " KM";
                            }
                            else
                            {
                                distanciapar = distrawpar.ToString() + "  M";
                            }
                            string grupopar = "Fiestas";
                            PartyandEvent party = new PartyandEvent(itemparty.name, itemparty.numberOfMen, itemparty.numberOfWomen, 
                                                                    itemparty.logoURL,
                                                                    itemparty.beginDate, itemparty.price, itemparty.place.name, distanciapar,distrawpar,grupopar);
                            if (itemparty.place.placeID == premium)
                            {
                                posiblepremium.Add(party);
                            }
                            else
                            {
                                a.Add(party);
                            }
                        }
                        if (posiblepremium.Count!=0)
                        {
                            Random random = new Random();
                            int premiumID = random.Next(0, posiblepremium.Count);
                            posiblepremium[premiumID].grupo = "Destacado";
                            foreach (PartyandEvent item in posiblepremium)
                            {
                                a.Add(item);
                            }
                        }

                    }
                    PartyandEventsReduced = a;
            }
            return PartyandEventsReduced;
        }
        public void GetSavedEventReduceds()
        {
            ObservableCollection<PartyandEvent> a = new ObservableCollection<PartyandEvent>();

            foreach (Object o in IsolatedStorageSettings.ApplicationSettings.Values)
            {
                a.Add((PartyandEvent)o);
            }
            PartyandEventsReduced = a;
        }

    }
}
