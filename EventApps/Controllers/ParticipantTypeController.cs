using EventApps.Helpers;
using EventApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventApps.Controllers
{
    public class ParticipantTypeController : Controller
    {
        // GET: ParticipantType
        public ActionResult Index()
        {
            List<ParticipantType> listOfParticipant = ParticipantTypeHelper.GetAllListParticipantType();
            return View(listOfParticipant);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ParticipantType participant)
        {
            bool result = ParticipantTypeHelper.SaveParticipantType(participant);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["msg"] = "<script>alert('Failed to insert data');</script>";
                return View();
            }
        }
    }
}