using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarguerosWebServer.Models
{
    public class CustomerType
    {
        public int idCustomerType { get; set; }
        public String type { get; set; }
        public int minimunScore { get; set; }

    }
}