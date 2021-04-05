using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using p1.Models;
namespace p1.ModelView
{
    public class ReportModel
    {

        public IEnumerable<SelectListItem> vendors { get; set; }

        public IEnumerable<SelectListItem> items { get; set; }
        public int item_code { get; set; }
        public int vendor_no { get; set; }
    }
}