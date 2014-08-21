using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarguerosWebServer.Models
{
    public class Billing
    {
        public int idBilling { get; set; }

        public int tax { get; set; }

        public int costStorage { get; set; }

        public int discount { get; set; }

        public int freight { get; set; }
    }
}