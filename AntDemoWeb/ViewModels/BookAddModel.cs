using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntDemoWeb.ViewModels
{
    public class BookAddModel
    {
        public string BookName { get; set; }
        public string BookDesc { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}