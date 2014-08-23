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
        //Cache key that represent Packages
        public const string CacheKey = "PackagesStore";
        //Object of the class CDMYSQLConecctions
        CDMySQLConnection mySQLConnection = CDMySQLConnection.Instance;

        public CDAccessPackages()
        {
        }

        /*
         * public override int createPackage(int weight,int size, String type, String price, String description,int account)
         * POST method for create new packages, return the id of the new packages
         */
        public override int createPackage(int weight,int size, String type, String price, String description,int account)
        {
            String aux_description = changeBacketsForSpace(description);
            String aux_type = changeBacketsForSpace(type);
            long idPackage = mySQLConnection.makePostQuery("INSERT INTO Packages SET weight = " + weight + ",size = " + size + " ,type = '" + type + "' ,price = '" + price + "' , description = '" + description + "';");
            int idPackageAux = Convert.ToInt32(idPackage);
            mySQLConnection.makeQuery("CALL `Register_Packages`(" + idPackage + "," + account + "," + price + ");");
            return idPackageAux;
        }

        /*
         * public String changeBacketsForSpace( String word)
         * Method that change '_' for ' ' in a String
         */
        public String changeBacketsForSpace( String word)
        {
            word = word.Replace('_', ' ');
            return word;
        }

        /*
         * public override Packages[] detailsPackages(int account)
         * GET method that return a list with all the details of exactly package (account = idPackage .... correct in the future)
         */
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

        /*
         * public List<Packages> getTabledetailsPakages(DataSet dataSet, int account)
         * Auxiliar method that return a dataSet with the data that  is need in the method detailsPackages(int account)
         */
        public List<Packages> getTabledetailsPakages(DataSet dataSet, int account)
        {
            int idPackages = -1;
            double price = -1;
            String costumer = "-";
            String packageState = "-";
            String containerArrivalDate = "-";
            String container = "-";
            String arrivalDate = "-";
            int size = -1;
            int weight = -1;
            String type = "-";            
            List<Packages> listPackages = new List<Packages>();
            foreach (DataTable dataTable in dataSet.Tables)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    if (dataTable.Columns.Contains("idPackages") && row["idPackages"] != DBNull.Value) { idPackages = Convert.ToInt32(row["idPackages"]); }
                    if (dataTable.Columns.Contains("Costumer") && row["Costumer"] != DBNull.Value) { costumer = row["Costumer"].ToString(); }
                    if (dataTable.Columns.Contains("package_state") && row["package_state"] != DBNull.Value) { packageState = row["package_state"].ToString(); }
                    if (dataTable.Columns.Contains("price") && row["price"] != DBNull.Value)
                    { price = float.Parse(row["price"].ToString(), System.Globalization.CultureInfo.InvariantCulture); }
                    if (dataTable.Columns.Contains("Container_arrivalDate") && row["Container_arrivalDate"] != DBNull.Value) { containerArrivalDate = row["Container_arrivalDate"].ToString(); }
                    if (dataTable.Columns.Contains("Container") && row["Container"] != DBNull.Value) { container = row["Container"].ToString(); }
                    if (dataTable.Columns.Contains("Estimated_Date_Arrival") && row["Estimated_Date_Arrival"] != DBNull.Value) { arrivalDate = row["Estimated_Date_Arrival"].ToString(); }
                    if (dataTable.Columns.Contains("type") && row["type"] != DBNull.Value) { type = row["type"].ToString(); }
                    if (dataTable.Columns.Contains("size") && row["size"] != DBNull.Value) { size = Convert.ToInt32(row["size"]); }
                    if (dataTable.Columns.Contains("weight") && row["weight"] != DBNull.Value) { weight = Convert.ToInt32(row["weight"]); }
                    listPackages.Add(new Packages
                    {
                        account=account,
                        idPackages = idPackages,
                        customer = costumer,
                        packageState = packageState,
                        containerArrivalDate = containerArrivalDate,
                        container = container,
                        arrivalDate = arrivalDate,
                        weight = weight,
                        size = size,
                        price = price,
                        type = type,
                        description = "-"
                    }  
                    );
                    rebootVargetTabledetailsPakages(ref idPackages, ref price, ref costumer, ref packageState, ref containerArrivalDate, ref container, ref arrivalDate);
                }
            }
            return listPackages;
        }

        /*
         * public void rebootVargetTabledetailsPakages(ref int idPackages, ref double price, ref  String costumer, ref  String packageState,
         * ref  String containerArrivalDate, ref  String container, ref  String arrivalDate)
         * Auxiliar Method used in the Method getTabledetailsPakages, with the finality of reboot some variables
         */
        public void rebootVargetTabledetailsPakages(ref int idPackages, ref double price, ref  String costumer, ref  String packageState, 
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

        /*
         * public override Packages[] packagesUser(int account)
         * GET method that return a list of all the package and some information about each package of an exactly user
         */
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

        /*
         * public List<Container> getTableContainersInRoute(DataSet dataSet)
         * Auxiliar method that return a dataSet with the data that  is need in the method containersInRoute()
         */
        public List<Packages> getTablePackagesPerUser(DataSet dataSet, int account)
        {
            int idPackages=-1;
            String description="-";            
            double price = -1;           
            String packageState = "-";
            String containerArrivalDate = "-";
            String container = "-";
            String arrivalDate = "-";
            int size = -1;
            int weight = -1;
            String type = "-";
            String customer = "-";

            List<Packages> listPackages = new List<Packages>();
            foreach (DataTable dataTable in dataSet.Tables)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    if (dataTable.Columns.Contains("idPackages") && row["idPackages"] != DBNull.Value) { idPackages = Convert.ToInt32(row["idPackages"]); }
                    if (dataTable.Columns.Contains("description") && row["description"] != DBNull.Value) { description = row["description"].ToString(); }

                    if (dataTable.Columns.Contains("package_state") && row["package_state"] != DBNull.Value) { packageState = row["package_state"].ToString(); }
                    if (dataTable.Columns.Contains("price") && row["price"] != DBNull.Value)
                    {
                        price = float.Parse(row["price"].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                    }
                    if (dataTable.Columns.Contains("Container_arrivalDate") && row["Container_arrivalDate"] != DBNull.Value) { containerArrivalDate = row["Container_arrivalDate"].ToString(); }
                    if (dataTable.Columns.Contains("Container") && row["Container"] != DBNull.Value) { container = row["Container"].ToString(); }
                    if (dataTable.Columns.Contains("Estimated_Date_Arrival") && row["Estimated_Date_Arrival"] != DBNull.Value) { arrivalDate = row["Estimated_Date_Arrival"].ToString(); }
                    if (dataTable.Columns.Contains("type") && row["type"] != DBNull.Value) { type = row["type"].ToString(); }
                    if (dataTable.Columns.Contains("size") && row["size"] != DBNull.Value) { size = Convert.ToInt32(row["size"]); }
                    if (dataTable.Columns.Contains("weight") && row["weight"] != DBNull.Value) { weight = Convert.ToInt32(row["weight"]); }


                    listPackages.Add(new Packages
                    {
                        account=account,                        
                        description = description,                                       
                        idPackages = idPackages,                        
                        packageState = packageState,
                        containerArrivalDate = containerArrivalDate,
                        container = container,
                        arrivalDate = arrivalDate,
                        weight = weight,
                        size = size,
                        price = price,
                        type = type,      
                        customer = customer
                       
                    }
                    );

                    rebootVargetTablePackagesPerUser(ref  description,ref  idPackages, ref  packageState,ref containerArrivalDate ,ref container ,ref arrivalDate ,ref weight ,ref size ,ref price  ,ref  type); 
                }
            }
            return listPackages;
        }

        private void rebootVargetTablePackagesPerUser( ref string description, ref int idPackages, ref string packageState, ref string containerArrivalDate, 
            ref string container, ref string arrivalDate, ref int weight, ref int size, ref double price, ref string type)
        {
             idPackages = -1;
             description = "-";
             price = -1;
             packageState = "-";
             containerArrivalDate = "-";
             container = "-";
             arrivalDate = "-";
             size = -1;
             weight = -1;
             type = "-";
           
        }

        public override Packages[] packageArrived()
        {
            HttpContext.Current.Cache.Remove(CacheKey);
            DataSet dataSet = mySQLConnection.makeQuery("CALL `Packages_arrived`();");
            List<Packages> listPackages = getTablePackageArrived(dataSet);
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

        public List<Packages> getTablePackageArrived(DataSet dataSet)
        {
            int idPackages = -1;
            int weight = -1;  
            int size = -1;     
            double price = -1;     
            int account = -1;
            String type = "-";
            String description = "-"; 
            String customer = "-";
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
                    if (dataTable.Columns.Contains("Description") && row["Description"] != DBNull.Value) { description = row["Description"].ToString(); }

                 
                    if (dataTable.Columns.Contains("price") && row["price"] != DBNull.Value)
                    {
                        price = float.Parse(row["price"].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                    }
                    if (dataTable.Columns.Contains("type") && row["type"] != DBNull.Value) { type = row["type"].ToString(); }
                    if (dataTable.Columns.Contains("size") && row["size"] != DBNull.Value) { size = Convert.ToInt32(row["size"]); }
                    if (dataTable.Columns.Contains("weight") && row["weight"] != DBNull.Value) { weight = Convert.ToInt32(row["weight"]); }


                    listPackages.Add(new Packages
                    {
                        account = account,
                        description = description,                       
                        idPackages = idPackages,
                        packageState = packageState,
                        containerArrivalDate = containerArrivalDate,
                        container = container,
                        arrivalDate = arrivalDate,
                        weight = weight,
                        size = size,
                        price = price,
                        type = type,
                        customer = customer

                    }
                    );

                    rebootVargetTablePackageArrived(ref size, ref price, ref  type, ref weight, ref idPackages, ref description);
                }
            }
            return listPackages;
        }

        private void rebootVargetTablePackageArrived(ref int size, ref double price, ref string type, ref int weight, ref int idPackages, ref string description)
        {
            size = -1;
            price = -1;
            type = "-";
            weight = -1;
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