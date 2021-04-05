using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace p1.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult Index()
        {
            if (Session["login_id"] != null)
            {
                if (TempData["role"].ToString() == "Accountant")
                {
                    return RedirectToAction("Index", "Purchase");
                }
                else if (TempData["role"].ToString() == "Manager")
                {
                    return RedirectToAction("Index", "ShowPurchase");
                }
                else if (TempData["role"].ToString() == "Chef")
                {
                    return RedirectToAction("Index", "Item");
                }
                else
                {
                    return RedirectToAction("Index", "Account");
                }
            }

            else
            {
                return RedirectToAction("Index", "Account");
            }
        }
    }
}