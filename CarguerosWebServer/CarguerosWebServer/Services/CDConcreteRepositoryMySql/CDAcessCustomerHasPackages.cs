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

        public override CustomerHasPackages[] showAllCustomerHasPackages()
        {
            DataSet dataSet = mySQLConnection.makeQuery("SELECT * FROM universidad.estudiante;"); 
            List<CustomerHasPackages> listCustomerHasPackages = getTableCustomerHasPackages(dataSet);    
            var ctx = HttpContext.Current;            
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    ctx.Cache[CacheKey] = listCustomerHasPackages.ToArray();                     
                }
            }
            return GetCustomerHasPackages();
        }



        public  List<CustomerHasPackages> getTableCustomerHasPackages(DataSet dataSet)
        {
            List<CustomerHasPackages> listCustomerHasPackages = new List<CustomerHasPackages>();
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {         

                    int customerAccount = Convert.ToInt32(row[0]);
                    int packagesIdPackages = Convert.ToInt32(row[1]);
                    String packageState = row[2].ToString();
                    int billingIdBilling = Convert.ToInt32(row[3]);

                    listCustomerHasPackages.Add(new CustomerHasPackages
                    {
                        customerAccount = customerAccount,
                        packagesIdPackages = packagesIdPackages,
                        packageState = packageState,
                        billingIdBilling = billingIdBilling                        
                    }
                    );
                }
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
            return new CustomerHasPackages[]
        {
            new CustomerHasPackages
            {
               customerAccount = 0,
               packagesIdPackages = 0,
               packageState = "",
               billingIdBilling = 0  
            }
        };
        }       

    }
}