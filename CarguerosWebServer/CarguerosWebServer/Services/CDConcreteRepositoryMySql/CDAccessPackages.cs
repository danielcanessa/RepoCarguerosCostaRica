using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarguerosWebServer.Models;
using System.Data;
using MySql.Data.MySqlClient;


namespace CarguerosWebServer.Services
{
    public class CDAccessPackages : CDPackagesRepository
    {
         public const string CacheKey = "PackagesStore";
        CDMySQLConnection mySQLConnection = CDMySQLConnection.Instance;

        public CDAccessPackages()
        {    
      
        }

        public override Packages[] showAllPackages()
        {
            DataSet dataSet = mySQLConnection.makeQuery("SELECT * FROM universidad.estudiante;"); 
            List<Packages> listPackages = getTablePackages(dataSet);    
            var ctx = HttpContext.Current;            
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    ctx.Cache[CacheKey] = listPackages.ToArray();                     
                }
            }
            return GetPackages();
        }

        public override Packages[] packagesPerUser(int account)
        {
            DataSet dataSet = mySQLConnection.makeQuery("CALL  `User_packages`("+account+");");
            List<Packages> listPackages = getTablePackagesPerUser(dataSet);
            var ctx = HttpContext.Current;
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    ctx.Cache[CacheKey] = listPackages.ToArray();
                }
            }
            return GetPackages();
        }

        public List<Packages> getTablePackagesPerUser(DataSet dataSet)
        {
            List<Packages> listPackages = new List<Packages>();
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    //Packages 

                    int idPackages = Convert.ToInt32(row[0]);                  
                    String description = row[1].ToString();

                    listPackages.Add(new Packages
                    {
                        idPackages = idPackages,
                        weight = 0,
                        size = 0,
                        price = 0,
                        type = "-",
                        description = description

                    }
                    );
                }
            }
            return listPackages;
        }


        public  List<Packages> getTablePackages(DataSet dataSet)
        {
            List<Packages> listPackages = new List<Packages>();
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    //Packages 

                    int idPackages = Convert.ToInt32(row[0]);
                    int weight = Convert.ToInt32(row[1]);
                    int size = Convert.ToInt32(row[2]);
                    int price = Convert.ToInt32(row[3]);
                    String type = row[4].ToString();
                    String description = row[5].ToString();
           
                    listPackages.Add(new Packages{
                        idPackages = idPackages,
                        weight = weight,
                        size = size,
                        price = price,
                        type = type,
                        description = description

                    }
                    );
                }
            }
            return listPackages;
        }

        public Packages[] GetPackages()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Packages[])ctx.Cache[CacheKey];
            }
            return new Packages[]
        {
            new Packages
            {
                idPackages = 0,
                weight = 0,
                size = 0,
                price = 0,
                type = "-",
                description = "-"
            }
        };
        }       
       
    }
}