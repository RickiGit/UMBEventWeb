using EventApps.Helpers;
using EventApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventApps.Controllers
{
    public class UserAccessController : Controller
    {
        // GET: UserAccess
        public ActionResult Index()
        {
            var listOfUserAccess = UserAccessHelper.GetAllUserAccess();
            return View(listOfUserAccess);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserAccess userAccess)
        {
            if (!userAccess.Password.Equals(userAccess.ConfirmPassword))
            {
                TempData["msg"] = "<script>alert('Confirm password not match');</script>";
                return RedirectToAction("Create");
            }
            else
            {
                var result = UserAccessHelper.SaveUser(userAccess);
                if (result)
                {
                    TempData["msg"] = "<script>alert('Saved successfuly');</script>";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["msg"] = "<script>alert('Failed to Insert Data');</script>";
                    return RedirectToAction("Create");
                }

            }
        }

        public ActionResult Edit(string nip)
        {
            var item = UserAccessHelper.GetDetailUser(nip);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(UserAccess userAccess)
        {
            if (!userAccess.Password.Equals(userAccess.ConfirmPassword))
            {
                TempData["msg"] = "<script>alert('Confirm password not match');</script>";
                return RedirectToAction("Edit", new { nip = userAccess.NIP });
            }
            else
            {
                var result = UserAccessHelper.EditUser(userAccess);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["msg"] = "<script>alert('Failed to Insert Data');</script>";
                    return RedirectToAction("Edit", new { nip = userAccess.NIP });
                }
            }
        }
    }
}