using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using p1.Models;
using p1.Repositories;
namespace p1.ModelView
{
    public class ModelAll
    {
        public ProjectDBEntities context;
        public ModelAll()
        {
            context = new ProjectDBEntities();
        }
        public Vendor_Master vendor_Master { get; set; }
        public Item_Master item_Master { get; set; }
        public Purchase purchase { get; set; }
        public Purchase_Detail purchase_Detail{get;set;}

        public vendorAddModel vendorAdd { get; set; }
        public itemAddModel itemAddModel { get; set; }

        public Inventory inventory { get; set; }
        public List<ItemsViewModel> List { get; set; }
        public List<ItemsViewModel> List_Min { get; set; }
        public string Message { get; set; }


        public string item_name { get; set; }
        public int qty { get; set; }
        public long item_rate { get; set; }
        public long amount { get; set; }


        public IEnumerable<SelectListItem> vendors { get; set; }

        public IEnumerable<SelectListItem> items { get; set; }
        public IEnumerable<SelectListItem> items_code { get; set; }

        
        public string purchase_no { get; set; }
        public long? invoice_no { get; set; }
        public int vendor_no { get; set; }
        public long tax { get; set; }
        public string status { get; set; }       
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> invoice_date { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> order_date { get; set; }


    }
}
public enum Status
{
    New,
    Approved,
    Pendding,
    Check
}