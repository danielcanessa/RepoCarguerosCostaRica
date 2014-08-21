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
    public class CDStorageController : ApiController
    {
        //Instance of this controller
        private CDStorageRepository storageRepository;

        /*
         * public CDStorageController()
         * Constructor of the controller
         */
        public CDStorageController()
        {            
            CDConcreteFactoryWebServer factory = CDConcreteFactoryWebServer.Instance;
            this.storageRepository = factory.CreateCDStorageRepository();
        }

        /*
         * public Storage[] averagePackageStorage()
         * Method for return the average time of the packages in Storage
         */
        [HttpGet]
        [ActionName("averagePackageStorage")]
        public Storage[] averagePackageStorage()
        {
            return storageRepository.averagePackageStorage();         
        }

        /*
         * public Storage[] showPackageInStorage()
         * Method for return all the package in storage
         */
        [HttpGet]
        [ActionName("showPackageInStorage")]
        public Storage[] showPackageInStorage()
        {
            return storageRepository.showPackageInStorage();
        }     
    }
}
