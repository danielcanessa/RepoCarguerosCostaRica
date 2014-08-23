using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarguerosWebServer.Models;
using System.Data;
using MySql.Data.MySqlClient;

namespace CarguerosWebServer.Services
{
    public class CDAcessRoute : CDRouteRepository
    {
         public const string CacheKey = "RouteStore";
        CDMySQLConnection mySQLConnection = CDMySQLConnection.Instance;

        public CDAcessRoute()
        {    
      
        }

        public override int createRoute(String name, String exitPoint, String arrivalPoint , String price, int duration, int maxAmount)
        {
          
            mySQLConnection.makeQuery("CALL `Create_Route`('" + name + "', '" + exitPoint + "','" + arrivalPoint + "','" + price+ "'," + duration + "," + maxAmount + ");");
            return HttpContext.Current.Response.StatusCode;
        }

        public override Route[] bestRoutes(int ammount)
        {
            HttpContext.Current.Cache.Remove(CacheKey);
            DataSet dataSet = mySQLConnection.makeQuery("CALL `Best_Routes`("+ammount+");");
            List<Route> listRoute = getTableBestWorstRoutes(dataSet);
            var ctx = HttpContext.Current;
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    ctx.Cache[CacheKey] = listRoute.ToArray();
                }
            }
            return GetRoute();
        }

        public override Route[] worstRoutes(int ammount)
        {
            HttpContext.Current.Cache.Remove(CacheKey);
            DataSet dataSet = mySQLConnection.makeQuery("CALL `Worst_Routes`("+ammount+");");
            List<Route> listRoute = getTableBestWorstRoutes(dataSet);
            var ctx = HttpContext.Current;
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    ctx.Cache[CacheKey] = listRoute.ToArray();
                }
            }
            return GetRoute();
        }

       


        public List<Route> getTableBestWorstRoutes(DataSet dataSet)
        {
            int idRoute = -1;
            double cost = -1;
            int duration = -1;
            int maxAmount = -1;
            int uses = -1;
            int customerAccount = -1;
            String name = "-";
            String exitPoint = "-";
            String arrivalPoint = "-";
            List<Route> listRoute = new List<Route>();

            foreach (DataTable dataTable in dataSet.Tables)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    if (dataTable.Columns.Contains("name") && row["name"] != DBNull.Value) { name = row["name"].ToString(); }
                    if (dataTable.Columns.Contains("Uses") && row["Uses"] != DBNull.Value) { uses = Convert.ToInt32(row["Uses"]); }
                    listRoute.Add(new Route
                    {
                        idRoute = idRoute,
                        cost = cost,
                        duration = duration,
                        maxAmount = maxAmount,
                        customerAccount = customerAccount,
                        name = name,
                        exitPoint = exitPoint,
                        arrivalPoint = arrivalPoint,
                        uses = uses
                    });
                    rebootVarBestWorstRoutes(ref  name, ref  uses);
                }
            }
            return listRoute;
        }

        private void rebootVarBestWorstRoutes(ref string name, ref int uses)
        {
            name = "-";
            uses = -1;
        }

        public override Route[] showRoutes()
        {
            HttpContext.Current.Cache.Remove(CacheKey);
            DataSet dataSet = mySQLConnection.makeQuery("CALL `View_Routes` ();");
            List<Route> listRoute = getTableShowRoute(dataSet);
            var ctx = HttpContext.Current;
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    ctx.Cache[CacheKey] = listRoute.ToArray();
                }
            }
            return GetRoute();
        }

      
        
        public List<Route> getTableShowRoute(DataSet dataSet)
        {
            int idRoute = -1;
            double cost  = -1;
            int duration  = -1;
            int maxAmount  = -1;
            int uses = -1;
            int customerAccount  = -1;
            String name = "-";
            String exitPoint = "-";
            String arrivalPoint = "-";           
            List<Route> listRoute = new List<Route>();

            foreach (DataTable dataTable in dataSet.Tables)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    if (dataTable.Columns.Contains("idRoute") && row["idRoute"] != DBNull.Value) { idRoute = Convert.ToInt32(row["idRoute"]); }
                    if (dataTable.Columns.Contains("duration") && row["duration"] != DBNull.Value) { duration = Convert.ToInt32(row["duration"]); }
                    if (dataTable.Columns.Contains("max_amount") && row["max_amount"] != DBNull.Value) { maxAmount = Convert.ToInt32(row["max_amount"]); }
                    if (dataTable.Columns.Contains("customerAccount") && row["customerAccount"] != DBNull.Value) { customerAccount = Convert.ToInt32(row["customerAccount"]); }
                    if (dataTable.Columns.Contains("cost") && row["cost"] != DBNull.Value)
                    { 
                        cost =  float.Parse(row["cost"].ToString(), System.Globalization.CultureInfo.InvariantCulture); 
                    }
                    if (dataTable.Columns.Contains("name") && row["name"] != DBNull.Value) { name = row["name"].ToString(); ; }
                    if (dataTable.Columns.Contains("exit_point") && row["exit_point"] != DBNull.Value) { exitPoint = row["exit_point"].ToString(); ; }
                    if (dataTable.Columns.Contains("arrival_point") && row["arrival_point"] != DBNull.Value) { arrivalPoint = row["arrival_point"].ToString(); ; }
                  

                    listRoute.Add(new Route
                    {
                          idRoute = idRoute,
                          cost  = cost,
                          duration  = duration,
                          maxAmount  = maxAmount,
                          customerAccount  = customerAccount,
                          name = name,
                          exitPoint = exitPoint,
                          arrivalPoint = arrivalPoint,
                          uses = uses
                    });
                    rebootVarShowRoute(ref  idRoute, ref  cost, ref  duration, ref  maxAmount, ref  customerAccount, ref  name, ref  exitPoint, ref  arrivalPoint);
                }
            }
            return listRoute;
        }

        private void rebootVarShowRoute(ref int idRoute, ref double cost, ref int duration, ref int maxAmount, ref int customerAccount, ref string name, ref string exitPoint, ref string arrivalPoint)
        {
            idRoute = -1;
            cost = -1;
            duration = -1;
            maxAmount = -1;
            customerAccount = -1;
            name = "-";
            exitPoint = "-";
            arrivalPoint = "-";   
           
        }

        public Route[] GetRoute()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Route[])ctx.Cache[CacheKey];
            }
            return null;
       
        }       
    }
}