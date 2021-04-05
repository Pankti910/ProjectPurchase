using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace p1.ModelView
{
    public class Detail
    { 
        [DisplayName("Purchase No")]
        [Required(ErrorMessage = "Purchase No is required")]
        public string purchase_no { get; set; }

        [DisplayName("Invoice No")]
        [Required(ErrorMessage = "Invoice No is required")]
        public long? invoice_no { get; set; }

        [DisplayName("Vendor")]
        [Required(ErrorMessage = "Vendor is required")]
        public int vendor_no { get; set; }

        [DisplayName("Vendor Name")]
        [Required(ErrorMessage = "Vendor is required")]
        public string vendor_name { get; set; }


        [DisplayName("Invoice Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> invoice_date { get; set; }


        public string item_name { get; set; }
        public int qty { get; set; }
        public long item_rate { get; set; }
        public long amount { get; set; }
        public string Message { get; set; }
       

        public List<ItemsViewModel> List { get; set; }
        

    }
}