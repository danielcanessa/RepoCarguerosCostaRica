using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarguerosWebServer.Models;
using System.Data;
using MySql.Data.MySqlClient;

namespace CarguerosWebServer.Services
{
    public class CDAcessCustomerType : CDCustomerTypeRepository
    {
        public const string CacheKey = "CustomerTypeStore";
        CDMySQLConnection mySQLConnection = CDMySQLConnection.Instance;

        public CDAcessCustomerType()
        {    
      
        }

        public override CustomerType[] showAllCustomerType()
        {
            DataSet dataSet = mySQLConnection.makeQuery("SELECT * FROM universidad.estudiante;"); 
            List<CustomerType> listCustomerType = getTableCustomerType(dataSet);    
            var ctx = HttpContext.Current;            
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    ctx.Cache[CacheKey] = listCustomerType.ToArray();                     
                }
            }
            return GetCustomerType();
        }



        public  List<CustomerType> getTableCustomerType(DataSet dataSet)
        {
            List<CustomerType> listCustomerType = new List<CustomerType>();
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                { 
                    int idCustomerType = Convert.ToInt32(row[0]);
                    String type = row[1].ToString();
                    int minimunScore = Convert.ToInt32(row[2].ToString());                                    
                    listCustomerType.Add(new CustomerType{
                        idCustomerType = idCustomerType,
                        type = type,
                        minimunScore = minimunScore                     
                    }
                    );
                }
            }
            return listCustomerType;
        }

        public CustomerType[] GetCustomerType()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (CustomerType[])ctx.Cache[CacheKey];
            }
            return new CustomerType[]
        {
            new CustomerType
            {
               idCustomerType = 0,
               type = "",
               minimunScore = 0
            }
        };
        }  

    }
}