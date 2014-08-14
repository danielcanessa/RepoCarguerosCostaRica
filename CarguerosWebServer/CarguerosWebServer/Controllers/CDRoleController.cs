using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CarguerosWebServer.Services;
using CarguerosWebServer.Models;

namespace CarguerosWebServer.Controllers
{
    public class CDRoleController : ApiController
    {
        private CDRoleRepository roleRepository;

        public CDRoleController()
        {
            
            CDConcreteFactoryWebServer factory = CDConcreteFactoryWebServer.Instance;
            this.roleRepository = factory.CreateCDRoleRepository();
           
           
        }
       
        [HttpGet]
        [ActionName("1")]
        public Role[] allRole()
        {    
            return roleRepository.showAllRole();            
        }      
    }
}
