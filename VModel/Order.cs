using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppV2.VModel
{
    public class Order
    {
        public int status { get; set; }
        public int orderID { get; set; }
        public int type { get; set; }
        public string countryCode { get; set; }
        public DateTime estimatedReadyDateTime { get; set; }
        public DateTime readyDateTime { get; set; }
        public string template { get; set; }
    }
}
