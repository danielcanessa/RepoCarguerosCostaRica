using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarguerosWebServer.Models
{
    public class CustomerHasPackages
    {
        public int customerAccount { get; set; }

        public int packagesIdPackages { get; set; }

        public String packageState { get; set; }

        public int billingIdBilling { get; set; }

        public String name { get; set; }

        public int total { get; set; }      
    }
}