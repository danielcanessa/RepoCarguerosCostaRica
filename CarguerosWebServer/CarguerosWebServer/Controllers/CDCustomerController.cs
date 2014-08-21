using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CarguerosWebServer.Services;
using CarguerosWebServer.Models;
using System.Web;

namespace CarguerosWebServer.Controllers
{
    public class CDCustomerController : ApiController
    {
        //Instance of this controller
        private CDCustomerRepository customerRepository;

        /*
         * public CDCustomerController()
         * Constructor of the controller
         */
        public CDCustomerController()
        {
            CDConcreteFactoryWebServer factory = CDConcreteFactoryWebServer.Instance;
            this.customerRepository = factory.CreateCDCustomerRepository();
        }

        /*
         * public Customer[] loginCustomer(String password, int account)
         * Method for validate the customer login, return the data of the customer
         */
        [HttpGet]
        [ActionName("login")]
        public Customer[] loginCustomer(String password, int account)
        {
            return customerRepository.loginCustomer(password, account);
        }

        /*
         * public String createCustomer(String name, String last_name, String telephone, String password, int route)
         * Method for create a new customer, return the new customer id
         */
        [HttpPost]
        [ActionName("SingUp")]
        public String createCustomer(String name, String last_name, String telephone, String password, int route)
        {
            int id = customerRepository.createCustomer(name, last_name, telephone, password, route);
            return Convert.ToString(id);
        }

    }
}
