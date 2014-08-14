using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CarguerosWebServer.Services
{
    public abstract class CDAbstractFactory
    {
        public abstract CDContainerRepository CreateCDContainerRepository();
        public abstract CDCustomerRepository CreateCDCustomerRepository();
        public abstract CDEmployeeRepository CreateCDEmployeeRepository();
        public abstract CDPackagesRepository CreateCDPackagesRepository();
        public abstract CDBillingRepository CreateCDBillingRepository();
        public abstract CDContainerManagerRepository CreateCDContainerManagerRepository();
        public abstract CDContainerManagerHasPackagesRepository CreateCDContainerManagerHasPackagesRepository();
        public abstract CDCustomerHasPackagesRepository CreateCDCustomerHasPackagesRepository();
        public abstract CDCustomerTypeRepository CreateCDCustomerTypeRepository();      
        public abstract CDPersonRepository CreateCDPersonRepository();
        public abstract CDRoleRepository CreateCDRoleRepository();
        public abstract CDRouteRepository CreateCDRouteRepository();
        public abstract CDStorageRepository CreateCDStorageRepository();

        
    }
}