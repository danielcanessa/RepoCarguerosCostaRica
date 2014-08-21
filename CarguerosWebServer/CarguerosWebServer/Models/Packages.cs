using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarguerosWebServer.Models
{
    public class Packages
    {
        public int idPackages { get; set; }

        public int weight { get; set; }   
    
        public int size { get; set; }

        public double price { get; set; }

        public int account { get; set; }

        public String type { get; set; }

        public String description { get; set; }

        public String customer { get; set; }

        public String packageState { get; set; }

        public String containerArrivalDate { get; set; }

        public String container { get; set; }

        public String arrivalDate { get; set; }        
    }
}