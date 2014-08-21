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

        [HttpPost]
        [ActionName("createRoute")]
        public String createRoute(String name, String exitPoint, String arrivalPoint, String price, int duration, int maxAmount)
        {
            try
            {
                float aux_price = float.Parse(price, System.Globalization.CultureInfo.InvariantCulture);
                if (routeRepository.createRoute(name, exitPoint, arrivalPoint, price, duration, maxAmount) == 200) { return "Sucess"; }
                else { return "fail"; }
            }
            catch (Exception)
            {
                return "fail";
            }
        }   
        

        [HttpGet]
        [ActionName("showRoutes")]
        public Route[] createRoute()
        {
            return routeRepository.showRoutes();
        }

        [HttpGet]
        [ActionName("bestRoutes")]
        public Route[] bestRoutes(int ammount)
        {
            return routeRepository.bestRoutes(ammount);
        }

        [HttpGet]
        [ActionName("worstRoutes")]
        public Route[] worstRoutes(int ammount)
        {
            return routeRepository.worstRoutes(ammount);
        }   
      
    }
}
