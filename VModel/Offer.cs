using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppV2.VModel
{
    public class Offer
    {
        public Place place { get; set; }
        public bool main { get; set; }
        public int offerID { get; set; }
        public string imageURL { get; set; }
        public bool active { get; set; }
        public string name { get; set; }
        public string info { get; set; }
        public string template { get; set; }
        public string back { get; set; }

        public Offer(objetoslistas.offer offer)
        {
            if (offer.place!=null)
            {
                Place _place = new Place(offer.place);
                place = _place;
            }
            main = offer.main;
            imageURL = offer.imageURL;
            active = offer.active;
            name = offer.name;
            info = offer.info;
            template = "offer";
            back = "#FF938E8E";
        }
    }
    public class Offer_Extended:Offer
    {
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }
        public string promoText { get; set; }

        public Offer_Extended(objetoslistas.offer offer):base(offer)
        {
            PlaceLegend _place = new PlaceLegend(offer.place);
            dateFrom = offer.dateFrom;
            dateTo = offer.dateTo;
            promoText = offer.promoText;
        }
    }
}
