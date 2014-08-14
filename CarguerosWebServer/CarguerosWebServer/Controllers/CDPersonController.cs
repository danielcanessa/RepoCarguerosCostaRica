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
    public class CDPersonController : ApiController
    {
        private CDPersonRepository personRepository;

        public CDPersonController()
        {
            
            CDConcreteFactoryWebServer factory = CDConcreteFactoryWebServer.Instance;
            this.personRepository = factory.CreateCDPersonRepository();
           
           
        }
       
        [HttpGet]
        [ActionName("1")]
        public Person[] allPerson()
        {    
            return personRepository.showAllPerson();            
        }      

    }
}
