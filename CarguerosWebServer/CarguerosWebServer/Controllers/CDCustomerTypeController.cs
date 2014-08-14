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
    public class CDCustomerTypeController : ApiController
    {
        private CDCustomerTypeRepository customerTypeRepository;

        public CDCustomerTypeController()
        {
            
            CDConcreteFactoryWebServer factory = CDConcreteFactoryWebServer.Instance;
            this.customerTypeRepository = factory.CreateCDCustomerTypeRepository();
           
           
        }
       
        [HttpGet]
        [ActionName("1")]
        public CustomerType[] allCustomerType()
        {
            return customerTypeRepository.showAllCustomerType();            
        }      
    }
}
