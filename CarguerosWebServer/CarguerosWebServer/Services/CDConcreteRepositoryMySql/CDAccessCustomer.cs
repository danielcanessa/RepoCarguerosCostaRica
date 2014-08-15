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

        public override Customer[] showAllCustomer()
        {
            ClearCacheItems();
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
            ClearCacheItems();
            DataSet dataSet = mySQLConnection.makeQuery("CALL `Login_Customer`('" + password + "'," + account + ")");
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
            int account = 0;
            String name = "";
            String lastName = "";
            String telephone = "";
            String type = "";
            int score = 0;

            List<Customer> listCustomer = new List<Customer>();
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    try
                    {
                        account = Convert.ToInt32(row[0]);
                    }
                    catch (Exception) { }

                    try
                    {
                        name = row[1].ToString();
                    }
                    catch (Exception) { }

                    try
                    {
                        lastName = row[2].ToString();
                    }
                    catch (Exception) { }

                    try
                    {
                        telephone = row[3].ToString();
                    }
                    catch (Exception) { }

                    try
                    {
                        type = row[4].ToString();
                    }
                    catch (Exception) { }

                    try
                    {
                        score = Convert.ToInt32(row[5]);
                    }
                    catch (Exception) { }

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