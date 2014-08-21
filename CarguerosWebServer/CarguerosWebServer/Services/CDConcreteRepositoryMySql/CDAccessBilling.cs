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

        public override Billing[] showcostumerBilling(int account)
        {
            HttpContext.Current.Cache.Remove(CacheKey);
            DataSet dataSet = mySQLConnection.makeQuery("CALL `Costumer_Bills`("+account+");");
            List<Billing> listBilling = getTableShowcostumerBilling(dataSet);    
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



        public List<Billing> getTableShowcostumerBilling(DataSet dataSet)
        {
            int idBilling = -1;
            int tax = -1;
            int costStorage = 0; //In the case that it is null, is the same than a 0
            int discount = -1;
            int freight = -1; 
            List<Billing> listBilling = new List<Billing>();
            foreach (DataTable dataTable in dataSet.Tables)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    if (dataTable.Columns.Contains("idBilling") && row["idBilling"] != DBNull.Value) { idBilling = Convert.ToInt32(row["idBilling"]); }
                    if (dataTable.Columns.Contains("tax") && row["tax"] != DBNull.Value) { tax = Convert.ToInt32(row["tax"]); }
                    if (dataTable.Columns.Contains("costStorage") && row["costStorage"] != DBNull.Value) { costStorage = Convert.ToInt32(row["costStorage"]); }
                    if (dataTable.Columns.Contains("discount") && row["discount"] != DBNull.Value) { discount = Convert.ToInt32(row["discount"]); }
                    if (dataTable.Columns.Contains("freight") && row["freight"] != DBNull.Value) { freight = Convert.ToInt32(row["freight"]); }

                    listBilling.Add(new Billing{
                        idBilling = idBilling,
                        tax = tax,
                        costStorage = costStorage,
                        discount = discount,
                        freight = freight
                    });
                    rebootVarShowcostumerBilling(ref  idBilling, ref  tax, ref  costStorage, ref  discount, ref  freight);
                }
            }
            return listBilling;
        }

        public void rebootVarShowcostumerBilling(ref int idBilling, ref int tax, ref int costStorage, ref int discount, ref int freight)
        {
             idBilling = -1;
             tax = -1;
             costStorage = 0;
             discount = -1;
             freight = -1; 

        }  

        public Billing[] GetBilling()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Billing[])ctx.Cache[CacheKey];
            }
            return null;
       
        }       
    }
}