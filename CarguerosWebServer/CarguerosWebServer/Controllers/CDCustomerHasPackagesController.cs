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
        //Instance of this controller
        private CDCustomerHasPackagesRepository CustomerHasPackagesRepository;

        /*
         * public CDCustomerHasPackagesController()
         * Constructor of the controller
         */
        public CDCustomerHasPackagesController()
        {
            CDConcreteFactoryWebServer factory = CDConcreteFactoryWebServer.Instance;
            this.CustomerHasPackagesRepository = factory.CreateCDCustomerHasPackagesRepository();
        }

        /*
         * public CustomerHasPackages[] packageArrive(int account, int idPackage)
         * Method for validate if a package arrive to a customer
         */
        [HttpGet]
        [ActionName("packageArrive")]
        public CustomerHasPackages[] packageArrive(int account, int idPackage)
        {
            return CustomerHasPackagesRepository.packageArrive(account, idPackage);
        }

        /*
         * public CustomerHasPackages[] bestCustomers(int ammount)
         * Method for get a best customer list
         */
        [HttpGet]
        [ActionName("bestCustomers")]
        public CustomerHasPackages[] bestCustomers(int ammount)
        {
            return CustomerHasPackagesRepository.bestCustomers(ammount);
        }

        /*
         * public CustomerHasPackages[] worstCustomers(int ammount)
         * Method for get a worst customer list
         */
        [HttpGet]
        [ActionName("worstCustomers")]
        public CustomerHasPackages[] worstCustomers(int ammount)
        {
            return CustomerHasPackagesRepository.worstCustomers(ammount);
        }
    }
}
