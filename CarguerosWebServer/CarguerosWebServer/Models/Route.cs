using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarguerosWebServer.Models
{
    public class Route
    {
        public int idRoute { get; set; }
        public int cost { get; set; }
        public int duration { get; set; }
        public int maxAmount { get; set; }
        public int customerAccount { get; set; }
        public String name { get; set; }
        public String exitPoint { get; set; }
        public String arrivalPoint { get; set; }
    
    }
}