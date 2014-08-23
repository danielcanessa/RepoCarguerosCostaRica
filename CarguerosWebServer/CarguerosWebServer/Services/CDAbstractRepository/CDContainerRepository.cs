using CarguerosWebServer.Models;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarguerosWebServer.Services
{
    /*
     * public abstract class CDContainerRepository
     * Interface for implement in CDAcessContainer
     */
    public abstract class CDContainerRepository
    {
        public abstract Container[] containerArrive(int idContainer, int route);
        public abstract int createContainer(String weight);
        public abstract Container[] leastUsedContainers(int ammount);
        public abstract Container[] mostUsedContainers(int ammount);
        public abstract Container[] containersInRoute();
        public abstract Container[] containerVsRoute();
    }
}