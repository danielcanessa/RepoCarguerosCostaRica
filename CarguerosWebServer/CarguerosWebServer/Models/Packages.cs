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
        public int price { get; set; }
        public String type { get; set; }
        public String description { get; set; }
        
    }
}