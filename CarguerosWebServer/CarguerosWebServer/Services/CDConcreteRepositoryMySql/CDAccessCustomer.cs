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
        //Cache key that represent Customer
        public const string CacheKey = "CustomerStore";
        CDMySQLConnection mySQLConnection = CDMySQLConnection.Instance;

        public CDAccessCustomer()
        {
        }


        /*
         * public override int createCustomer(String name, String last_name, String telephone, String password, int route)
         * POST method that generate a new Customer and return the ID of he.
         */
        public override int createCustomer(String name, String last_name, String telephone, String password, int route)
        {
            long idCustomer = mySQLConnection.makePostQuery("Insert INTO  Person SET name = '" + name + "',last_name = '" + last_name + "', telephone = '" + telephone + "',password = '" + password + "'; ");
            int idCustomerAux = Convert.ToInt32(idCustomer);
            mySQLConnection.makeQuery(" CALL `Register_Customer`(" + idCustomerAux + "," + route + ");");
            return idCustomerAux;
        }

        /*
          * public override Customer[] loginCustomer(String password, int account)
          * GET method that return a list with the personal data of a customer if the password and account do match
          */
        public override Customer[] loginCustomer(String password, int account)
        {
            HttpContext.Current.Cache.Remove(CacheKey);
            DataSet dataSet = mySQLConnection.makeQuery("CALL `Login_Customer`('" + password + "'," + account + ");");
            List<Customer> ListCustomer = getLoginCustomer(dataSet);
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

        /*
         * public List<Customer> getLoginCustomer(DataSet dataSet)
         * Auxiliar method that return a dataSet with the data that  is need in the method loginCustomer() 
         */
        public List<Customer> getLoginCustomer(DataSet dataSet)
        {
            int account = -1;
            String name = "-";
            String lastName = "-";
            String telephone = "-";
            String type = "-";
            int score = -1;
            String password = "-";
            String route = "-";
            List<Customer> listCustomer = new List<Customer>();
            DataTable dataTable = dataSet.Tables[0];
            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                if (dataTable.Columns.Contains("account") && row["account"] != DBNull.Value) { account = Convert.ToInt32(row["account"]); }
                if (dataTable.Columns.Contains("name") && row["name"] != DBNull.Value) { name = Convert.ToString(row["name"]); }
                if (dataTable.Columns.Contains("last_name") && row["last_name"] != DBNull.Value) { lastName = Convert.ToString(row["last_name"]); }
                if (dataTable.Columns.Contains("telephone") && row["telephone"] != DBNull.Value) { telephone = Convert.ToString(row["telephone"]); }
                if (dataTable.Columns.Contains("type") && row["type"] != DBNull.Value) { type = Convert.ToString(row["type"]); }
                if (dataTable.Columns.Contains("score") && row["score"] != DBNull.Value) { score = Convert.ToInt32(row["score"]); }
                listCustomer.Add(new Customer
                {
                    account = account,
                    score = score,
                    type = type,
                    name = name,
                    telephone = telephone,
                    lastName = lastName,
                    route = route,
                    password = password
                });
            }
            return listCustomer;
        }

        /*
         * public void rebootVarCreateCustomer(ref int account, ref String name, ref String lastName, ref String telephone,ref String type,ref int score)
         * Auxiliar Method used in the Method getLoginCustomer, with the finality of reboot some variables
         */
        public void rebootVarCreateCustomer(ref int account, ref String name, ref String lastName, ref String telephone,
            ref String type, ref int score)
        {
            account = -1;
            name = "-";
            lastName = "-";
            telephone = "-";
            type = "-";
            score = -1;
        }

        /*
         *  public Customer[] GetCustomer()
         *  GET Method for post in the cache a json array of elements
         */
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