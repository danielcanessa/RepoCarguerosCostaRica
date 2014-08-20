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

        public override int createPackage(int weight,int size, String type, int price, String description,int account)
        {
            String aux_description = changeBacketsForSpace(description);
            String aux_type = changeBacketsForSpace(type);

            long idPackage = mySQLConnection.makePostQuery("INSERT INTO Packages SET weight = " + weight + ",size = " + size + " ,type = '" + type + "' ,price = " + price + " , description = '" + description + "';");
            int idPackageAux = Convert.ToInt32(idPackage);
            mySQLConnection.makeQuery("CALL `Register_Packages`(" + idPackage + "," + account + "," + price + ");");
            return idPackageAux;
        }

        public String changeBacketsForSpace( String word)
        {
            word = word.Replace('_', ' ');
            return word;
        }

        public override Packages[] detailsPackages(int account)
        {
            HttpContext.Current.Cache.Remove(CacheKey);
            DataSet dataSet = mySQLConnection.makeQuery("CALL `Details_Packages`(" + account + ");");
            List<Packages> listPackages = getTabledetailsPakages(dataSet, account);
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


        public List<Packages> getTabledetailsPakages(DataSet dataSet, int account)
        {
            int idPackages = -1;
            int price = -1;
            String costumer = "-";
            String packageState = "-";
            String containerArrivalDate = "-";
            String container = "-";
            String arrivalDate = "-";
            List<Packages> listPackages = new List<Packages>();
            foreach (DataTable dataTable in dataSet.Tables)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    if (dataTable.Columns.Contains("idPackages") && row["idPackages"] != DBNull.Value) { idPackages = Convert.ToInt32(row["idPackages"]); }
                    if (dataTable.Columns.Contains("costumer") && row["costumer"] != DBNull.Value) { costumer = row["costumer"].ToString(); }
                    if (dataTable.Columns.Contains("packageState") && row["packageState"] != DBNull.Value) { packageState = row["packageState"].ToString(); }
                    if (dataTable.Columns.Contains("price") && row["price"] != DBNull.Value) { price = Convert.ToInt32(row["price"]); }
                    if (dataTable.Columns.Contains("containerArrivalDate") && row["containerArrivalDate"] != DBNull.Value) { containerArrivalDate = row["containerArrivalDate"].ToString(); }
                    if (dataTable.Columns.Contains("container") && row["container"] != DBNull.Value) { container = row["container"].ToString(); }
                    if (dataTable.Columns.Contains("arrivalDate") && row["arrivalDate"] != DBNull.Value) { arrivalDate = row["arrivalDate"].ToString(); }
                    listPackages.Add(new Packages
                    {
                        account=account,
                        idPackages = idPackages,
                        customer = costumer,
                        packageState = packageState,
                        containerArrivalDate = containerArrivalDate,
                        container = container,
                        arrivalDate = arrivalDate,
                        weight = -1,
                        size = -1,
                        price = price,
                        type = "-",
                        description = "-"

                    }  
                    );
                    rebootVargetTabledetailsPakages(ref idPackages, ref price, ref costumer, ref packageState, ref containerArrivalDate, ref container, ref arrivalDate);
                }
            }
            return listPackages;
        }

        public void rebootVargetTabledetailsPakages(ref int idPackages, ref int price, ref  String costumer, ref  String packageState, 
            ref  String containerArrivalDate, ref  String container, ref  String arrivalDate)
        {
            idPackages = -1;
            price = -1;
            costumer = "-";
            packageState = "-";
            containerArrivalDate = "-";
            container = "-";
            arrivalDate = "-";
        }     


        public override Packages[] packagesUser(int account)
        {
            HttpContext.Current.Cache.Remove(CacheKey);
            DataSet dataSet = mySQLConnection.makeQuery("CALL  `User_packages`(" + account + ");");
            List<Packages> listPackages = getTablePackagesPerUser(dataSet, account);
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



        public List<Packages> getTablePackagesPerUser(DataSet dataSet, int account)
        {
            int idPackages=-1;
            String description="-";

            List<Packages> listPackages = new List<Packages>();
            foreach (DataTable dataTable in dataSet.Tables)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    if (dataTable.Columns.Contains("idPackages") && row["idPackages"] != DBNull.Value) { idPackages = Convert.ToInt32(row["idPackages"]); }
                    if (dataTable.Columns.Contains("description") && row["description"] != DBNull.Value) { description = row["description"].ToString(); }
                    listPackages.Add(new Packages
                    {
                        account=account,
                        idPackages = idPackages,
                        description = description,
                        weight = -1,
                        size = -1,
                        price = -1,
                        type = "-",
                        customer = "-",
                        packageState = "-",
                        containerArrivalDate = "-",
                        container = "-",
                        arrivalDate = "-"
                    }
                    );

                    rebootVargetTablePackagesPerUser(ref  idPackages, ref description); 
                }
            }
            return listPackages;
        }

        public void rebootVargetTablePackagesPerUser(ref int idPackages, ref  String description)
        {
            idPackages = -1;
            description = "-";
        }

        public Packages[] GetPackages()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Packages[])ctx.Cache[CacheKey];
            }
            return null;
        }       

    }
}