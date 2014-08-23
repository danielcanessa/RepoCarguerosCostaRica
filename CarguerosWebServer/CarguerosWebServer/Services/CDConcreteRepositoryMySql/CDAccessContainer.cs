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
        //Cache key that represent Container
        public const string CacheKey = "ContainerStore";
        CDMySQLConnection mySQLConnection = CDMySQLConnection.Instance;

        public CDAccessContainer()
        {
        }

        /*
         * public override int createContainer(String weight)
         * POST method for create a new container, return the state of the trasaction (200 = OK)
         */
        public override int createContainer(String weight)
        {
            mySQLConnection.makeQuery("CALL `Create_Container`('" + weight + "');");
            return HttpContext.Current.Response.StatusCode;
        }

        /*
         * public override Container[] leastUsedContainers(int ammount)
         * GET Method for return the less used containers
         */
        public override Container[] leastUsedContainers(int ammount)
        {
            HttpContext.Current.Cache.Remove(CacheKey);
            DataSet dataSet = mySQLConnection.makeQuery("CALL `Least_Used_Containers`(" + ammount + ");");
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

        /*
         * public override Container[] mostUsedContainers(int ammount)
         * GET Method for return the most used containers
         */
        public override Container[] mostUsedContainers(int ammount)
        {
            HttpContext.Current.Cache.Remove(CacheKey);
            DataSet dataSet = mySQLConnection.makeQuery("CALL `Most_Used_Containers`(" + ammount + ")");
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

        /*
         * public List<Container> getTableMostLeastUsedContainers(DataSet dataSet)
         * Auxiliar method that return a dataSet with the data that are need in the methods mostUsedContainers and leastUsedContainers
         */
        public List<Container> getTableMostLeastUsedContainers(DataSet dataSet)
        {
            int idContainer = -1;
            int route = -1;
            int containerArrive = -1;
            int maxWeight = -1;
            int uses = -1;
            String routeName = "-";
            int idContainer_Manager = -1;
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
                        uses = uses,
                        routeName = routeName,
                        idContainer_Manager = idContainer_Manager
                    }
                    );
                    rebootVarMostLeastUsedContainers(ref idContainer, ref uses);
                }
            }
            return listContainer;
        }

        /*
         * private void rebootVarMostLeastUsedContainers(ref int idContainer, ref int uses)
         * Auxiliar Method used in the Method getTableMostLeastUsedContainers, with the finality of reboot some variables
         */
        private void rebootVarMostLeastUsedContainers(ref int idContainer, ref int uses)
        {
            idContainer = -1;
            uses = -1;
        }

        /*
         * public override Container[] containersInRoute()
         * GET method that return a list of all the containers that actually are on route
         */
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

        /*
         * public List<Container> getTableContainersInRoute(DataSet dataSet)
         * Auxiliar method that return a dataSet with the data that  is need in the method containersInRoute()
         */
        public List<Container> getTableContainersInRoute(DataSet dataSet)
        {
            int idContainer = -1;
            int route = -1;
            int containerArrive = -1;
            int maxWeight = -1;
            int uses = -1;
            String routeName = "-";
            int idContainer_Manager = -1;
            List<Container> listContainer = new List<Container>();

            foreach (DataTable dataTable in dataSet.Tables)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    if (dataTable.Columns.Contains("idContainer") && row["idContainer"] != DBNull.Value) { idContainer = Convert.ToInt32(row["idContainer"]); }
                    if (dataTable.Columns.Contains("idRoute") && row["idRoute"] != DBNull.Value) { route = Convert.ToInt32(row["idRoute"]); }
                    if (dataTable.Columns.Contains("name") && row["name"] != DBNull.Value) { routeName = row["name"].ToString(); }
                    listContainer.Add(new Container
                    {
                        idContainer = idContainer,
                        route = route,
                        containerArrive = containerArrive,
                        maxWeight = maxWeight,
                        uses = uses,
                        routeName = routeName,
                        idContainer_Manager = idContainer_Manager
                    }
                    );
                    rebootVarContainersInRoute(ref idContainer, ref route);
                }
            }
            return listContainer;
        }

        /*
         * private void rebootVarContainersInRoute(ref int idContainer, ref int route)
         * Auxiliar Method used in the Method getTableContainersInRoute, with the finality of reboot some variables
         */
        private void rebootVarContainersInRoute(ref int idContainer, ref int route)
        {
            idContainer = -1;
            route = -1;
        }

        /*
         * public override Container[] containerArrive(int idContainer, int route)
         * GET method for consult if a specified container in a specified route have arrived
         */
        public override Container[] containerArrive(int idContainer, int route)
        {
            HttpContext.Current.Cache.Remove(CacheKey);
            DataSet dataSet = mySQLConnection.makeQuery("CALL `Container_arrived`(" + idContainer + " , " + route + ");");
            List<Container> listContainer = getTableContainerArrive(dataSet, idContainer, route);
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

        /*
         * public List<Container> getTableContainerArrive(DataSet dataSet, int idContainer, int route)
         * Auxiliar method that return a dataSet with the data that  is need in the method containerArrive()
         */
        public List<Container> getTableContainerArrive(DataSet dataSet, int idContainer, int route)
        {
            int containerArrive = -1;
            int maxWeight = -1;
            int uses = -1;
            String routeName = "-";
            int idContainer_Manager = -1;
            List<Container> listContainer = new List<Container>();

            foreach (DataTable dataTable in dataSet.Tables)
            {
                foreach (DataRow row in dataTable.Rows)                {
                    if (dataTable.Columns.Contains("Container_arrived") && row["Container_arrived"] != DBNull.Value) { containerArrive = Convert.ToInt32(row["Container_arrived"]); }
                    listContainer.Add(new Container
                    {
                        idContainer = idContainer,
                        route = route,
                        containerArrive = containerArrive,
                        maxWeight = maxWeight,
                        uses = uses,
                        routeName = routeName,
                        idContainer_Manager = idContainer_Manager
                    }
                    );
                    rebootVarContainerArrive(ref containerArrive);
                }
            }
            return listContainer;
        }

        /*
         * private void rebootVarContainerArrive(ref int containerArrive)
         * Auxiliar Method used in the Method getTableContainerArrive, with the finality of reboot some variables
         */
        private void rebootVarContainerArrive(ref int containerArrive)
        {
            containerArrive = -1;
        }

        /*
         * public override Container[] containerVsRoute()
         * GET method that return a list with the number of uses, of a containers in a specified route.
         */
        public override Container[] containerVsRoute()
        {
            HttpContext.Current.Cache.Remove(CacheKey);
            DataSet dataSet = mySQLConnection.makeQuery("CALL `Container_Vs_Route`();");
            List<Container> listContainer = getTableContainerVsRoute(dataSet);
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

        /*
         * public List<Container> getTableContainerVsRoute(DataSet dataSet)
         * Auxiliar method that return a dataSet with the data that  is need in the method containerVsRoute()
         */
        public List<Container> getTableContainerVsRoute(DataSet dataSet)
        {
            int idContainer = -1;
            int route = -1;
            int containerArrive = -1;
            int maxWeight = -1;
            int uses = -1;
            int idContainer_Manager = -1;
            String routeName = "-";
            List<Container> listContainer = new List<Container>();
            foreach (DataTable dataTable in dataSet.Tables)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    if (dataTable.Columns.Contains("idContainer") && row["idContainer"] != DBNull.Value) { idContainer = Convert.ToInt32(row["idContainer"]); }
                    if (dataTable.Columns.Contains("Route") && row["Route"] != DBNull.Value) { routeName = row["Route"].ToString(); }
                    if (dataTable.Columns.Contains("Uses") && row["Uses"] != DBNull.Value) { uses = Convert.ToInt32(row["Uses"]); }
                    listContainer.Add(new Container
                    {
                        idContainer = idContainer,
                        route = route,
                        containerArrive = containerArrive,
                        maxWeight = maxWeight,
                        uses = uses,
                        routeName = routeName,
                        idContainer_Manager = idContainer_Manager
                    }
                    );
                    rebootVarContainerArrive(ref idContainer, ref routeName, ref uses);
                }
            }
            return listContainer;
        }

        /*
         * private void rebootVarContainerArrive(ref int idContainer, ref string routeName, ref int uses)
         * Auxiliar Method used in the Method getTableContainerVsRoute, with the finality of reboot some variables
         */
        private void rebootVarContainerArrive(ref int idContainer, ref string routeName, ref int uses)
        {
            idContainer = -1;
            uses = -1;
            routeName = "-";
        }

        /*
         * public override Container[] arrivalContainerNotNotified()
         * GET Method for return the containers that are'nt notified of arrival
         */
        public override Container[] arrivalContainerNotNotified()
        {
            HttpContext.Current.Cache.Remove(CacheKey);
            DataSet dataSet = mySQLConnection.makeQuery("CALL `ArrivalContainerNotNotified`();");
            List<Container> listContainer = getTableArrivalContainerNotNotified(dataSet);
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

        /*
         * public List<Container> getTableArrivalContainerNotNotified(DataSet dataSet)
         * Auxiliar method that return a dataSet with the data that  is need in the method arrivalContainerNotNotified()
         */
        public List<Container> getTableArrivalContainerNotNotified(DataSet dataSet)
        {
            int idContainer = -1;
            int route = -1;
            int containerArrive = -1;
            int maxWeight = -1;
            int uses = -1;
            int idContainer_Manager = -1;
            String routeName = "-";
            List<Container> listContainer = new List<Container>();
            foreach (DataTable dataTable in dataSet.Tables)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    if (dataTable.Columns.Contains("idContainer") && row["idContainer"] != DBNull.Value) { idContainer = Convert.ToInt32(row["idContainer"]); }
                    if (dataTable.Columns.Contains("idContainer_Manager") && row["idContainer_Manager"] != DBNull.Value) { idContainer_Manager = Convert.ToInt32(row["idContainer_Manager"]); }
                    listContainer.Add(new Container
                    {
                        idContainer = idContainer,
                        route = route,
                        containerArrive = containerArrive,
                        maxWeight = maxWeight,
                        uses = uses,
                        routeName = routeName,
                        idContainer_Manager = idContainer_Manager
                    }
                    );
                    rebootVarArrivalContainerNotNotified(ref idContainer, ref idContainer_Manager);
                }
            }
            return listContainer;
        }

        /*
         * private void rebootVarArrivalContainerNotNotified(ref int idContainer, ref int idContainer_Manager)
         * Auxiliar Method used in the Method getTableArrivalContainerNotNotified, with the finality of reboot some variables
         */
        private void rebootVarArrivalContainerNotNotified(ref int idContainer, ref int idContainer_Manager)
        {
            idContainer = -1;
            idContainer_Manager = -1;
        }

        /*
         * public override int setNotifiedContainerArrived(int idContainer)
         * PUT method for set notified the arrival of a container, return the state of the trasaction (200 = OK)
         */
        public override int setNotifiedContainerArrived(int idContainerManager)
        {
            mySQLConnection.makeQuery("CALL `SetNotifiedContainerArrived`( " + idContainerManager + ");");
            return HttpContext.Current.Response.StatusCode;
        }


        /*
         *  public Container[] GetContainer()
         *  GET Method for post in the cache a json array of elements
         */
        public Container[] GetContainer()
        {
            var ctx = HttpContext.Current;
            if (ctx != null)
            {
                return (Container[])ctx.Cache[CacheKey];
            }
            return null;

        }
    }
}