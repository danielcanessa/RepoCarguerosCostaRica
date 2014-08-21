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

    public class CDBillingController : ApiController
    {

        //Instance of this controller
        private CDBillingRepository billingRepository;

        /*
         * public CDBillingController()
         * Constructor of the controller
         */
        public CDBillingController()
        {
            CDConcreteFactoryWebServer factory = CDConcreteFactoryWebServer.Instance;
            this.billingRepository = factory.CreateCDBillingRepository();
        }

        /*
         * public Billing[] showcostumerBilling(int account)
         * Get method for retrun the Billing list of a customer
         */
        [HttpGet]
        [ActionName("showcostumerBilling")]
        public Billing[] showcostumerBilling(int account)
        {
            return billingRepository.showcostumerBilling(account);
        }

    }
}
