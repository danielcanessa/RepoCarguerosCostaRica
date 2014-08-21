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
        //Instance of this controller
        private CDEmployeeRepository employeeRepository;

        /*
         * public CDEmployeeController()
         * Constructor of the controller
         */
        public CDEmployeeController()
        {            
            CDConcreteFactoryWebServer factory = CDConcreteFactoryWebServer.Instance;
            this.employeeRepository = factory.CreateCDEmployeeRepository();                      
        }

        /*
         * public Employee[] allEmployee(String password, int idEmployee)
         * Method for validate an employee login, return the employee data
         */
        [HttpGet]
        [ActionName("loginEmployee")]
        public Employee[] allEmployee(String password, int idEmployee)
        {
            return employeeRepository.loginEmployee(password, idEmployee);     
        }

        /*
         * public String createEmployee(String name, String last_name, String telephone, String password, int role)
         * Method for create a new employee, return the ID of the new employee
         */
        [HttpPost]
        [ActionName("SingUp")]
        public String createEmployee(String name, String last_name, String telephone, String password, int role)
        {
            int id= employeeRepository.createEmployee(name, last_name, telephone, password, role);         
            return Convert.ToString(id);
        }
    }
}
