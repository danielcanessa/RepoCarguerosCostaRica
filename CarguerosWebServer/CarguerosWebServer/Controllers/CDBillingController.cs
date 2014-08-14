using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CarguerosWebServer.Services;
using CarguerosWebServer.Models;

namespace CarguerosWebServer.Controllers{
       
    public class CDBillingController : ApiController
    {
       
        private CDBillingRepository billingRepository;

        public CDBillingController()
        {
            
            CDConcreteFactoryWebServer factory = CDConcreteFactoryWebServer.Instance;
            this.billingRepository = factory.CreateCDBillingRepository();
           
           
        }
       
        [HttpGet]
        [ActionName("1")]
        public Billing[] allBilling()
        {    
            return billingRepository.showAllBilling();            
        }      

    }
}
