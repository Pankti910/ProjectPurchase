using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using p1.Models;
namespace p1.Repositories
{
    public class VendorRepo
    {
        private ProjectDBEntities context;
        public VendorRepo()
        {
            context =new  ProjectDBEntities();
        }
        public IEnumerable<SelectListItem> GetVendor()
        {
            IEnumerable<SelectListItem> vendorListItem = new List<SelectListItem>();
//            int restaurat_code = Convert.ToInt32(HttpContext.Current.Session["restaurat_code"]);

            vendorListItem = context.Vendor_Master.Select(x =>
                 new SelectListItem
                 {
                     Text = x.vendor_name,
                     Value = x.vendor_code.ToString()
                 }
            ).ToList().OrderBy(x=>x.Text);
            return vendorListItem;
        }
    }
}