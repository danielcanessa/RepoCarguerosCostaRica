using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarguerosWebServer.Models;

namespace CarguerosWebServer.Services
{
    /*
     * public abstract class CDRouteRepository
     * Interface for implement in CDAcessRoute
     */
    public abstract class CDRouteRepository
    {
        public abstract int createRoute(String name, String exitPoint, String arrivalPoint, String price, int duration, int maxAmount);
        public abstract Route[] showRoutes();
        public abstract Route[] bestRoutes(int ammount);
        public abstract Route[] worstRoutes(int ammount);
    }
}