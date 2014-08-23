using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarguerosWebServer.Models;
using System.Data;
using MySql.Data.MySqlClient;

namespace CarguerosWebServer.Services
{
    public class CDAcessStorage : CDStorageRepository
    {
        //Cache key that represent Packages
        public const string CacheKey = "StorageStore";
        //Object of the class CDMYSQLConecctions
        CDMySQLConnection mySQLConnection = CDMySQLConnection.Instance;

        public CDAcessStorage()
        {          
        }

        /*
         * public override Storage[] showPackageInStorage()
         * GET method that return a list of all package in storage
         */
        public override Storage[] showPackageInStorage()
        {
            HttpContext.Current.Cache.Remove(CacheKey);
            DataSet dataSet = mySQLConnection.makeQuery("CALL `View_Storage_Packages`();");
            List<Storage> listStorage = getTableleShowPackageInStorage(dataSet);
            var ctx = HttpContext.Current;
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    ctx.Cache[CacheKey] = listStorage.ToArray();
                }
            }
            return GetStorage();
        }

        /*
         * public List<Storage> getTableleShowPackageInStorage(DataSet dataSet)
         * Auxiliar method that return a dataSet with the data that  is need in the method showPackageInStorage()
         */
        public List<Storage> getTableleShowPackageInStorage(DataSet dataSet)
        {
            int packagesIdPackages = -1;
            int size = -1;
            double price = -1;
            String arrivalDate = "-";
            String costumer = "-";
            String type = "-";            
            String departureDate = "-";
            int waitingAverage = -1;
            int countStorage = -1;      
            List<Storage> listStorage = new List<Storage>();
            foreach (DataTable dataTable in dataSet.Tables)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    if (dataTable.Columns.Contains("idPackages") && row["idPackages"] != DBNull.Value) { packagesIdPackages = Convert.ToInt32(row["idPackages"]); }
                    if (dataTable.Columns.Contains("size") && row["size"] != DBNull.Value) { size = Convert.ToInt32(row["size"]); }
                    if (dataTable.Columns.Contains("price") && row["price"] != DBNull.Value)
                    { price = float.Parse(row["price"].ToString(), System.Globalization.CultureInfo.InvariantCulture); }
                    if (dataTable.Columns.Contains("Estimated_Date_Arrival") && row["Estimated_Date_Arrival"] != DBNull.Value) { arrivalDate = row["Estimated_Date_Arrival"].ToString(); }
                    if (dataTable.Columns.Contains("type") && row["type"] != DBNull.Value) { type = row["type"].ToString(); }
                    if (dataTable.Columns.Contains("Costumer") && row["Costumer"] != DBNull.Value) { costumer = row["Costumer"].ToString(); }
                    listStorage.Add(new Storage
                    {
                        packagesIdPackages = packagesIdPackages,
                        arrivalDate = arrivalDate,
                        departureDate = departureDate,
                        waitingAverage = waitingAverage,
                        countStorage = countStorage,
                        costumer = costumer,
                        price = price,
                        type = type,
                        size = size
                    }
                    );
                    rebootVarShowPackageInStorage(ref packagesIdPackages, ref size, ref price, ref arrivalDate, ref costumer, ref type);
                }
            }
            return listStorage;
        }

        /*
         * private void rebootVarShowPackageInStorage(ref int packagesIdPackages, ref int size, ref double price, ref string arrivalDate, ref string costumer, ref string type)
         * Auxiliar Method used in the Method getTableleShowPackageInStorage, with the finality of reboot some variables
         */
        private void rebootVarShowPackageInStorage(ref int packagesIdPackages, ref int size, ref double price, ref string arrivalDate, ref string costumer, ref string type)
        {
             packagesIdPackages = -1;
             size = -1;
             price = -1;
             arrivalDate = "-";
             costumer = "-";
             type = "-";            
        }

        /*
         * public override Storage[] averagePackageStorage()
         * GET method that return the average time  of the packages in storage
         */
        public override Storage[] averagePackageStorage()
        {
            HttpContext.Current.Cache.Remove(CacheKey);
            DataSet dataSet = mySQLConnection.makeQuery("CALL `Average_Packet_Storage`();");
            List<Storage> listStorage = getTableleAveragePacketStorage(dataSet);
            var ctx = HttpContext.Current;
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    ctx.Cache[CacheKey] = listStorage.ToArray();
                }
            }
            return GetStorage();
        }

        /*
         * public List<Storage> getTableleAveragePacketStorage(DataSet dataSet)
         * Auxiliar method that return a dataSet with the data that  is need in the method averagePackageStorage()
         */
        public List<Storage> getTableleAveragePacketStorage(DataSet dataSet)
        {
            int packagesIdPackages = -1;
            String arrivalDate = "-";
            String departureDate = "-";
            int waitingAverage = 0;
            int countStorage = -1;
            String costumer = "-";
            double price = -1;
            String type = "-";
            int size = -1;
            List<Storage> listStorage = new List<Storage>();
            foreach (DataTable dataTable in dataSet.Tables)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    if (dataTable.Columns.Contains("countPackages") && row["countPackages"] != DBNull.Value) { countStorage = Convert.ToInt32(row["countPackages"]); }
                    if (dataTable.Columns.Contains("Waiting_Average") && row["Waiting_Average"] != DBNull.Value) { waitingAverage = Convert.ToInt32(row["Waiting_Average"]); }
                    listStorage.Add(new Storage
                    {
                        packagesIdPackages = packagesIdPackages,
                        arrivalDate = arrivalDate,
                        departureDate = departureDate,
                        waitingAverage = waitingAverage,
                        countStorage = countStorage,
                        costumer = costumer,
                        price = price,
                        type = type,
                        size = size
                    }
                    );
                    rebootVarAveragePacketStorage(ref countStorage, ref waitingAverage);
                }
            }
            return listStorage;
        }

        /*
         * private void rebootVarAveragePacketStorage(ref int countStorage, ref int waitingAverage)
         * Auxiliar Method used in the Method getTableleAveragePacketStorage, with the finality of reboot some variables
         */
        private void rebootVarAveragePacketStorage(ref int countStorage, ref int waitingAverage)
        {
            countStorage = -1;
            waitingAverage = 0;
        }

        /*
         *  public Storage[] GetStorage()
         *  GET Method for post in the cache a json array of elements
         */
        public Storage[] GetStorage()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Storage[])ctx.Cache[CacheKey];
            }
            return null;        
        }       
    }
}