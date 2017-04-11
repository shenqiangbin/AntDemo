using AntDemoWeb.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AntDemoWeb.Controllers
{
    public class SysController : ApiController
    {
        [HttpGet] //不加的话会出现 The requested resource does not support http method 'GET'
        public IHttpActionResult InitDB()
        {
            try
            {
                //throw new Exception("lsjdf");
                SysMgr mgr = new SysMgr();
                mgr.InitDB();
                return Json(new { code = 1, msg = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, msg = ex.Message });
            }            
        }
    }
}
