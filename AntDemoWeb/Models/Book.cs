using AntDemoWeb.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntDemoWeb.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string BookPath { get; set; }
        public ConvertStatusEnum ConvertStatus { get; set; }
        public string SwfPath { get; set; }
        public DateTime UploadTime { get; set; }
        public DeleteFlagEnum DeleteFlag { get; set; }
    }
}