using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace p1.ModelView
{
    public class vendorAddModel
    {


        [DisplayName("Vendor Name")]

        public string vendor_name { get; set; }

        [DisplayName("Contact No")]

        public long vendor_contactno { get; set; }

        [DisplayName("Address")]

        public string vendor_address { get; set; }

        [DisplayName("State")]

        public string vendor_state { get; set; }

        [DisplayName("GST No")]

        public string vendor_GSTno { get; set; }
    }
}