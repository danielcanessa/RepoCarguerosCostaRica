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

        public override Route[] showAllRoute()
        {
            DataSet dataSet = mySQLConnection.makeQuery("SELECT * FROM universidad.estudiante;"); 
            List<Route> listRoute = getTableRoute(dataSet);    
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



        public  List<Route> getTableRoute(DataSet dataSet)
        {
            List<Route> listRoute = new List<Route>();
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    int idRoute = Convert.ToInt32(row[0]);
                    int cost = Convert.ToInt32(row[1]);
                    int duration = Convert.ToInt32(row[2]);
                    int maxAmount = Convert.ToInt32(row[3]);
                    int customerAccount = Convert.ToInt32(row[4]);
                    String name = row[5].ToString();
                    String exitPoint = row[6].ToString();
                    String arrivalPoint = row[7].ToString();
                                  
                    listRoute.Add(new Route{
                        idRoute = idRoute,
                        cost = cost,
                        duration = duration,
                        maxAmount = maxAmount,
                        customerAccount = customerAccount,
                        name = name,
                        exitPoint = exitPoint,
                        arrivalPoint = arrivalPoint                       
                    }
                    );
                }
            }
            return listRoute;
        }

        public Route[] GetRoute()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Route[])ctx.Cache[CacheKey];
            }
            return new Route[]
        {
            new Route
            {
                idRoute = 0,
                cost = 0,
                duration = 0,
                maxAmount = 0,
                customerAccount = 0,
                name = "",
                exitPoint = "",
                arrivalPoint = ""   
            }
        };
        }       
    }
}