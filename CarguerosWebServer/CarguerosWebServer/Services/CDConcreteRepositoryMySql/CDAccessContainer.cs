using CarguerosWebServer.Models;
using System.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarguerosWebServer.Services
{
    public class CDAccessContainer : CDContainerRepository
    {
        public const string CacheKey = "ContainerStore";
        CDMySQLConnection mySQLConnection = CDMySQLConnection.Instance;

        public CDAccessContainer()
        {

        }


        public override Container[] containerArrive(int idContainer, int route)
        {
            HttpContext.Current.Cache.Remove(CacheKey);
            DataSet dataSet = mySQLConnection.makeQuery("CALL `Container_arrived`(" + idContainer + " , "+route+");");
            List<Container> listContainer = getTableContainerArrive(dataSet,idContainer,route);
            var ctx = HttpContext.Current;
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    ctx.Cache[CacheKey] = listContainer.ToArray();
                }
            }
            return GetContainer();
        }

        public List<Container> getTableContainerArrive(DataSet dataSet, int idContainer, int route)
        {
            int containerArrive = -1;
            int maxWeight = -1;
            List<Container> listContainer = new List<Container>();

            foreach (DataTable dataTable in dataSet.Tables)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    if (dataTable.Columns.Contains("Container_arrived") && row["Container_arrived"] != DBNull.Value) { containerArrive = Convert.ToInt32(row["Container_arrived"]); }
                             
                   
                    listContainer.Add(new Container
                    {
                        idContainer = idContainer,
                        route = route,
                        containerArrive = containerArrive,
                        maxWeight = maxWeight
                    }
                    );
                }
            }
            return listContainer;
        }

        public Container[] GetContainer()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Container[])ctx.Cache[CacheKey];
            }
            return new Container[]
        {
            new Container
            {
                idContainer = 0,
                maxWeight = 0,               
            }
        };
        }
    }
}