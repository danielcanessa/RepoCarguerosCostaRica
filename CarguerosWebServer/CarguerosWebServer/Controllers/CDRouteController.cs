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
    public class CDRouteController : ApiController
    {
        private CDRouteRepository routeRepository;

        public CDRouteController()
        {
            
            CDConcreteFactoryWebServer factory = CDConcreteFactoryWebServer.Instance;
            this.routeRepository = factory.CreateCDRouteRepository();
           
           
        }
       
        [HttpGet]
        [ActionName("1")]
        public Route[] allRoute()
        {    
            return routeRepository.showAllRoute();            
        }      
    }
}
