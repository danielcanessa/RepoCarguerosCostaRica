﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarguerosWebServer.Models;

namespace CarguerosWebServer.Services
{
    public abstract class CDPersonRepository
    {
        public abstract Person[] showAllPerson();
    }
}