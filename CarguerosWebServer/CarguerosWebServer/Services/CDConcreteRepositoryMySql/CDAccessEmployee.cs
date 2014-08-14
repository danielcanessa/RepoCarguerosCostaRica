using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarguerosWebServer.Models;
using System.Data;
using MySql.Data.MySqlClient;


namespace CarguerosWebServer.Services
{
    public class CDAccessEmployee : CDEmployeeRepository
    {
         public const string CacheKey = "EmployeeStore";
        CDMySQLConnection mySQLConnection = CDMySQLConnection.Instance;

        public CDAccessEmployee()
        {    
      
        }

        public override Employee[] showAllEmployee()
        {
            DataSet dataSet = mySQLConnection.makeQuery("SELECT * FROM universidad.estudiante;"); 
            List<Employee> listEmployee = getTableEmployee(dataSet);    
            var ctx = HttpContext.Current;            
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    ctx.Cache[CacheKey] = listEmployee.ToArray();                     
                }
            }
            return GetEmployee();
        }



        public  List<Employee> getTableEmployee(DataSet dataSet)
        {
            List<Employee> listEmployee = new List<Employee>();
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
             
                    int personIdPerson = Convert.ToInt32(row[0]);                                      
                    listEmployee.Add(new Employee{
                        personIdPerson = personIdPerson                      
                    }
                    );
                }
            }
            return listEmployee;
        }

        public Employee[] GetEmployee()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Employee[])ctx.Cache[CacheKey];
            }
            return new Employee[]
        {
            new Employee
            {
                 personIdPerson = 0     
            }
        };
        }       
        
    }
}