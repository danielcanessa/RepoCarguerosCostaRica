using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarguerosWebServer.Models
{
    public class Employee
    {
        public int personIdPerson { get; set; }

        public String name { get; set; }

        public String last_name { get; set; }

        public String telephone { get; set; }

        public String password { get; set; }

        public String role { get; set; }        
    }
}