using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using p1.ModelView;
using p1.Repositories;
using p1.Models;
using Newtonsoft.Json;
using System.Net;

namespace p1.Controllers
{
    
    public class PurchaseController : Controller
    {
        ProjectDBEntities context = new ProjectDBEntities();
        
        
        
        public ActionResult Index()
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Login", "Account");

            }
            else if(Session["role"].ToString()=="Chef" || Session["role"].ToString()=="Manager")
            {
                TempData["role"] = Session["role"].ToString();
                return RedirectToAction("Index", "Roles");
            }
            else
            {
                TempData["role"] = Session["role"].ToString();
              
                var vendorRepo = new VendorRepo();
                var itemRepo = new ItemRepo();
                ModelAll modelAll = Session["Model"] as ModelAll;


                if (modelAll is null)
                {
                    modelAll = new ModelAll();
                }
                modelAll.vendors = vendorRepo.GetVendor();
                modelAll.items = itemRepo.GetItem();
                if (ViewData["msg"] != null)
                {
                    Session["data"] = null;
                }
                modelAll.List = Session["data"] as List<ItemsViewModel>;

                if (modelAll.List is null)
                {
                    modelAll.List = new List<ItemsViewModel>();

                }
                return View(modelAll);
            }
          
           
        }
        [HttpPost]
        public ActionResult Index(ModelAll modelAll, string addItemList,string finalAddVendor,string finalAddItem,string placeOrder)

        {
                    modelAll.List_Min = new List<ItemsViewModel>();
                    if (!string.IsNullOrEmpty(finalAddVendor))
                    {
                        Vendor_Master vm = new Vendor_Master();
                        modelAll.Message = null;
                        vm.vendor_name = modelAll.vendorAdd.vendor_name;
                        vm.vendor_address = modelAll.vendorAdd.vendor_address;
                        vm.vendor_state = modelAll.vendorAdd.vendor_state;
                        vm.vendor_contactno = modelAll.vendorAdd.vendor_contactno;
                        vm.vendor_GSTno = modelAll.vendorAdd.vendor_GSTno;
                        context.Vendor_Master.Add(vm);
                        context.SaveChanges();
                        var vendorRepo = new VendorRepo();
                        var itemRepo = new ItemRepo();
                        modelAll.vendors = vendorRepo.GetVendor();
                        modelAll.items = itemRepo.GetItem();
                        modelAll.List = Session["data"] as List<ItemsViewModel>;
                        if (modelAll.List is null)
                        {
                            modelAll.List = new List<ItemsViewModel>();

                        }

                        modelAll.vendor_no = context.Vendor_Master.Where(x => x.vendor_name.Equals(modelAll.vendorAdd.vendor_name)
                          && x.vendor_contactno == modelAll.vendorAdd.vendor_contactno &&
                          x.vendor_GSTno.Equals(modelAll.vendorAdd.vendor_GSTno)
                        ).Select(x => x.vendor_code).SingleOrDefault();
                        modelAll.vendor_Master.vendor_contactno = modelAll.vendorAdd.vendor_contactno;
                        modelAll.vendor_Master.vendor_GSTno = modelAll.vendorAdd.vendor_GSTno;
                        modelAll.vendor_Master.vendor_state = modelAll.vendorAdd.vendor_state;
                        modelAll.vendor_Master.vendor_address = modelAll.vendorAdd.vendor_address;
                        TempData["Message"] = "No";

                TempData["role"] = Session["role"].ToString();
                return View(modelAll);
                 

                }
                if (!string.IsNullOrEmpty(finalAddItem))
                {
                        Item_Master im = new Item_Master();
                        modelAll.Message = null;
                        im.item_name = modelAll.itemAddModel.item_name;
                        im.item_rate = modelAll.itemAddModel.item_rate;
                        im.item_unit = modelAll.itemAddModel.item_unit;
                        context.Item_Master.Add(im);
                        context.SaveChanges();

                        //write code inventory
                        Inventory inventory = new Inventory();
                        inventory.item_code = im.item_code;
                        inventory.min_qty = 1;
                        inventory.current_qty = 0;

                        context.Inventories.Add(inventory);
                        context.SaveChanges();


                        var vendorRepo = new VendorRepo();
                        var itemRepo = new ItemRepo();
                        modelAll.vendors = vendorRepo.GetVendor();
                        modelAll.items = itemRepo.GetItem();
                        modelAll.List = Session["data"] as List<ItemsViewModel>;
                        if (modelAll.List is null)
                        {
                            modelAll.List = new List<ItemsViewModel>();

                        }
                        modelAll.item_name = modelAll.itemAddModel.item_name;
                        modelAll.item_rate = modelAll.itemAddModel.item_rate;
                        modelAll.item_Master.item_unit = modelAll.itemAddModel.item_unit;
                        modelAll.qty = 1;

                TempData["role"] = Session["role"].ToString();
                TempData["Message"] = "No";
                        return View(modelAll);
                    

                }

                if (!string.IsNullOrEmpty(addItemList))
                {
                    
                        int id = 0;
                        ModelAll modelAll_send = new ModelAll();
                        modelAll_send = modelAll;
                        ItemsViewModel ItemObject = new ItemsViewModel();
                        modelAll.Message = null;
                        modelAll.List = Session["data"] as List<ItemsViewModel>;
                        if (modelAll.List is null)
                        {
                            Session["id"] = 1;
                            modelAll.List = new List<ItemsViewModel>();
                        }
                        else
                        {
                            Session["id"] = Convert.ToInt32(Session["id"]) + 1;
                        }
                        id = Convert.ToInt32(Session["id"]);

                        ItemObject.id = id;
                        ItemObject.item_name = modelAll.item_name;
                        ItemObject.item_rate = modelAll.item_rate;
                        ItemObject.qty = modelAll.qty;
                        ItemObject.amount = ItemObject.item_rate * ItemObject.qty;


                        modelAll.List.Add(ItemObject);

                        Session["data"] = modelAll.List;
                        modelAll_send.List = modelAll.List;
                        Session["id"] = Convert.ToInt32(Session["id"]);

                        var vendorRepo = new VendorRepo();
                        var itemRepo = new ItemRepo();
                        modelAll.vendors = vendorRepo.GetVendor();
                        modelAll.items = itemRepo.GetItem();
                        modelAll.item_name = " ";
                        modelAll.item_rate = 0;
                        modelAll.item_Master.item_unit = "";
                TempData["role"] = Session["role"].ToString();
                        TempData["Message"] = "No";
                        return View(modelAll);
                    
                }
                if (!string.IsNullOrEmpty(placeOrder))
                {
                    
                        modelAll.List = Session["data"] as List<ItemsViewModel>;
                    if (modelAll.List != null)
                    {
                            {
                                var last_record = context.Purchases.OrderByDescending(x => x.id).FirstOrDefault();
                                if (last_record is null)
                                {
                                    modelAll.purchase_no = "PO-000001";
                                }
                                else
                                {
                                    string last_purchase_no = last_record.purchase_no;
                                    var parts = last_purchase_no.Split('-');
                                    string po = parts[0];
                                    int number = Convert.ToInt32(parts[1]) + 1;
                                    modelAll.purchase_no = po + "-" + number.ToString("D6");
                                }

                                Purchase purchase = new Purchase();
                                purchase.purchase_no = modelAll.purchase_no;
                                purchase.status = "New";
                                purchase.order_date = modelAll.order_date;
                                purchase.vendor_no = modelAll.vendor_no;
                                try
                                {


                                    context.Purchases.Add(purchase);
                                    context.SaveChanges();
                                }
                                catch (Exception e)
                                {
                                    return Content(e.ToString());
                                }
                                bool check = context.Purchases.Any(x => x.purchase_no.Equals(modelAll.purchase_no));
                                if (check)
                                {
                                    foreach (ItemsViewModel item in modelAll.List)
                                    {

                                        Purchase_Detail pd = new Purchase_Detail();
                                        pd.item_code = context.Item_Master.Where(x => x.item_name.Equals(item.item_name)).Select(x => x.item_code)
                                             .SingleOrDefault();
                                        pd.item_name = item.item_name;
                                        pd.purchase_no = modelAll.purchase_no;
                                        pd.qty = item.qty;
                                        pd.item_rate = item.item_rate;
                                        pd.amount = item.amount;
                                        context.Purchase_Detail.Add(pd);
                                        context.SaveChanges();
                                    }
                                }
                                var vendorRepo = new VendorRepo();
                                var itemRepo = new ItemRepo();
                                modelAll.vendors = vendorRepo.GetVendor();
                                modelAll.items = itemRepo.GetItem();
                                var role = Session["role"].ToString();
                        var login_id = Session["login_id"].ToString();
                        Session["data"] = null;
                        Session["login_id"] = login_id;
                        TempData["role"] = Session["role"].ToString();
                        ViewData["msg"] = "Your Purchase Order No:" + modelAll.purchase_no;
                                return View(modelAll);

                            
                        }
                    }
                }


                return View(modelAll);
            
        }
        [HttpPost]
        [Route("Purchase/getVendorInfo/{vendor_no?}")]
        public JsonResult getVendorInfo(int vendor_no)
        {
            TempData["role"] = Session["role"].ToString();
            //int id = 1; 
            var restaurat_code = Convert.ToInt32(Session["restaurat_code"]);
            var _data = context.Vendor_Master.Where(x => x.vendor_code == vendor_no).Select(x=>new {x.vendor_code, x.vendor_name,x.vendor_state,x.vendor_GSTno,x.vendor_address,x.vendor_contactno}).ToList();
            var data = new { data=_data};
            return Json(data, JsonRequestBehavior.AllowGet);
            //return Json(new { data });
        }

        [HttpPost]
        [Route("Purchase/ItemRate/{item_name?}")]
        public JsonResult ItemRate(string item_name)
        {
            TempData["role"] = Session["role"].ToString();

            var _data = context.Item_Master.Where(x => x.item_name.Equals(item_name) ).Select(x => new { x.item_rate, x.item_unit }).ToList();
            var data = new { data = _data };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet] 
        public ActionResult Reload()
        {
            TempData["role"] = Session["role"].ToString();

            ModelAll modelAll = new ModelAll();
            var vendorRepo = new VendorRepo();
            var itemRepo = new ItemRepo();
            modelAll.vendors = vendorRepo.GetVendor();
            modelAll.items = itemRepo.GetItem();

            return RedirectToAction("Index", modelAll);

        }
        [HttpGet]
        public ActionResult Delete(int? id, ModelAll detail)
        {
            if (Session["role"].ToString() == "Chef" || Session["role"].ToString() == "Manager")
            {
                TempData["role"] = Session["role"].ToString();
                return RedirectToAction("Index", "Roles");
            }
            else
            {
                TempData["role"] = Session["role"].ToString();
                var vendorRepo = new VendorRepo();
                var itemRepo = new ItemRepo();
                detail.vendors = vendorRepo.GetVendor();
                detail.items = itemRepo.GetItem();
                if (detail is null)
                {
                    detail = new ModelAll();
                }
                detail.List = Session["data"] as List<ItemsViewModel>;
                if (detail.List is null)
                {
                    Session["id"] = 1;
                    detail.List = new List<ItemsViewModel>();
                }

                ItemsViewModel itemTodelete = detail.List.Find(x => x.id == id);
                detail.List.Remove(itemTodelete);

                Session["ContainModel"] = detail;
                Session["data"] = detail.List;
                detail.List_Min = new List<ItemsViewModel>();
                return RedirectToAction("Index", detail);
            }

        }
      
    }
}