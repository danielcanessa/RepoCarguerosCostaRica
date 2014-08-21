 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CarguerosWebServer.Services
{
    public class CDConcreteFactoryWebServer : CDAbstractFactory
    {
        //variables
        private static CDConcreteFactoryWebServer instance;

        //singleton
        private CDConcreteFactoryWebServer() { }

        public static CDConcreteFactoryWebServer Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new CDConcreteFactoryWebServer();
                }
                return instance;
            }
        }

        //interface methods
        public override CDBillingRepository CreateCDBillingRepository()
        {
            return new CDAccessBilling();
        }

        public override CDPackagesRepository CreateCDPackagesRepository()
        {
            return new CDAccessPackages();
        }

        public override CDContainerRepository CreateCDContainerRepository()
        {
            return new CDAccessContainer();
        }

        public override CDEmployeeRepository CreateCDEmployeeRepository()
        {
            return new CDAccessEmployee();
        }

        public override CDCustomerRepository CreateCDCustomerRepository()
        {
            return new CDAccessCustomer();
        }
        
        public override CDCustomerHasPackagesRepository CreateCDCustomerHasPackagesRepository()
        {
            return new CDAcessCustomerHasPackages();
        }        
        public override CDRouteRepository CreateCDRouteRepository()
        {
            return new CDAcessRoute();
        }
        public override CDStorageRepository CreateCDStorageRepository()
        {
            return new CDAcessStorage();
        }


    

        

    }
}