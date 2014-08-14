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
            DataSet dataSet = mySQLConnection.makeQuery("SELECT * FROM universidad.estudiante;");
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
                    int personIdPerson = Convert.ToInt32(row[1]);
                    int score = Convert.ToInt32(row[2]);
                    int type = Convert.ToInt32(row[3]);
                    listCustomer.Add(new Customer
                    {
                        account = account,
                        personIdPerson = personIdPerson,
                        score = score,
                        type = type
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
                personIdPerson = 0,
                score = 0,
                type = 0
            }
        };
        }       

    }
}