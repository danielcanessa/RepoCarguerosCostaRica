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


        public override Container[] showAllContainer()
        {
            DataSet dataSet = mySQLConnection.makeQuery("SELECT * FROM universidad.estudiante;");  
            List<Container> listContainer = getTableContainer(dataSet);    
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

        public  List<Container> getTableContainer(DataSet dataSet)
        {
            List<Container> listContainer = new List<Container>();
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    int idContainer = Convert.ToInt32(row[0]);
                    int maxWeight = Convert.ToInt32(row[1].ToString());                                
                    listContainer.Add(new Container{
                        idContainer = idContainer,
                        maxWeight = maxWeight,                        
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