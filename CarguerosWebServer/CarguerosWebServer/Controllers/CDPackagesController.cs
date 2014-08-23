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
    public class CDPackagesController : ApiController
    {
        //Instance of this controller
        private CDPackagesRepository packagesRepository;

        /*
         * public CDPackagesController()
         * Constructor of the controller
         */
        public CDPackagesController()
        {
            CDConcreteFactoryWebServer factory = CDConcreteFactoryWebServer.Instance;
            this.packagesRepository = factory.CreateCDPackagesRepository();
        }

        /*
         * public Packages[] allPackagesPerUser(int account)
         * Method for return all the packages of a customer
         */
        [HttpGet]
        [ActionName("UserPackages")]
        public Packages[] allPackagesPerUser(int account)
        {
            return packagesRepository.packagesUser(account);
        }

        /*
         * public Packages[] packagesDetailsPerUser(int account)
         * Method for return the details of all package of a specific customer 
         */
        [HttpGet]
        [ActionName("PackagesDetails")]
        public Packages[] packagesDetailsPerUser(int account)
        {
            return packagesRepository.detailsPackages(account);
        }

        /*
         * public Packages[] packageArrived(int account)
         * Method that return a list of package that have a state of arrive
         */
        [HttpGet]
        [ActionName("packageArrived")]
        public Packages[] packageArrived()
        {
            return packagesRepository.packageArrived();
        }

        /*
         * public String createPackage(int weight, int size, String type, String price, String description, int account)
         * Method for create a new package, return the ID of the new package
         */
        [HttpPost]
        [ActionName("createPackage")]
        public String createPackage(int weight, int size, String type, String price, String description, int account)
        {
            int id = packagesRepository.createPackage(weight, size, type, price, description, account);
            return Convert.ToString(id);
        }

    }
}
