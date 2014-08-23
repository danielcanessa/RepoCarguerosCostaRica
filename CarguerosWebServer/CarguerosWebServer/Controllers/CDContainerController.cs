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
    public class CDContainerController : ApiController
    {
        //Instance of this controller
        private CDContainerRepository containerRepository;

        /*
         *  public CDContainerController()
         * Constructor of the controller
         */
        public CDContainerController()
        {            
            CDConcreteFactoryWebServer factory = CDConcreteFactoryWebServer.Instance;
            this.containerRepository = factory.CreateCDContainerRepository();                   
        }

        /*
         * public Container[] containerArrive(int idContainer, int route)
         * Method that verify if a specific container arrive to it destination
         */
        [HttpGet]
        [ActionName("containerArrive")]
        public Container[] containerArrive(int idContainer, int route)
        {
            return containerRepository.containerArrive(idContainer, route);
        }

        /*
         * public Container[] leastUsedContainers(int ammount)
         * Method that return the least used containers list
         */
        [HttpGet]
        [ActionName("leastUsedContainers")]
        public Container[] leastUsedContainers(int ammount)
        {
            return containerRepository.leastUsedContainers(ammount);
        }

        /*
         * public Container[] mostUsedContainers(int ammount)
         * Method that return the most used containers list
         */
        [HttpGet]
        [ActionName("mostUsedContainers")]
        public Container[] mostUsedContainers(int ammount)
        {
            return containerRepository.mostUsedContainers(ammount);
        }

        /*
         * public Container[] containerVsRoute()
         * Method that return a list of the uses of the container classified by route
         */
        [HttpGet]
        [ActionName("containerVsRoute")]
        public Container[] containerVsRoute()
        {
            return containerRepository.containerVsRoute();
        }

        /*
         * public Container[] containersInRoute()
         * Method that return all the containers in route
         */
        [HttpGet]
        [ActionName("containersInRoute")]
        public Container[] containersInRoute()
        {
            return containerRepository.containersInRoute();
        }

        /*
         * public String createContainer(String weight)
         * Method that create a new container
         */
        [HttpPost]
        [ActionName("createContainer")]
        public String createContainer(String weight)
        {
            //Condition for validate that the price is a double number
            try
            {                
                float aux_price = float.Parse(weight, System.Globalization.CultureInfo.InvariantCulture);
                if (containerRepository.createContainer(weight) == 200) { return "Sucess"; }
                else { return "fail"; }
            }
            catch (Exception)
            {
                return "fail";
            }
        }   
    }
}
