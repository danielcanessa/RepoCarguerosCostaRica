using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarguerosWebServer.Models;

namespace CarguerosWebServer.Services
{
    public abstract class CDEmployeeRepository
    {
   

        public abstract int createEmployee(String name, String last_name, String telephone, String password, int role);

        public abstract Employee[] loginEmployee(String password, int idEmployee);

    }
}