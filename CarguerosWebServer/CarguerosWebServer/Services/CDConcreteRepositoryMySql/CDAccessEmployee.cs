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
        

        public override int createEmployee(String name, String last_name, String telephone, String password, int role)
        {

            long idEmployee=mySQLConnection.makePostQuery("Insert INTO  Person SET name = '" + name + "',last_name = '" + last_name + "', telephone = '" + telephone + "',password = '" + password + "'; ");
            int idEmployeeAux = Convert.ToInt32(idEmployee);
            mySQLConnection.makeQuery("CALL `Register_Employee`(" + idEmployeeAux + ", " + role + ");");

            return idEmployeeAux;

        }


        public override Employee[] loginEmployee(String password,int idEmployee)
        {
            HttpContext.Current.Cache.Remove(CacheKey);
            DataSet dataSet = mySQLConnection.makeQuery("CALL `Login_Employee`('"+password+"' ,"+idEmployee+");"); 
            List<Employee> listEmployee = getTableLoginEmployee(dataSet);    
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



        public List<Employee> getTableLoginEmployee(DataSet dataSet)
        {
            int personIdPerson =-1;
            String name = "-";
            String last_name = "-";
            String telephone = "-";
            String password = "-";
            String role = "-";
            List<Employee> listEmployee = new List<Employee>();
            
            DataTable dataTable = dataSet.Tables[0];            
            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                if (dataTable.Columns.Contains("idPerson") && row["idPerson"] != DBNull.Value) {  personIdPerson = Convert.ToInt32(row["idPerson"]); }
                if (dataTable.Columns.Contains("name") && row["name"] != DBNull.Value) { name = Convert.ToString(row["name"]); }
                if (dataTable.Columns.Contains("last_name") && row["last_name"] != DBNull.Value) { last_name = Convert.ToString(row["last_name"]); }
                if (dataTable.Columns.Contains("telephone") && row["telephone"] != DBNull.Value) { telephone = Convert.ToString(row["telephone"]); }
                if (dataTable.Columns.Contains("role") && row["role"] != DBNull.Value) { role = Convert.ToString(row["role"]); }

                listEmployee.Add(new Employee
                {
                    personIdPerson = personIdPerson,
                    name = name,
                    last_name = last_name,
                    telephone = telephone,
                    password = password,
                    role = role
                }
                );
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
            return null;
        }
    }
}