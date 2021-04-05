using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using p1.Models;

namespace p1.Repositories
{
    public class InventoryRepo
    {
        public Item_Master item { get; set; }
        public Inventory inventory { get; set; }
    }

}