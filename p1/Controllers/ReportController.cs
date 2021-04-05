using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using p1.Models;
using p1.Repositories;
using p1.ReportsModel;
using System.Data.Entity;

namespace p1.Controllers
{
    public class ReportController : Controller
    {
        ProjectDBEntities context = new ProjectDBEntities();
        // GET: Report
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
                var restaurat_code = Convert.ToInt32(Session["restaurat_code"]);
                VendorRepo dropdown = new VendorRepo();
                Vendor_Tax vendor_Tax = new Vendor_Tax();
                vendor_Tax.vendors = dropdown.GetVendor();
                return View(vendor_Tax);
            }
        }


        [HttpGet]
        [Route("Report/GetDataTax/{vendorCode}/{startDate}/{endDate}")]
        public ActionResult GetDataTax(int vendorCode,string startDate,string endDate)
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
                DateTime? _startDate;
                DateTime? _endDate;
                if (startDate == "")
                {
                    _startDate = null;
                }
                else
                {
                    _startDate = Convert.ToDateTime(startDate);
                }
                if (endDate == "")
                {
                    _endDate = null;
                }

                else
                {
                    _endDate = Convert.ToDateTime(endDate);
                }

                if (_startDate is null && _endDate is null)
                {
                    var vendor = context.Purchases.Where(x => x.vendor_no == vendorCode).Select(x => new { x.purchase_no,x.order_date,x.invoice_date, x.invoice_no, x.tax }).ToList();


                    if (vendor is null)
                    {
                        return Json(null);
                    }
                    else
                    {
                        return Json(new { data = vendor }, JsonRequestBehavior.AllowGet);

                    }
                }
                else if ((_startDate != null) && _endDate is null)
                {
                    var vendor = context.Purchases.Where(x => x.vendor_no == vendorCode && x.order_date >= _startDate).Select(x => new { x.purchase_no, x.order_date, x.invoice_date, x.invoice_no, x.tax }).ToList();

                    if (vendor is null)
                    {
                        return Json(null);
                    }
                    else
                    {
                        return Json(new { data = vendor }, JsonRequestBehavior.AllowGet);
                    }
                }

                else if ((_endDate != null) && _startDate is null)
                {
                    var vendor = context.Purchases.Where(x => x.vendor_no == vendorCode && x.order_date <= _endDate).Select(x => new { x.purchase_no, x.order_date, x.invoice_date, x.tax }).ToList();
                    if (vendor is null)
                    {
                        return Json(null);
                    }
                    else
                    {
                        return Json(new { data = vendor }, JsonRequestBehavior.AllowGet);
                    }
                }

                else if ((_endDate != null) && (_startDate != null))
                {
                    var vendor = context.Purchases.Where(x => x.vendor_no == vendorCode && x.order_date >= _startDate && x.order_date <= _endDate).Select(x => new { x.purchase_no, x.order_date,x.invoice_date, x.invoice_no, x.tax }).ToList();
                    if (vendor is null)
                    {
                        return Json(null);
                    }
                    else
                    {
                        return Json(new { data = vendor }, JsonRequestBehavior.AllowGet);
                    }

                }
            }
            return View();
        }

        public ActionResult ItemsReport()
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
                var restaurat_code = Convert.ToInt32(Session["restaurat_code"]);
                ItemRepo dropdown = new ItemRepo();
                ItemBase itemBase = new ItemBase();
                itemBase.items = dropdown.GetItemCode();
                return View(itemBase);
            }
        }
        [HttpGet]
        [Route("Report/GetDataItem/{itemCode}/{startDate}/{endDate}")]

        public ActionResult GetDataItem(int itemCode, string startDate, string endDate)
            {
                TempData["role"] = Session["role"].ToString();
                DateTime? _startDate;
                DateTime? _endDate;
                if (startDate == "")
                {
                    _startDate = null;
                }
                else
                {
                    _startDate = Convert.ToDateTime(startDate);
                }
                if (endDate == "")
                {
                    _endDate = null;
                }

                else
                {
                    _endDate = Convert.ToDateTime(endDate);
                }
                if (_startDate is null && _endDate is null)
                {




                    var items =
                    context.Purchase_Detail.Join(context.Purchases, pd => pd.purchase_no, p => p.purchase_no, (pd, p) => new { pd, p })
                    .Where(m => m.pd.item_code == itemCode)
                    .Select(m => new { m.p.purchase_no, m.p.order_date, m.pd.item_rate, m.pd.qty, m.pd.amount }).ToList();
                    if (items is null)
                    {
                        return Json(null);
                    }
                    else
                    {
                        return Json(new { data = items }, JsonRequestBehavior.AllowGet);

                    }
                }

                else if ((_startDate != null) && _endDate is null)
                {
                    var items =
                    context.Purchase_Detail.Join(context.Purchases, pd => pd.purchase_no, p => p.purchase_no, (pd, p) => new { pd, p })
                    .Where(m => m.pd.item_code == itemCode && m.p.order_date >= _startDate)
                    .Select(m => new { m.p.purchase_no, m.p.order_date, m.pd.item_rate, m.pd.qty, m.pd.amount }).ToList();

                    if (items is null)
                    {
                        return Json(null);
                    }
                    else
                    {
                        return Json(new { data = items }, JsonRequestBehavior.AllowGet);
                    }
                }

                else if ((_endDate != null) && _startDate is null)
                {
                    var items =
                    context.Purchase_Detail.Join(context.Purchases, pd => pd.purchase_no, p => p.purchase_no, (pd, p) => new { pd, p })
                    .Where(m => m.pd.item_code == itemCode && m.p.order_date <= _endDate)
                    .Select(m => new { m.p.purchase_no, m.p.order_date, m.pd.item_rate, m.pd.qty, m.pd.amount }).ToList();

                    if (items is null)
                    {
                        return Json(null);
                    }
                    else
                    {
                        return Json(new { data = items }, JsonRequestBehavior.AllowGet);
                    }
                }

                else if ((_endDate != null) && (_startDate != null))
                {
                    var items =
                    context.Purchase_Detail.Join(context.Purchases, pd => pd.purchase_no, p => p.purchase_no, (pd, p) => new { pd, p })
                    .Where(m => m.pd.item_code == itemCode && m.p.order_date >= _startDate && m.p.order_date < _endDate)
                    .Select(m => new { m.p.purchase_no, m.p.order_date, m.pd.item_rate, m.pd.qty, m.pd.amount }).ToList();

                    if (items is null)
                    {
                        return Json(null);
                    }
                    else
                    {
                        return Json(new { data = items }, JsonRequestBehavior.AllowGet);
                    }
                }



                return View();
            }
        public ActionResult StatusReport()
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

                return View();
            }
        }


        public JsonResult GetDataStatusNew()
        {
            /* var list = context.Purchases.Join(context.Purchase_Detail, x => x.purchase_no, y => y.purchase_no, (x, y) => new { x, y })

                 .Where(val => val.x.purchase_no.Equals(val.y.purchase_no) && val.x.status.Equals("New"))
                 .Select(val => new { val.x.purchase_no, val.x.Vendor_Master.vendor_name, val.x.order_date, val.y.amount }).ToList();*/
            var list = context.Purchases.Join(context.Purchase_Detail, x => x.purchase_no, y => y.purchase_no, (x, y) => new { x, y })
                .Where(x=>x.x.status.Equals("New"))
                .GroupBy(data => new { data.x.purchase_no, data.x.order_date, data.x.Vendor_Master.vendor_name })
                .Select(data => new { data.Key.purchase_no, data.Key.order_date, data.Key.vendor_name,diff=(DbFunctions.DiffDays(data.Key.order_date, DateTime.Now)) ,count = data.Sum(c => c.y.amount) }).ToList();
                   
       
            if (list is null)
                return Json(null);
            else
                return Json(new { data = list}, JsonRequestBehavior.AllowGet);

        }



        public JsonResult GetDataStatusApproved()
        {
            var list = context.Purchases.Join(context.Purchase_Detail, x => x.purchase_no, y => y.purchase_no, (x, y) => new { x, y })
              .Where(x => x.x.status.Equals("Approved"))
              .GroupBy(data => new { data.x.purchase_no, data.x.order_date, data.x.Vendor_Master.vendor_name,data.x.invoice_date,data.x.invoice_no })
              .Select(data => new { data.Key.purchase_no, data.Key.order_date,data.Key.invoice_date,data.Key.invoice_no,data.Key.vendor_name, count = data.Sum(c => c.y.amount) }).ToList();


            if (list is null)
                return Json(null);
            else
                return Json(new { data = list }, JsonRequestBehavior.AllowGet);

        }
    }

    }
