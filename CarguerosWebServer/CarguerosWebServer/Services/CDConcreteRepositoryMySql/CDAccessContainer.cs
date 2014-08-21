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

        public override int createContainer(String weight)
        {
            mySQLConnection.makeQuery("CALL `Create_Container`('" +weight+ "');");
            return HttpContext.Current.Response.StatusCode;
        }

        public override Container[] leastUsedContainers(int ammount)
        {
            HttpContext.Current.Cache.Remove(CacheKey);
            DataSet dataSet = mySQLConnection.makeQuery("CALL `Least_Used_Containers`("+ammount+");");
            List<Container> listContainer = getTableMostLeastUsedContainers(dataSet);
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

        public override Container[] mostUsedContainers(int ammount)
        {
            HttpContext.Current.Cache.Remove(CacheKey);
            DataSet dataSet = mySQLConnection.makeQuery("CALL `Most_Used_Containers`("+ammount+")");
            List<Container> listContainer = getTableMostLeastUsedContainers(dataSet);
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

        public List<Container> getTableMostLeastUsedContainers(DataSet dataSet)
        {
            int idContainer = -1;
            int route = -1;   
            int containerArrive = -1;
            int maxWeight = -1;
            int uses = -1;
            List<Container> listContainer = new List<Container>();

            foreach (DataTable dataTable in dataSet.Tables)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    if (dataTable.Columns.Contains("NumContainer") && row["NumContainer"] != DBNull.Value) { idContainer = Convert.ToInt32(row["NumContainer"]); }
                    if (dataTable.Columns.Contains("Uses") && row["Uses"] != DBNull.Value) { uses = Convert.ToInt32(row["Uses"]); }


                    listContainer.Add(new Container
                    {
                        idContainer = idContainer,
                        route = route,
                        containerArrive = containerArrive,
                        maxWeight = maxWeight,
                        uses = uses
                    }
                    );
                    rebootVarCreateCustomer(ref idContainer, ref uses);
                }
            }
            return listContainer;
        }

        private void rebootVarCreateCustomer(ref int idContainer, ref int uses)
        {
            idContainer = -1;
            uses = -1;
        }

        public override Container[] containersInRoute()
        {
            HttpContext.Current.Cache.Remove(CacheKey);
            DataSet dataSet = mySQLConnection.makeQuery("CALL `Containers_Route`();");
            List<Container> listContainer = getTableContainersInRoute(dataSet);
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

        public List<Container> getTableContainersInRoute(DataSet dataSet)
        {
            int idContainer = -1;
            int route = -1;
            int containerArrive = -1;
            int maxWeight = -1;
            int uses = -1;
            List<Container> listContainer = new List<Container>();

            foreach (DataTable dataTable in dataSet.Tables)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    if (dataTable.Columns.Contains("idContainer") && row["idContainer"] != DBNull.Value) { idContainer = Convert.ToInt32(row["idContainer"]); }
                    if (dataTable.Columns.Contains("idRoute") && row["idRoute"] != DBNull.Value) { route = Convert.ToInt32(row["idRoute"]); }


                    listContainer.Add(new Container
                    {
                        idContainer = idContainer,
                        route = route,
                        containerArrive = containerArrive,
                        maxWeight = maxWeight,
                        uses = uses
                    }
                    );
                    rebootVarContainersInRoute(ref idContainer, ref route);
                }
            }
            return listContainer;
        }

        private void rebootVarContainersInRoute(ref int idContainer, ref int route)
        {
            idContainer = -1;
            route = -1;
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
            int uses = -1;
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
                        maxWeight = maxWeight,
                        uses=uses
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