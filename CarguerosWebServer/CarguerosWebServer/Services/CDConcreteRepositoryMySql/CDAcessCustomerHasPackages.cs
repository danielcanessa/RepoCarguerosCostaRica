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

        public const string CacheKey = "CustomerHasPackagesStore";
        CDMySQLConnection mySQLConnection = CDMySQLConnection.Instance;

        public CDAcessCustomerHasPackages()
        {

        }

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

        public List<CustomerHasPackages> getpackageArrive(DataSet dataSet, int customerAccount, int packagesIdPackages)
        {
            List<CustomerHasPackages> listCustomerHasPackages = new List<CustomerHasPackages>();

            String packageState = "-";
            int billingIdBilling = -1;

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
                }
                );
            }
            return listCustomerHasPackages;
        }
              

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