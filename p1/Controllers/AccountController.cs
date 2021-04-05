using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using p1.Models;
using System.Web.Security;

namespace p1.Controllers
{
    public class AccountController : Controller
    {
        ProjectDBEntities context = new ProjectDBEntities();
        // GET: Home
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }
        public ActionResult Login()
        {
            return View(); 

        }
        [HttpPost]
        public ActionResult Login(Login_Master login)
        {

            // int count = context.Login_Master.Where(x => x.username.Equals(login.username) && x.password.Equals(login.password)).Select(x=>x.login_id).SingleOrDefault();
            int login_id = context.Login_Master.Where(x => x.username.Equals(login.username) && x.password.Equals(x.password)).Select(x => x.login_id).FirstOrDefault();

           // return Content(count.ToString());
            if(login_id > 0)
            {

               Session["login_id"] = login_id;
                Session["role"] = context.Login_Master.
                    Join(context.Roles, x => x.role_id, y => y.role_id, (x, y) => new { x, y }).
                    Where(data => data.x.login_id == login_id).
                    Select(data => data.y.role_name).SingleOrDefault();
                TempData["role"] = Session["role"].ToString();
              
                return RedirectToAction("Index", "Roles"); 
            }
            ModelState.AddModelError("", "Invalid username and password");
            return View();
        }


        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
    }
}