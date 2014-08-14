using CarguerosWebServer.Models;
using System.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarguerosWebServer.Services
{
    public class CDAccessCostumer : CDCostumerRepository
    {
        public const string CacheKey = "Costumer";
        CDMySQLConnection mySQLConnection = CDMySQLConnection.Instance;

        public CDCostumerRepository constructor()
        {
            return new CDAccessCostumer();
        }

        public override Models.Customer getCostumer(string numeroCuenta)
        {
            DataSet dataSet = mySQLConnection.makeQuery("call obtenerUsuario (0);");
            Customer customer = getCostumerObject(dataSet);    
            var ctx = HttpContext.Current;            
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    ctx.Cache[CacheKey] = customer.ToString();                    
                }
            }
            return getCostumerObject(dataSet);
        }

        public Customer getCostumerObject(DataSet dataSet)
        {
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    string nombre = row[0].ToString();
                    string apellido = row[1].ToString();
                    string telefono = row[2].ToString();
                    return new Customer
                    {
                        name = nombre,
                        last_name = apellido,
                        telephone = telefono
                    };
                    
                }
                return null;
            }
            return null;
           
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