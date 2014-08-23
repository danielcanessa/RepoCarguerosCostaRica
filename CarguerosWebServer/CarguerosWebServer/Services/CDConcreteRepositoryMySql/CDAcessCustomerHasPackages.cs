using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarguerosWebServer.Models;
using System.Data;
using MySql.Data.MySqlClient;

namespace CarguerosWebServer.Services
{
    public class CDAcessCustomerHasPackages : CDCustomerHasPackagesRepository
    {
        //Cache key that represent Packages
        public const string CacheKey = "CustomerHasPackagesStore";
        //Object of the class CDMYSQLConecctions
        CDMySQLConnection mySQLConnection = CDMySQLConnection.Instance;

        public CDAcessCustomerHasPackages()
        {
        }

        /*
         * public override CustomerHasPackages[] bestCustomers(int ammount)
         * GET method that return a list of the best customers
         */
        public override CustomerHasPackages[] bestCustomers(int ammount)
        {
            HttpContext.Current.Cache.Remove(CacheKey);
            DataSet dataSet = mySQLConnection.makeQuery("CALL `Best_Customers`(" + ammount + ");");
            List<CustomerHasPackages> ListCustomer = getBestWorstCustomer(dataSet);
            var ctx = HttpContext.Current;
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    ctx.Cache[CacheKey] = ListCustomer.ToArray();
                }
            }
            return GetCustomerHasPackages();
        }

        /* public List<CustomerHasPackages> getBestWorstCustomer(DataSet dataSet)
         * Auxiliar method that return a dataSet with the data that  are need in the methods bestCustomers(int ammount) and worstCustomers(int ammount)
         */
        public List<CustomerHasPackages> getBestWorstCustomer(DataSet dataSet)
        {
            List<CustomerHasPackages> listCustomer = new List<CustomerHasPackages>();
            int customerAccount = -1;
            int packagesIdPackages  = -1;
            String packageState  = "-";
            int billingIdBilling = -1;
            String name = "-";
            int total = -1;
            foreach (DataTable dataTable in dataSet.Tables)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    if (dataTable.Columns.Contains("totalB") && row["totalB"] != DBNull.Value) { total = Convert.ToInt32(row["totalB"]); }
                    if (dataTable.Columns.Contains("name") && row["name"] != DBNull.Value) { name = Convert.ToString(row["name"]); }
                    listCustomer.Add(new CustomerHasPackages
                    {
                        customerAccount = customerAccount,
                        packagesIdPackages = packagesIdPackages,
                        packageState = packageState,
                        billingIdBilling = billingIdBilling,
                        name = name,
                        total = total                      

                    }
                    );
                    rebootBestWorstCustomer(ref total, ref name);
                }
            }
            return listCustomer;
        }

        /*
         * public override CustomerHasPackages[] bestCustomers(int ammount)
         * GET method that return a list of the worst customers
         */
        public override CustomerHasPackages[] worstCustomers(int ammount)
        {
            HttpContext.Current.Cache.Remove(CacheKey);
            DataSet dataSet = mySQLConnection.makeQuery("CALL `worst_Customers`("+ammount+");");
            List<CustomerHasPackages> ListCustomer = getBestWorstCustomer(dataSet);
            var ctx = HttpContext.Current;
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    ctx.Cache[CacheKey] = ListCustomer.ToArray();
                }
            }
            return GetCustomerHasPackages();
        }

        /*
         * private void rebootBestWorstCustomer(ref int total, ref string name)
         * Auxiliar Method used in the Method getBestWorstCustomer, with the finality of reboot some variables
         */
        private void rebootBestWorstCustomer(ref int total, ref string name)
        {
            name = "-";
            total = -1;
        }

        /*
         * public override CustomerHasPackages[] packageArrive(int account, int idPackage)
         * GET method that return if a package of a customer arrive to the final destination
         */
        public override CustomerHasPackages[] packageArrive(int account, int idPackage)
        {
            HttpContext.Current.Cache.Remove(CacheKey);
            DataSet dataSet = mySQLConnection.makeQuery("CALL `Package_arrived`(" + account + "," + idPackage + ");");
            List<CustomerHasPackages> ListPackageArrive = getpackageArrive(dataSet, account, idPackage);
            var ctx = HttpContext.Current;
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    ctx.Cache[CacheKey] = ListPackageArrive.ToArray();
                }
            }
            return GetCustomerHasPackages();
        }

        /*
         * public List<CustomerHasPackages> getpackageArrive(DataSet dataSet, int customerAccount, int packagesIdPackages)
         * Auxiliar method that return a dataSet with the data that  is need in the method packageArrive(int account, int idPackage)
         */
        public List<CustomerHasPackages> getpackageArrive(DataSet dataSet, int customerAccount, int packagesIdPackages)
        {
            List<CustomerHasPackages> listCustomerHasPackages = new List<CustomerHasPackages>();
            String packageState = "-";
            int billingIdBilling = -1;
            String name = "-";
            int total = -1;
            DataTable dataTable = dataSet.Tables[0];
            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                if (dataTable.Columns.Contains("package_available") && row["package_available"] != DBNull.Value)
                {
                    if (Convert.ToInt32(row["package_available"]) == 1) { packageState = "delivered"; }
                    else { packageState = "no_delivered"; }
                }
                listCustomerHasPackages.Add(new CustomerHasPackages
                {
                    customerAccount = customerAccount,
                    packagesIdPackages = packagesIdPackages,
                    packageState = packageState,
                    billingIdBilling = billingIdBilling,
                    name = name,
                    total = total
                }
                );
            }
            return listCustomerHasPackages;
        }

        /*
         *  public CustomerHasPackages[] GetCustomerHasPackages()
         *  GET Method for post in the cache a json array of elements
         */
        public CustomerHasPackages[] GetCustomerHasPackages()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (CustomerHasPackages[])ctx.Cache[CacheKey];
            }
            return null;
        }

    }
}