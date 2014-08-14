using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarguerosWebServer.Models;
using System.Data;
using MySql.Data.MySqlClient;

namespace CarguerosWebServer.Services
{
    public class CDAcessPerson : CDPersonRepository
    {
        
        public const string CacheKey = "PersonStore";
        CDMySQLConnection mySQLConnection = CDMySQLConnection.Instance;

        public CDAcessPerson()
        {    
      
        }

        public override Person[] showAllPerson()
        {
            DataSet dataSet = mySQLConnection.makeQuery("SELECT * FROM universidad.estudiante;"); 
            List<Person> listPerson = getTablePerson(dataSet);    
            var ctx = HttpContext.Current;            
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    ctx.Cache[CacheKey] = listPerson.ToArray();                     
                }
            }
            return GetPerson();
        }



        public  List<Person> getTablePerson(DataSet dataSet)
        {
            List<Person> listPerson = new List<Person>();
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {      

                    int idPerson = Convert.ToInt32(row[0]);
                    String name = row[1].ToString();                    
                    String lastName = row[2].ToString();
                    String telephone = row[3].ToString();
                                  
                    listPerson.Add(new Person{
                        idPerson = idPerson,
                        name = name,
                        lastName = lastName,
                        telephone = telephone
                       
                    }
                    );
                }
            }
            return listPerson;
        }

        public Person[] GetPerson()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Person[])ctx.Cache[CacheKey];
            }
            return new Person[]
        {
            new Person
            {
                 idPerson = 0,
                 name = "",
                 lastName = "",
                 telephone = ""
            }
        };
        }       

    }
}