using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarguerosWebServer.Models;

namespace CarguerosWebServer.Services
{
    /*
     * public abstract class CDStorageRepository
     * Interface for implement in CDAcessStorage
     */
    public abstract class CDStorageRepository
    {
        public abstract Storage[] averagePackageStorage();
        public abstract Storage[] showPackageInStorage();
    }
}