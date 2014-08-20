using CarguerosWebServer.Models;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CarguerosWebServer.Services
{
    public abstract class CDCustomerRepository
    {

        public abstract Customer[] loginCustomer(String password, int account);

        public abstract int createCustomer(String name, String last_name, String telephone, String password, int route);

    }

}