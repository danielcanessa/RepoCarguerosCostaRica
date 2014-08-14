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
    public class CDContainerManagerController : ApiController
    {
        private CDContainerManagerRepository containerManagerRepository;

        public CDContainerManagerController()
        {
            
            CDConcreteFactoryWebServer factory = CDConcreteFactoryWebServer.Instance;
            this.containerManagerRepository = factory.CreateCDContainerManagerRepository();
           
           
        }
       
        [HttpGet]
        [ActionName("1")]
        public ContainerManager[] allContainerManager()
        {    
            return containerManagerRepository.showAllContainerManager();            
        }      
    }
}
