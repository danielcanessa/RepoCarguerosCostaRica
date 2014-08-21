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
        //Instance of this controller
        private CDRouteRepository routeRepository;

        /*
         * public CDRouteController()
         * Constructor of the controller
         */
        public CDRouteController()
        {
            CDConcreteFactoryWebServer factory = CDConcreteFactoryWebServer.Instance;
            this.routeRepository = factory.CreateCDRouteRepository();
        }

        /*
         * public String createRoute(String name, String exitPoint, String arrivalPoint, String price, int duration, int maxAmount)
         * Method for create a new route
         */
        [HttpPost]
        [ActionName("createRoute")]
        public String createRoute(String name, String exitPoint, String arrivalPoint, String price, int duration, int maxAmount)
        {
            //Condition for validate that the price is a double number
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

        /*
         * public Route[] showRoutes()
         * Method for return all the existing routes
         */
        [HttpGet]
        [ActionName("showRoutes")]
        public Route[] showRoutes()
        {
            return routeRepository.showRoutes();
        }

        /*
         * public Route[] bestRoutes(int ammount)
         * Method for return the best routes
         */
        [HttpGet]
        [ActionName("bestRoutes")]
        public Route[] bestRoutes(int ammount)
        {
            return routeRepository.bestRoutes(ammount);
        }

        /*
         * public Route[] worstRoutes(int ammount)
         * Method for return the worst routes
         */
        [HttpGet]
        [ActionName("worstRoutes")]
        public Route[] worstRoutes(int ammount)
        {
            return routeRepository.worstRoutes(ammount);
        }
    }
}
