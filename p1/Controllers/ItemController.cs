using p1.Models;
using p1.ModelView;
using p1.Repositories;
using System.Linq;
using System.Web.Mvc;
using System;
using p1.Repositories;
namespace p1.Controllers
{
    public class ItemController : Controller
    {
        // GET: Report
        ProjectDBEntities context = new ProjectDBEntities();
        public ActionResult Index()
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Login", "Account");

            }
            else if (Session["role"].ToString() == "Accountant")
            {
                TempData["role"] = Session["role"].ToString();
                return RedirectToAction("Index", "Roles");
            }
            else
            {
                TempData["role"] = Session["role"].ToString();
                var list = context.Item_Master.Join(context.Inventories, x => x.item_code, y => y.item_code,
                  (x, y) => new InventoryRepo { item = x, inventory = y }).OrderBy(data=>data.item.item_name).ToList();
                //var list = context.Item_Master.Join(context.Inventories, x => x.item_code, y => y.item_code, (x, y) => new InventoryRepo { item = x, inventory = y }).ToList();
                return View(list);
            }
        }
        [Route("Item/Delete/{item_code}")]
        public ActionResult Delete(int item_code)
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Login", "Account");

            }
            else if (Session["role"].ToString() == "Accountant")
            {
                TempData["role"] = Session["role"].ToString();
                return RedirectToAction("Index", "Roles");
            }

            else
            {

                TempData["role"] = Session["role"].ToString();
                Inventory inventory = context.Inventories.Where(x => x.item_code == item_code).SingleOrDefault();
                context.Inventories.Remove(inventory);
                Item_Master item = context.Item_Master.Where(x => x.item_code == item_code).SingleOrDefault();
                context.Item_Master.Remove(item);
                context.SaveChanges();


                return RedirectToAction("Index");
            }
        }
        [Route("Item/Increase/{item_code}")]
        public ActionResult Increase(int item_code)
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Login", "Account");

            }
            else if (Session["role"].ToString() == "Accountant")
            {
                TempData["role"] = Session["role"].ToString();
                return RedirectToAction("Index", "Roles");
            }

            else
            {
                TempData["role"] = Session["role"].ToString();
                Inventory i = context.Inventories.Where(x => x.item_code == item_code).SingleOrDefault();
                i.current_qty = i.current_qty + 1;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [Route("Item/Decrease/{item_code}")]
        public ActionResult Decrease(int item_code)
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Login", "Account");

            }
            else if (Session["role"].ToString() == "Accountant")
            {
                TempData["role"] = Session["role"].ToString();
                return RedirectToAction("Index", "Roles");
            }

            else
            {
                TempData["role"] = Session["role"].ToString();
                Inventory i = context.Inventories.Where(x => x.item_code == item_code).SingleOrDefault();
                i.current_qty = i.current_qty - 1;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
        }

       public ActionResult Create()
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Login", "Account");

            }
            else if (Session["role"].ToString() == "Accountant")
            {
                TempData["role"] = Session["role"].ToString();
                return RedirectToAction("Index", "Roles");
            }

            else
            {
                TempData["role"] = Session["role"].ToString();
                return View();
            }
        }
        [HttpPost]
        public ActionResult Create(InventoryRepo inventoryRepo)
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Login", "Account");

            }
            else if (Session["role"].ToString() == "Accountant")
            {
                TempData["role"] = Session["role"].ToString();
                return RedirectToAction("Index", "Roles");
            }

            else
            {

                TempData["role"] = Session["role"].ToString();
                context.Item_Master.Add(inventoryRepo.item);
                context.SaveChanges();
                if (inventoryRepo.item.item_code > 0)
                {
                    inventoryRepo.inventory.item_code = inventoryRepo.item.item_code;
                    context.Inventories.Add(inventoryRepo.inventory);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
        }
        [Route("Item/Edit/{item_code}")]

        public ActionResult Edit(int item_code)
        {

            if (Session["role"] == null)
            {
                return RedirectToAction("Login", "Account");

            }
            else if (Session["role"].ToString() == "Accountant")
            {
                TempData["role"] = Session["role"].ToString();
                return RedirectToAction("Index", "Roles");
            }

            else
            {
                TempData["role"] = Session["role"].ToString();
                var item = context.Item_Master.SingleOrDefault(x => x.item_code == item_code);
                var inventory = context.Inventories.SingleOrDefault(x => x.item_code == item_code);
                InventoryRepo inventoryRepo = new InventoryRepo();
                inventoryRepo.item = item;
                inventoryRepo.inventory = inventory;
                return View(inventoryRepo);
            }
        }
        [HttpPost]
        public ActionResult Edit(InventoryRepo inventoryRepo)
        {
            var item_code = inventoryRepo.item.item_code;
            Item_Master item = context.Item_Master.SingleOrDefault(x => x.item_code == item_code);
            Inventory inventory= context.Inventories.SingleOrDefault(x => x.item_code == item_code);
            item.item_name = inventoryRepo.item.item_name;
            item.item_rate = inventoryRepo.item.item_rate;
            item.item_unit = inventoryRepo.item.item_unit;
            inventory.min_qty = inventoryRepo.inventory.min_qty;
            inventory.current_qty = inventoryRepo.inventory.current_qty;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    } 

}