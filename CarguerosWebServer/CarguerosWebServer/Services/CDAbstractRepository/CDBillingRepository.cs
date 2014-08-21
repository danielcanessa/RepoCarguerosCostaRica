using CarguerosWebServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarguerosWebServer.Services
{
    /*
     * public abstract class CDBillingRepository
     * Interface for implement in CDAcessBilling
     */
    public abstract class CDBillingRepository
    {      
        public abstract Billing[] showcostumerBilling(int account);
    }
}