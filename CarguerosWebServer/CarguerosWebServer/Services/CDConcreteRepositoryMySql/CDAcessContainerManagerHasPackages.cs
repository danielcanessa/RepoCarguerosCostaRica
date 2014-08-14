using CarguerosWebServer.Models;
using System.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarguerosWebServer.Services
{
    public class CDAcessContainerManagerHasPackages : CDContainerManagerHasPackagesRepository
    {
        
        public const string CacheKey = "ContainerManagerHasPackagesStore";
        CDMySQLConnection mySQLConnection = CDMySQLConnection.Instance;

        public CDAcessContainerManagerHasPackages()
        {    
      
        }

        public override ContainerManagerHasPackages[] showAllContainerManagerHasPackages()
        {
            DataSet dataSet = mySQLConnection.makeQuery("SELECT * FROM universidad.estudiante;");
            List<ContainerManagerHasPackages> listContainerManagerHasPackages = getTableContainerManagerHasPackages(dataSet);    
            var ctx = HttpContext.Current;            
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    ctx.Cache[CacheKey] = listContainerManagerHasPackages.ToArray();                     
                }
            }
            return GetContainerManagerHasPackages();
        }

        public List<ContainerManagerHasPackages> getTableContainerManagerHasPackages(DataSet dataSet)
        {
            List<ContainerManagerHasPackages> listContainerManagerHasPackages = new List<ContainerManagerHasPackages>();
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {        
                    int idContainerManagerP = Convert.ToInt32(row[0]);
                    int idPackagesP = Convert.ToInt32(row[1]);

                    listContainerManagerHasPackages.Add(new ContainerManagerHasPackages
                    {

                        idContainerManagerP = idContainerManagerP,
                        idPackagesP = idPackagesP                       
                    }
                    );
                }
            }
            return listContainerManagerHasPackages;
        }

        public ContainerManagerHasPackages[] GetContainerManagerHasPackages()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (ContainerManagerHasPackages[])ctx.Cache[CacheKey];
            }
            return new ContainerManagerHasPackages[]
        {
            new ContainerManagerHasPackages
            {
                 idContainerManagerP = 0,
                 idPackagesP = 0
            }
        };
        } 


    }
}