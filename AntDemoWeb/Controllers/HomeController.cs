﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntDemoWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            int j = 0;
            var r = 1 / j;
            return View();
        }

        public ActionResult Record()
        {
            return View();
        }
    }
}