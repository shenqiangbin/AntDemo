using AntDemoWeb.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntDemoWeb.Filter
{
    public class ExcepitonFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            try
            {
                string request = filterContext.RequestContext.HttpContext.Request.RawUrl;
                string message = filterContext.Exception.Message;
                string stackTrace = filterContext.Exception.StackTrace;

                Logger.Log($"{request} \r\n {message} \r\n {stackTrace}");
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message + ex.StackTrace);
            }
        }
    }
}