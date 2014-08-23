using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace CarguerosWebServer.Services
{
    public class CDMySQLConnection
    {
        private static CDMySQLConnection instance;
        private static MySqlConnection connectionMySQL;

        //singleton
        private CDMySQLConnection() { }
       
        public static CDMySQLConnection Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new CDMySQLConnection();
                }
                if (connectionMySQL == null)
                {
                    connectionMySQL = new MySqlConnection("server=cargodispatcherdb.csmwl817fkzh.us-west-1.rds.amazonaws.com; user id=dcanessa; password=GUMS2011; database=CargoDispatcher; pooling=TRUE;");
                //    connectionMySQL = new MySqlConnection("server=localhost; user id=root; password=dcanessa; database=billing; pooling=false;");
               
                }
                return instance;
            }
        }

        public DataSet makeQuery(String procedure)
        {

            
                MySqlDataAdapter adapterSQL = new MySqlDataAdapter(procedure, connectionMySQL);

                DataSet dataSetResult = new DataSet();
                adapterSQL.Fill(dataSetResult);

                return dataSetResult;
           
        }

        public long makePostQuery(String procedure)
        {
            
            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = connectionMySQL;
            cmd.Connection.Open();

            string commandLine = procedure;
            cmd.CommandText = commandLine;

          

            cmd.ExecuteNonQuery();
   
                 

            cmd.Connection.Close();
            
            return cmd.LastInsertedId;
           
        }

    }
}