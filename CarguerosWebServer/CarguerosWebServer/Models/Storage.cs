using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarguerosWebServer.Models
{
    public class Storage
    {
        public int packagesIdPackages { get; set; }

        public String arrivalDate { get; set; }  
     
        public String departureDate { get; set; }

        public String costumer { get; set; }

        public double price { get; set; }

        public String type { get; set; }

        public int size { get; set; }

        public int waitingAverage { get; set; }

        public int countStorage { get; set; }
    }
}