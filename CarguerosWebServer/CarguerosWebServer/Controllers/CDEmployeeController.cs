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
    public class CDEmployeeController : ApiController
    {
        private CDEmployeeRepository employeeRepository;

        public CDEmployeeController()
        {
            
            CDConcreteFactoryWebServer factory = CDConcreteFactoryWebServer.Instance;
            this.employeeRepository = factory.CreateCDEmployeeRepository();
           
           
        }
       
        [HttpGet]
        [ActionName("1")]
        public Employee[] allEmployee()
        {    
            return employeeRepository.showAllEmployee();            
        }      


    }
}
