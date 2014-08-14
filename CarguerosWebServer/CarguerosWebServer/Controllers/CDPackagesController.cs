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
        private CDPackagesRepository packagesRepository;

        public CDPackagesController()
        {
            
            CDConcreteFactoryWebServer factory = CDConcreteFactoryWebServer.Instance;
            this.packagesRepository = factory.CreateCDPackagesRepository();
           
           
        }
       
        [HttpGet]
        [ActionName("1")]
        public Packages[] allPackages()
        {    
            return packagesRepository.showAllPackages();            
        }      
    }
}
