using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using p1.Models;
namespace Project.Controllers
{
    public class VendorController : Controller
    {

        ProjectDBEntities context = new ProjectDBEntities();
        
        // GET: Home
        public ActionResult Index()
        {

          //  
            if (Session["role"]==null)
            {
                return RedirectToAction("Login", "Account");
          
            }
            else if (Session["role"].ToString() == "Accountant"|| Session["role"].ToString() == "Chef")
            {
                TempData["role"] = Session["role"].ToString();
                return RedirectToAction("Index", "Roles");
            }

            else
            {
                TempData["role"] = Session["role"].ToString();
                var vendors = context.Vendor_Master.ToList();
                return View(vendors);

            }
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Login", "Account");

            }

            else if (Session["role"].ToString() == "Accountant" || Session["role"].ToString() == "Chef")
            {
                TempData["role"] = Session["role"].ToString();
                return RedirectToAction("Index", "Roles");
            }
            else
            {
                TempData["role"] = Session["role"].ToString();
                var vendor = context.Vendor_Master.SingleOrDefault(v => v.vendor_code == id);
                return View(vendor);
            }
        }

        // GET: Home/Create
       
        public ActionResult Create()
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Login", "Account");

            }

            else if (Session["role"].ToString() == "Accountant" || Session["role"].ToString() == "Chef")
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

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(Vendor_Master vendor)
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Login", "Account");

            }

            else if (Session["role"].ToString() == "Accountant" || Session["role"].ToString() == "Chef")
            {
                TempData["role"] = Session["role"].ToString();
                return RedirectToAction("Index", "Roles");
            }
            else
            {
                TempData["role"] = Session["role"].ToString();
                try
                {
                    // TODO: Add insert logic here
                    context.Vendor_Master.Add(vendor);
                    context.SaveChanges();
                    if (vendor.vendor_code > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                catch
                {
                    return View();
                }
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Login", "Account");

            }

            else if (Session["role"].ToString() == "Accountant" || Session["role"].ToString() == "Chef")
            {
                TempData["role"] = Session["role"].ToString();
                return RedirectToAction("Index", "Roles");
            }
            else
            {
                TempData["role"] = Session["role"].ToString();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var vendor = context.Vendor_Master.SingleOrDefault(v => v.vendor_code == id);
                if (vendor == null)
                {
                    return HttpNotFound();
                }
                return View(vendor);
            }
        }


        [HttpPost]
        public ActionResult Edit(int id,Vendor_Master vendor)
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Login", "Account");

            }

            else if (Session["role"].ToString() == "Accountant" || Session["role"].ToString() == "Chef")
            {
                TempData["role"] = Session["role"].ToString();
                return RedirectToAction("Index", "Roles");
            }
            else
            {
                TempData["role"] = Session["role"].ToString();
                try
                {
                    // TODO: Add update logic here
                    if (ModelState.IsValid)
                    {
                        context.Entry(vendor).State = EntityState.Modified;
                        context.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    return View(vendor);
                }
                catch
                {
                    return View();
                }
            }
        }



        [HttpGet]
        [Route("Vendor/Delete/{id}")]
        public ActionResult Delete(int id)
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Login", "Account");

            }

            else if (Session["role"].ToString() == "Accountant" || Session["role"].ToString() == "Chef")
            {
                TempData["role"] = Session["role"].ToString();
                return RedirectToAction("Index", "Roles");
            }
            else
            {
                TempData["role"] = Session["role"].ToString();
                try
                {
                    // TODO: Add delete logic here
                    var vendor = context.Vendor_Master.SingleOrDefault(v => v.vendor_code == id);
                    context.Vendor_Master.Remove(vendor ?? throw new InvalidOperationException());
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {

                    return RedirectToAction("Index");
                }
            }
        }


        
    }
}
