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
        [ActionName("loginEmployee")]
        public Employee[] allEmployee(String password, int idEmployee)
        {
            return employeeRepository.loginEmployee(password, idEmployee);     
        }
        
        [HttpPost]
        [ActionName("SingUp")]
        public String createEmployee(String name, String last_name, String telephone, String password, int role)
        {
            int id= employeeRepository.createEmployee(name, last_name, telephone, password, role);
         
            return Convert.ToString(id);
            

        }


    }
}
