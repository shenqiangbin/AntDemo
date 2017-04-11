using AntDemoWeb.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntDemoWeb.Common
{
    public class Logger
    {
        public static void Init()
        {
            log4net.Config.XmlConfigurator.Configure();
            GlobalFilters.Filters.Add(new ExcepitonFilter());
        }

        public static void Log(string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("mvclog");
            log.Error(msg);
        }

        public static void Log(Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("mvclog");
            log.Error(ex);
        }

    }
}