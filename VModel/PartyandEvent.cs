using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppV2.VModel
{
    public class PartyandEvent
    {
        public string name { get; set; }
        public int number_of_men { get; set; }
        public int number_of_women { get; set; }
        public string logoURL { get; set; }
        public DateTime beginDate { get; set; }
        public double price { get; set; }
        public string cabecera { get; set; }
        public string distancia { get; set; }
        public int distraw { get; set; }
        public string grupo { get; set; }

        public PartyandEvent(string _name, int nummen, int numwomen,
            string logo,
            DateTime fecha, double precio, string cab, string _distancia, int _distraw, string _grupo)
        {
            name = _name;
            number_of_men = nummen;
            number_of_women = numwomen;
            logoURL = logo;
            beginDate = fecha;
            price = precio;
            cabecera = cab;
            distancia = _distancia;
            distraw = _distraw;
            grupo = _grupo;
        }
    }
}
