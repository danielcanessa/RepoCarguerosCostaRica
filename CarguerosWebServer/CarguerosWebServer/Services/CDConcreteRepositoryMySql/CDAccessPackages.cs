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

        public static void ClearCacheItems()
        {
            var enumerator = HttpContext.Current.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                HttpContext.Current.Cache.Remove(enumerator.Key.ToString());
            }
        }

        public override Packages[] detailsPackages()
        {
            ClearCacheItems();
            DataSet dataSet = mySQLConnection.makeQuery("CALL `Details_Packages`(" + "1" + ");");
            List<Packages> listPackages = getTabledetailsPakages(dataSet);
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

        public List<Packages> getTabledetailsPakages(DataSet dataSet)
        {
            int idPackages = 0; int price = 0;
            String costumer = "-"; String packageState = "-"; String containerArrivalDate = "-"; String container = "-"; String arrivalDate = "-";
            List<Packages> listPackages = new List<Packages>();
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {

                    try
                    {
                        idPackages = Convert.ToInt32(row[0]);
                    }
                    catch (Exception) { }

                    try
                    {
                        costumer = row[1].ToString();
                    }
                    catch (Exception) { }

                    try
                    {
                        packageState = row[2].ToString();
                    }
                    catch (Exception) { }

                    try
                    {
                        price = Convert.ToInt32(row[3]);
                    }
                    catch (Exception) { }

                    try
                    {
                        containerArrivalDate = row[4].ToString();
                    }
                    catch (Exception) { }

                    try
                    {
                        container = row[5].ToString();
                    }
                    catch (Exception) { }

                    try
                    {
                        arrivalDate = row[6].ToString();
                    }
                    catch (Exception) { }

                    listPackages.Add(new Packages
                    {
                        idPackages = idPackages,
                        weight = 0,
                        size = 0,
                        price = price,
                        type = "-",
                        customer = costumer,
                        packageState = packageState,
                        containerArrivalDate = containerArrivalDate,
                        container = container,
                        arrivalDate = arrivalDate,
                        description = "-"

                    }
                    );
                }
            }
            return listPackages;
        }


        public override Packages[] packagesUser(int account)
        {
            ClearCacheItems();
            DataSet dataSet = mySQLConnection.makeQuery("CALL  `User_packages`(" + account + ");");
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
            int idPackages = 0;
            String description = "";
            List<Packages> listPackages = new List<Packages>();
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    try
                    {
                        idPackages = Convert.ToInt32(row[0]);
                    }
                    catch (Exception) { }
                    try
                    {
                        description = row[1].ToString();
                    }
                    catch (Exception) { }
                    listPackages.Add(new Packages
                    {
                        idPackages = idPackages,
                        weight = 0,
                        size = 0,
                        price = 0,
                        type = "-",
                        customer = "-",
                        packageState = "-",
                        containerArrivalDate = "-",
                        container = "-",
                        arrivalDate = "-",
                        description = description
                    }
                    );
                }
            }
            return listPackages;
        }

        public override Packages[] showAllPackages()
        {
            ClearCacheItems();
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

        public List<Packages> getTablePackages(DataSet dataSet)
        {
            int idPackages = 0;
            int weight = 0;
            int size = 0;
            int price = 0;
            String type = "";
            String description = "";

            List<Packages> listPackages = new List<Packages>();
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    try
                    {
                        idPackages = Convert.ToInt32(row[0]);
                    }
                    catch (Exception) { }

                    try
                    {
                        weight = Convert.ToInt32(row[1]);
                    }
                    catch (Exception) { }

                    try
                    {
                        size = Convert.ToInt32(row[2]);
                    }
                    catch (Exception) { }

                    try
                    {
                        price = Convert.ToInt32(row[3]);
                    }
                    catch (Exception) { }

                    try
                    {
                        type = row[4].ToString();
                    }
                    catch (Exception) { }

                    try
                    {
                        description = row[5].ToString();
                    }
                    catch (Exception) { }

                    listPackages.Add(new Packages
                    {
                        idPackages = idPackages,
                        weight = weight,
                        size = size,
                        price = price,
                        type = type,
                        description = description,
                        customer = "-",
                        packageState = "-",
                        containerArrivalDate = "-",
                        container = "-",
                        arrivalDate = "-"

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
                description = "-",
                customer = "-",
                packageState = "-",
                containerArrivalDate = "-",
                container = "-",
                arrivalDate = "-"
            }
        };
        }

    }
}