using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace p1.ModelView
{
    public class ItemsViewModel
    {
        public int id { get; set; }
        public int item_code { get; set; }
        public string item_name { get; set; }
        public int qty { get; set; }
        public long item_rate { get; set; }
        public long amount { get; set; }

    }
}