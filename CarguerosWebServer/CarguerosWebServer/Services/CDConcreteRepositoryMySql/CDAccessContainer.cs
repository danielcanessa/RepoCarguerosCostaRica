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


        public override Container[] containerArrive(int idEmployee, int password)
        {
            DataSet dataSet = mySQLConnection.makeQuery("CALL `Login_Employee`(" + password + " ," + idEmployee + ");");
            List<Container> listContainer = getTableContainerArrive(dataSet);
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

        public List<Container> getTableContainerArrive(DataSet dataSet)
        {
            int idContainer = -1;
            int route = -1;
            int containerArrive = -1;
            int maxWeight = -1;
            List<Container> listContainer = new List<Container>();

            foreach (DataTable dataTable in dataSet.Tables)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    if (dataTable.Columns.Contains("idContainer") && row["idContainer"] != DBNull.Value) { idContainer = Convert.ToInt32(row["idContainer"]); }
                    if (dataTable.Columns.Contains("route") && row["route"] != DBNull.Value) { route = Convert.ToInt32(row["route"]); }
                    if (dataTable.Columns.Contains("containerArrive") && row["containerArrive"] != DBNull.Value) { containerArrive = Convert.ToInt32(row["containerArrive"]); }                   
                   
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