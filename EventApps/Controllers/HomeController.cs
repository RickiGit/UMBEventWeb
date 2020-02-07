using EventApps.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventApps.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.TotalEvent = EventHelper.GetTotalEvent();
            ViewBag.TotalComingEvent = EventHelper.GetTotalComingEvent();
            ViewBag.TotalPastEvent = EventHelper.GetTotalPastEvent();
            ViewBag.TotalParticipant = ParticipantHelper.GetTotalParticipant();

            var listOfEvent = EventHelper.Get5ComingEvent();

            return View(listOfEvent);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}