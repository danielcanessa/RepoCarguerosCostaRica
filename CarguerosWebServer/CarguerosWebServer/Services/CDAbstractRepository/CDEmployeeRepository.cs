using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarguerosWebServer.Models;

namespace CarguerosWebServer.Services
{
    public abstract class CDEmployeeRepository
    {
        public abstract Employee[] showAllEmployee();

        public abstract int createEmployee(String name, String last_name, String telephone, String password, String role);
    }
}