using EventApps.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventApps.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            var listOfUser = UserAccessHelper.GetAllUserAccess();
            return View(listOfUser);
        }

        [HttpPost]
        public ActionResult CheckLogin(string Username, string Password)
        {
            if (Username.Equals("") || Password.Equals(""))
            {
                TempData["msg"] = "<script>alert('Harap isi Username dan Password');</script>";
                return View("Index");
            }
            else
            {
                var item = LoginHelper.CheckUserLogin(Username, Password);
                if (item != null)
                {
                    Session["Username"] = item.Username;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["msg"] = "<script>alert('Login Failed');</script>";
                    return View("Index");
                }
            }
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index");
        }

    }
}