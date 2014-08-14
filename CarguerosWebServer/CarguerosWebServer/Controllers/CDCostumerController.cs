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
    public class CDCostumerController : ApiController
    {
        private CDCostumerRepository costumerRepository;

        public CDCostumerController()
        {
            CDConcreteFactoryWebServer factory = CDConcreteFactoryWebServer.Instance;
            this.costumerRepository = factory.CreateCustomerRepository();
        }

       
        [HttpGet]
        [ActionName("getCustomer")]
        public Customer edward(string id)
        {
            return costumerRepository.getCostumer(id);
        }

    }
}
