using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarguerosWebServer.Models;

namespace CarguerosWebServer.Models
{
    public class Customer
    {

        public int account { get; set; }       

        public int score { get; set; }

        public String type { get; set; }

        public String name { get; set; }

        public String lastName { get; set; }

        public String telephone { get; set; }

        public String password { get; set; }

        public String route { get; set; }

        

    }

}