using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarguerosWebServer.Models;
using System.Data;
using MySql.Data.MySqlClient;

namespace CarguerosWebServer.Services
{
    public class CDAcessRole : CDRoleRepository
    {
         public const string CacheKey = "RoleStore";
        CDMySQLConnection mySQLConnection = CDMySQLConnection.Instance;

        public CDAcessRole()
        {    
      
        }

        public override Role[] showAllRole()
        {
            DataSet dataSet = mySQLConnection.makeQuery("SELECT * FROM universidad.estudiante;"); 
            List<Role> listRole = getTableRole(dataSet);    
            var ctx = HttpContext.Current;            
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    ctx.Cache[CacheKey] = listRole.ToArray();                     
                }
            }
            return GetRole();
        }



        public  List<Role> getTableRole(DataSet dataSet)
        {
            List<Role> listRole = new List<Role>();
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    int employeePersonIdPerson = Convert.ToInt32(row[0]);
                    String type = row[1].ToString();
                                 
                    listRole.Add(new Role{
                        employeePersonIdPerson = employeePersonIdPerson,
                        type = type
                       
                    }
                    );
                }
            }
            return listRole;
        }

        public Role[] GetRole()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Role[])ctx.Cache[CacheKey];
            }
            return new Role[]
        {
            new Role
            {
                 employeePersonIdPerson = 0,
                 type = ""
            }
        };
        }       
    }
}