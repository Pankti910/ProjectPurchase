using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using p1.Models;
using p1.ModelView;
using Newtonsoft.Json;
using p1.Repositories;
using System.Data.Entity;


namespace p1.ModelView
{
    public class ShowPurchaseController : Controller
    {
        // GET: ShowPurchase
        ProjectDBEntities context = new ProjectDBEntities();


        public ActionResult Index()
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Login", "Account");

            }
            else if (Session["role"].ToString() == "Chef")
            {
                TempData["role"] = Session["role"].ToString();
                return RedirectToAction("Index", "Roles");
            }
            else
            {
                TempData["role"] = Session["role"].ToString();
                var list = context.Purchases.ToList();
                return View(list);
            }
        }
        public ActionResult Approved()
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Login", "Account");

            }
            else if (Session["role"].ToString() == "Chef" )
            {
                TempData["role"] = Session["role"].ToString();
                return RedirectToAction("Index", "Roles");
            }
            else
            {
                TempData["role"] = Session["role"].ToString();
                var list = context.Purchases.ToList();
                return View(list);
            }
        }
        [HttpGet]
        [Route("ShowPurchase/Delete/{purchase_no?}")]

        public ActionResult Delete(string purchase_no)
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Login", "Account");

            }
            else if (Session["role"].ToString() == "Chef")
            {
                TempData["role"] = Session["role"].ToString();
                return RedirectToAction("Index", "Roles");
            }
            else
            {
                TempData["role"] = Session["role"].ToString();
                try
                {

                    var pd = context.Purchase_Detail.Where(x => x.purchase_no.Equals(purchase_no)).ToList();

                    foreach (var data in pd)
                    {
                        context.Purchase_Detail.Remove(data);
                        context.SaveChanges();

                    }
                    var purchase = context.Purchases.SingleOrDefault(x => x.purchase_no.Equals(purchase_no));
                    context.Purchases.Remove(purchase);
                    context.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(nameof(DetailsPurchase), purchase_no);
                }
            }
        }
        public ActionResult DetailsPurchase(string purchase_no)
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Login", "Account");

            }
            else if (Session["role"].ToString() == "Chef")
            {
                TempData["role"] = Session["role"].ToString();
                return RedirectToAction("Index", "Roles");
            }
            else
            {
                TempData["role"] = Session["role"].ToString();
                if (purchase_no == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var editdata = context.Purchases.SingleOrDefault(x => x.purchase_no.Equals(purchase_no));
                if (editdata == null)
                {
                    return HttpNotFound();
                }
                int id = 0;
                List<ItemsViewModel> list = new List<ItemsViewModel>();
                var data_list = context.Purchase_Detail.Where(x => x.purchase_no.Equals(purchase_no)).ToList();
                foreach (var data_item in data_list)
                {
                    ItemsViewModel itemsView = new ItemsViewModel();
                    itemsView.id = id + 1;
                    itemsView.item_name = data_item.item_name;
                    itemsView.item_rate = data_item.item_rate;
                    itemsView.qty = data_item.qty;
                    itemsView.amount = data_item.item_rate * data_item.qty;
                    list.Add(itemsView);
                }

                var data = context.Purchases.Where(x => x.purchase_no.Equals(purchase_no)).SingleOrDefault();


                Detail detail = new Detail();
                detail.vendor_no = data.vendor_no;
                detail.invoice_no = data.invoice_no;
                detail.invoice_date = data.invoice_date;
                detail.purchase_no = purchase_no;
                detail.vendor_name = context.Vendor_Master.Where(x => x.vendor_code == data.vendor_no).Select(x => x.vendor_name).SingleOrDefault();
                detail.List = list;
                return View(detail);

            }


        }
        [HttpPost]
        public ActionResult UpdateStatusNew(int id)
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Login", "Account");

            }
            else if (Session["role"].ToString() == "Chef")
            {
                TempData["role"] = Session["role"].ToString();
                return RedirectToAction("Index", "Roles");
            }
            else
            {
                TempData["role"] = Session["role"].ToString();
                if (Request.IsAjaxRequest())
                {
                    Purchase purchase_to_update = context.Purchases.Where(x => x.id == id).SingleOrDefault();

                    purchase_to_update.status = "New";
                    purchase_to_update.invoice_date = null;
                    purchase_to_update.invoice_no = null;
                    purchase_to_update.tax = 0;
                    if (ModelState.IsValid)
                    {
                        context.Entry(purchase_to_update).State = EntityState.Modified;
                        var pd_list = context.Purchase_Detail.Where(x => x.purchase_no.
                        Equals(purchase_to_update.purchase_no)).ToList();
                        foreach (var data in pd_list)
                        {
                            Inventory inventory = context.Inventories.Where(x => x.item_code == data.item_code).SingleOrDefault();
                            inventory.current_qty = inventory.current_qty - data.qty;

                        }
                        context.SaveChanges();
                        return Json(string.Format("Invoice No:{0}", purchase_to_update.purchase_no));
                    }


                }
                {
                    return RedirectToAction("Index");
                }
            }
        }
        [HttpPost]
        public ActionResult Update(int id,long invoice_no,string invoice_date, long tax)
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Login", "Account");

            }
            else if (Session["role"].ToString() == "Chef")
            {
                TempData["role"] = Session["role"].ToString();
                return RedirectToAction("Index", "Roles");
            }
            else
            {
                TempData["role"] = Session["role"].ToString();
                if (Request.IsAjaxRequest())
                {
                    DateTime date = Convert.ToDateTime(invoice_date);
                    Purchase purchase_to_update = context.Purchases.Where(x => x.id == id).SingleOrDefault();
                    purchase_to_update.invoice_date = date;
                    purchase_to_update.invoice_no = invoice_no;
                    purchase_to_update.tax = tax;
                    purchase_to_update.status = "Approved";

                    if (ModelState.IsValid)
                    {
                        context.Entry(purchase_to_update).State = EntityState.Modified;
                        var pd_list = context.Purchase_Detail.Where(x => x.purchase_no.Equals(purchase_to_update.purchase_no)).ToList();
                        foreach (var data in pd_list)
                        {
                            Inventory inventory = context.Inventories.Where(x => x.item_code == data.item_code).SingleOrDefault();
                            inventory.current_qty = inventory.current_qty + data.qty;
                            context.SaveChanges();
                        }
                    }
                    context.SaveChanges();
                    return Json(string.Format("Invoice No:{0}", invoice_date));
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }

        
    }
}