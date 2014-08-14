using CarguerosWebServer.Models;
using System.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CarguerosWebServer.Services
{
    public class CDAccessBilling : CDBillingRepository
    {
        public const string CacheKey = "BillingStore";
        CDMySQLConnection mySQLConnection = CDMySQLConnection.Instance;

        public CDAccessBilling()
        {    
      
        }

        public override Billing[] showAllBilling()
        {
            DataSet dataSet = mySQLConnection.makeQuery("SELECT * FROM universidad.estudiante;"); 
            List<Billing> listBilling = getTableBilling(dataSet);    
            var ctx = HttpContext.Current;            
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    ctx.Cache[CacheKey] = listBilling.ToArray();                     
                }
            }
            return GetBilling();
        }



        public  List<Billing> getTableBilling(DataSet dataSet)
        {
            List<Billing> listBilling = new List<Billing>();
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    int idBilling = Convert.ToInt32(row[0]);
                    int tax = Convert.ToInt32(row[1].ToString());
                    int costStorage = Convert.ToInt32(row[2].ToString());
                    int discount = Convert.ToInt32(row[3].ToString());
                    int freight = Convert.ToInt32(row[4].ToString());                   
                    listBilling.Add(new Billing{
                        idBilling = idBilling,
                        tax = tax,
                        costStorage = costStorage,
                        discount = discount,
                        freight = freight
                    }
                    );
                }
            }
            return listBilling;
        }

        public Billing[] GetBilling()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Billing[])ctx.Cache[CacheKey];
            }
            return new Billing[]
        {
            new Billing
            {
                idBilling = 0,
                tax = 0,
                costStorage = 0,
                discount = 0,
                freight = 0
            }
        };
        }       
    }
}