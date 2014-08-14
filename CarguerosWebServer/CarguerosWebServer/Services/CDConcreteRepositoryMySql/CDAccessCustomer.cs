using CarguerosWebServer.Models;
using System.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarguerosWebServer.Services
{
    public class CDAccessCustomer : CDCustomerRepository
    {
        public const string CacheKey = "CustomerStore";
        CDMySQLConnection mySQLConnection = CDMySQLConnection.Instance;

        public CDAccessCustomer()
        {          
        }

        public override Customer[] showAllCustomer()
        {
            DataSet dataSet = mySQLConnection.makeQuery("SELECT * FROM billing.customer;");
            List<Customer> listCustomer = getTableCustomer(dataSet);
            var ctx = HttpContext.Current;
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    ctx.Cache[CacheKey] = listCustomer.ToArray();
                }
            }
            return GetCustomer();
        }

        

        public override Customer[] loginCustomer(String password, int account)
        {
            DataSet dataSet = mySQLConnection.makeQuery("CALL `Login_Customer`('"+password+"',"+account+")");
            List<Customer> listCustomer = getTableCustomer(dataSet);
            var ctx = HttpContext.Current;
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    ctx.Cache[CacheKey] = listCustomer.ToArray();
                }
            }
            return GetCustomer();
        }


        public List<Customer> getTableCustomer(DataSet dataSet)
        {
            List<Customer> listCustomer = new List<Customer>();
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    int account = Convert.ToInt32(row[0]);
                    String name = row[1].ToString();
                    String lastName = row[2].ToString();
                    String telephone = row[3].ToString();
                    String type = row[4].ToString();
                    int score = Convert.ToInt32(row[5]);
                    listCustomer.Add(new Customer
                    {
                        account = account,                      
                        score = score,
                        type = type,
                        name = name,
                        telephone = telephone,
                        lastName = lastName
                    
                    }
                    );
                }
            }
            return listCustomer;
        }

        public Customer[] GetCustomer()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Customer[])ctx.Cache[CacheKey];
            }
            return new Customer[]
        {
            new Customer
            {
                account = 0,   
                score = 0,
                type = "",
                name = "",
                telephone = "",
                lastName = ""

                       
            }
        };
        }       

    }
}