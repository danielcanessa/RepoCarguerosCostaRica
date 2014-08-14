using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarguerosWebServer.Models;
using System.Data;
using MySql.Data.MySqlClient;

namespace CarguerosWebServer.Services
{
    public class CDAcessStorage : CDStorageRepository
    {
         public const string CacheKey = "StorageStore";
        CDMySQLConnection mySQLConnection = CDMySQLConnection.Instance;

        public CDAcessStorage()
        {    
      
        }

        public override Storage[] showAllStorage()
        {
            DataSet dataSet = mySQLConnection.makeQuery("SELECT * FROM universidad.estudiante;"); 
            List<Storage> listStorage = getTableStorage(dataSet);    
            var ctx = HttpContext.Current;            
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    ctx.Cache[CacheKey] = listStorage.ToArray();                     
                }
            }
            return GetStorage();
        }



        public  List<Storage> getTableStorage(DataSet dataSet)
        {
            List<Storage> listStorage = new List<Storage>();
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    int packagesIdPackages = Convert.ToInt32(row[0]);
                    String arrivalDate = row[1].ToString();
                    String departureDate = row[2].ToString();
                                   
                    listStorage.Add(new Storage{
                        packagesIdPackages = packagesIdPackages,
                        arrivalDate = arrivalDate,
                        departureDate = departureDate
                        
                    }
                    );
                }
            }
            return listStorage;
        }

        public Storage[] GetStorage()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Storage[])ctx.Cache[CacheKey];
            }
            return new Storage[]
        {
            new Storage
            {
                 packagesIdPackages = 0,
                 arrivalDate = "",
                 departureDate = ""
            }
        };
        }       
    }
}