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
        private CDContainerRepository containerRepository;

        public CDContainerController()
        {            
            CDConcreteFactoryWebServer factory = CDConcreteFactoryWebServer.Instance;
            this.containerRepository = factory.CreateCDContainerRepository();
                   
        }

        [HttpGet]
        [ActionName("containerArrive")]
        public Container[] containerArrive(int idContainer, int route)
        {
            return containerRepository.containerArrive(idContainer, route);
        }

        [HttpPost]
        [ActionName("createContainer")]
        public String createContainer(String weight)
        {
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
