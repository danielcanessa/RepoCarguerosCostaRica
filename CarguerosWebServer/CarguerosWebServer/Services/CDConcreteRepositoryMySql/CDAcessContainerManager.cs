using CarguerosWebServer.Models;
using System.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarguerosWebServer.Services
{
    public class CDAcessContainerManager : CDContainerManagerRepository
    {
        public const string CacheKey = "ContainerManagerStore";
        CDMySQLConnection mySQLConnection = CDMySQLConnection.Instance;

        public CDAcessContainerManager()
        {    
      
        }

        public override ContainerManager[] showAllContainerManager()
        {
            DataSet dataSet = mySQLConnection.makeQuery("SELECT * FROM universidad.estudiante;");
            List<ContainerManager> listContainerManager = getTableContainerManager(dataSet);    
            var ctx = HttpContext.Current;            
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    ctx.Cache[CacheKey] = listContainerManager.ToArray();                     
                }
            }
            return GetContainerManager();
        }

        public List<ContainerManager> getTableContainerManager(DataSet dataSet)
        {
            List<ContainerManager> listContainerManager = new List<ContainerManager>();
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    int idContainerManager = Convert.ToInt32(row[0]);
                    int containerIdContainer = Convert.ToInt32(row[1]);
                    String available = row[2].ToString();
                    String arrivalDate = row[3].ToString();
                    String departureDate = row[4].ToString();
                    int actualWeight = Convert.ToInt32(row[5]);     
                    int routeIdRoute = Convert.ToInt32(row[6]);

                    listContainerManager.Add(new ContainerManager
                    {
                        idContainerManager = idContainerManager,
                        containerIdContainer = containerIdContainer,
                        available = available,
                        arrivalDate = arrivalDate,
                        departureDate = departureDate,
                        actualWeight = actualWeight,
                        routeIdRoute = routeIdRoute
                    }
                    );
                }
            }
            return listContainerManager;
        }

        public ContainerManager[] GetContainerManager()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (ContainerManager[])ctx.Cache[CacheKey];
            }
            return new ContainerManager[]
        {
            new ContainerManager
            {
                idContainerManager = 0,
                containerIdContainer = 0,
                available = "",
                arrivalDate = "",
                departureDate = "",
                actualWeight = 0,
                routeIdRoute = 0
            }
        };
        }       
    }
}