using CarguerosWebServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarguerosWebServer.Services
{
    public abstract class CDCustomerHasPackagesRepository
    {
        
        public abstract CustomerHasPackages[] packageArrive(int account, int idPackage);
        public abstract CustomerHasPackages[] bestCustomers(int ammount);
        public abstract CustomerHasPackages[] worstCustomers(int ammount);
       
    }
}