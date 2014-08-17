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

        public static void ClearCacheItems()
        {
            var enumerator = HttpContext.Current.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                HttpContext.Current.Cache.Remove(enumerator.Key.ToString());
            }
        }

        
        public override Customer[] loginCustomer(String password, int account)
        {
            HttpContext.Current.Cache.Remove(CacheKey);
            DataSet dataSet = mySQLConnection.makeQuery("CALL `Login_Customer`('" + password + "'," + account + ")");
            List<Customer> ListCustomer = createCustomer(dataSet);
            var ctx = HttpContext.Current;
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    ctx.Cache[CacheKey] = ListCustomer.ToArray();
                }
            }
            return GetCustomer();
        }


        public List<Customer> createCustomer(DataSet dataSet)
        {
            List<Customer> listCustomer = new List<Customer>();
           
            int account = -1;
            String name = "";
            String lastName = "";
            String telephone = "";
            String type = "";
            int score = -1;
        
            foreach (DataTable dataTable in dataSet.Tables)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    if (dataTable.Columns.Contains("account"))       { account = Convert.ToInt32(row["account"]); }
                    if (dataTable.Columns.Contains("name"))          { name = Convert.ToString(row["name"]); }
                    if (dataTable.Columns.Contains("last_name"))     { lastName = Convert.ToString(row["last_name"]); }
                    if (dataTable.Columns.Contains("telephone"))     { telephone = Convert.ToString(row["telephone"]); }
                    if (dataTable.Columns.Contains("type"))          { type = Convert.ToString(row["type"]); }
                    if (dataTable.Columns.Contains("score"))         { score = Convert.ToInt32(row["score"]); }

                    listCustomer.Add(new Customer
                    {
                        account = account,
                        score = score,
                        type = type,
                        name = name,
                        telephone = telephone,
                        lastName = lastName

                    });
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
            return null;
        }


    }
}