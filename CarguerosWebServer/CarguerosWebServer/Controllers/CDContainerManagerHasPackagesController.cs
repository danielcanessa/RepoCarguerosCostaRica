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
    public class CDContainerManagerHasPackagesController : ApiController
    {
         private CDContainerManagerHasPackagesRepository containerManagerHasPackagesRepository;

         public CDContainerManagerHasPackagesController()
        {
            
            CDConcreteFactoryWebServer factory = CDConcreteFactoryWebServer.Instance;
            this.containerManagerHasPackagesRepository = factory.CreateCDContainerManagerHasPackagesRepository();
           
           
        }
       
        [HttpGet]
        [ActionName("1")]
         public ContainerManagerHasPackages[] allContainerManagerHasPackages()
        {
            return containerManagerHasPackagesRepository.showAllContainerManagerHasPackages();            
        }     
    }
}
