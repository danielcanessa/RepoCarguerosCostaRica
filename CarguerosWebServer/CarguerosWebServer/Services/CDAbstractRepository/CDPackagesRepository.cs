﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarguerosWebServer.Models;

namespace CarguerosWebServer.Services
{
    public abstract class CDPackagesRepository
    {
        public abstract Packages[] packagesUser(int account);

        public abstract Packages[] detailsPackages(int account);

        public abstract int createPackage(int weight, int size, String type, int price, String description, int account);
    }
}