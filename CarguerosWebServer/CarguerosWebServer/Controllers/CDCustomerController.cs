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
        private CDCustomerRepository customerRepository;


         public CDCustomerController()
        {
            CDConcreteFactoryWebServer factory = CDConcreteFactoryWebServer.Instance;
            this.customerRepository = factory.CreateCDCustomerRepository();
        }
       
        
        [HttpGet]
        [ActionName("login")]
        public Customer[] loginCustomer(String password, int account)
        {
            return customerRepository.loginCustomer(password,account);
        }

        [HttpPost]
        [ActionName("SingUp")]
        public String createCustomer(String name, String last_name, String telephone, String password, int route)
        {
            int id= customerRepository.createCustomer(name, last_name, telephone, password, route);
            return Convert.ToString(id);
        }
         
    }
}
