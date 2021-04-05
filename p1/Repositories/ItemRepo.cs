using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using p1.Models;
namespace p1.Repositories
{
    public class ItemRepo
    {

        private ProjectDBEntities context;
        public ItemRepo()
        {
            context = new ProjectDBEntities();
        }
        public IEnumerable<SelectListItem> GetItem()
        {
            IEnumerable<SelectListItem> itemListItem = new List<SelectListItem>();



            List<Inventory> inventories = new List<Inventory>();
            itemListItem = context.Item_Master.Select(x => new

                SelectListItem
            {
                Text =x.item_name,
                Value = x.item_name
            }
            ).ToList().OrderBy(x=>x.Text);
           
            /*foreach(Inventory data in inventories)
            {
                if(data.min_qty>data.current_qty)
                {
                    itemListItem.Where(x => Convert.ToInt32(x.Value) == data.item_code).SingleOrDefault();
                    var text = itemListItem.Where(x => Convert.ToInt32(x.Value) == data.item_code).Select(x=>x.Text)+"(Low)";
                    itemListItem.Where(x => Convert.ToInt32(x.Value) == data.item_code).SingleOrDefault();

                }
            }*/
            
            return itemListItem;
        }

        public IEnumerable<SelectListItem> GetItemCode()
        {
            IEnumerable<SelectListItem> itemListItem = new List<SelectListItem>();
      //          int restaurat_code = Convert.ToInt32(HttpContext.Current.Session["restaurat_code"]);
        
            itemListItem = context.Item_Master.Select(x => new

                SelectListItem
            {
                Text = x.item_name,
                Value = x.item_code.ToString()
            }
            ).ToList().OrderBy(x => x.Text);
            return itemListItem;
         
        }
    }
}