using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CarguerosWebServer.Services;
using CarguerosWebServer.Models;

namespace CarguerosWebServer.Controllers
{
    public class CDCustomerHasPackagesController : ApiController
    {
        private CDCustomerHasPackagesRepository CustomerHasPackagesRepository;

        public CDCustomerHasPackagesController()
        {
            
            CDConcreteFactoryWebServer factory = CDConcreteFactoryWebServer.Instance;
            this.CustomerHasPackagesRepository = factory.CreateCDCustomerHasPackagesRepository();
           
           
        }
       
        [HttpGet]
        [ActionName("packageArrive")]
        public CustomerHasPackages[] packageArrive(int account, int idPackage)
        {
            return CustomerHasPackagesRepository.packageArrive(account, idPackage);           
        }     

       
    }
}
